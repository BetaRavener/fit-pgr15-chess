//#define PARALLEL

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CSG;
using CSG.Shapes;
using OpenTK;
using OpenTK.Graphics;
using RayMath;
using RayTracer;
using Chess.Scene;

namespace Raytracer
{
    public class Raytracer
    {
        private const int ComponentsPerPixel = 4;
        private const double NearPlaneDist = 1.0f;
        private const double FovY = Math.PI/4.0f;

        private int _heightInPixels;
        private int _widthInPixels;

        private LightSource _lightSource;
        private Camera _camera;
        private List<SceneObject> _sceneObjects;

        private List<Ray> _rayCache;
        private List<Color4> _colorCache;

        private static Color4 Background
        {
            get { return Color.MidnightBlue; }
        }

        public Camera Eye
        {
            get { return _camera; }
            set { _camera = value; }
        }

        public LightSource Light
        {
            get { return _lightSource; }
            set { _lightSource = value; }
        }

        public int ReflectionDepth { get; set; } = 2;

        public bool OnlyBoundingBoxes { get; set; }

        double Ratio
        {
            get { return _widthInPixels/(double) _heightInPixels; }
        }

        private double NearPlaneHeight
        {
            get { return 2*Math.Tan(FovY/2.0)*NearPlaneDist; }
        }

        private double NearPlaneWidth
        {
            get { return Ratio*NearPlaneHeight; }
        }

        private double PixelSize
        {
            get { return (1.0f/_heightInPixels)*NearPlaneHeight; }
        }

        private int _antialiasFactor = 1;

        public int AntialiasFactor
        {
            get { return _antialiasFactor; }
            set
            {
                _antialiasFactor = value;
                Resize(_widthInPixels, _heightInPixels);
                BuildRayCache();
            }
        }
        
        private int AntialiasedWidth { get { return _widthInPixels * AntialiasFactor; } }

        private int AntialiasedHeight { get { return _heightInPixels * AntialiasFactor; } }

        public int NumberOfThreads { get; set; } = 1;

        public Raytracer()
        {
            _heightInPixels = 0;
            _widthInPixels = 0;
            _camera = new Camera(100, 400, 200);
            _camera.LookAt = new Vector3d(400, 0, 400);
            _lightSource = new LightSource(-300, 300, -700);
            _sceneObjects = new List<SceneObject>();

            var game = new Game();
            game.BuildBaseLayout();

            game.Start();

            var gameLoader = new GameLoader(@".");

            gameLoader.SaveGame(game, "scene1.json");

            var loadedGame = gameLoader.LoadGame("scene1.json");

            _sceneObjects.AddRange(loadedGame.GetSceneObjects());
            _rayCache = new List<Ray>();
            _colorCache = new List<Color4>();
        }

        public void Resize(int widthInPixels, int heightInPixels)
        {
            _heightInPixels = heightInPixels;
            _widthInPixels = widthInPixels;

            _rayCache.Clear();
            _colorCache.Clear();
            var firstX = 0;
            for (var y = 0; y < AntialiasedHeight; y++)
            {
                for (var x = 0; x < AntialiasedWidth; x++)
                {
                    var ray = new Ray(Eye.Position, Vector3d.UnitZ, firstX + x);

                    _rayCache.Add(ray);
                    _colorCache.Add(Color4.Black);
                }
                firstX += AntialiasedWidth;
            }
        }


        private Intersection GetClosestIntersection(Ray ray)
        {
            // Search for closest intersection
            var closestIntersection = new Intersection(IntersectionKind.None);

            for (int index = 0; index < _sceneObjects.Count; index++)
            {
                SceneObject sceneObject = _sceneObjects[index];
                var intersection = sceneObject.IntersectFirst(ray, OnlyBoundingBoxes);
                if (intersection.Kind != IntersectionKind.None &&
                    intersection.Distance < closestIntersection.Distance)
                {
                    closestIntersection = intersection;
                }
            }

            return closestIntersection;
        }

        /// <summary>
        /// Check if specified ray intersects any object on the way to light
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="lightDistance"></param>
        /// <returns></returns>
        private bool IsInShadow(Ray ray, double lightDistance)
        {
            for (int index = 0; index < _sceneObjects.Count; index++)
            {
                var sceneObject = _sceneObjects[index];
                var inters = sceneObject.IntersectFirst(ray);

                if (inters.Kind != IntersectionKind.None && inters.Distance < lightDistance)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Traces single ray throughout the scene. 
        /// </summary>
        /// <param name="ray">Tracing ray.</param>
        /// <param name="depth">Actual level of recurse</param>
        /// <param name="noHitColor"></param>
        /// <returns>color of traced object at the intersection point.</returns>
        private Color4 TraceRay(Ray ray, int depth, Color4 noHitColor)
        {
            // normalize here ray because of recursion
            ray.Direction.Normalize();

            // Search for closest intersection
            var closestIntersection = GetClosestIntersection(ray);
            if (closestIntersection.Kind == IntersectionKind.None)
            {
                return noHitColor;
            }

            Shape hitShape = closestIntersection.Shape;

            Vector3d hitPosition = ray.PointAt(closestIntersection.Distance);
            Vector3d hitNormal = closestIntersection.ShapeNormal(hitPosition);
            Vector3d lightDirection = (Light.Position - hitPosition);
            double lightDistance = lightDirection.Length;
            lightDirection.Normalize();
            Vector3d lightReflection = lightDirection.Reflect(hitNormal).Normalized();
            var angleToLight = Vector3d.Dot(hitNormal, lightDirection);
            if (OnlyBoundingBoxes)
            {
                return hitShape.Color(hitPosition, hitNormal).Times((float)angleToLight);
            }

            Color4 finalColor = Light.Color.Times(0.1f);
            Ray shadowRay = new Ray(hitPosition, lightDirection).Shift();
            Ray reflectedRay = new Ray(hitPosition, ray.Direction.Reflect(hitNormal).Normalized()).Shift();

            if (!IsInShadow(shadowRay, lightDistance))
            {
                // diffuse light (default shape color)
                var diffuseColor = Color4Extension.Multiply(hitShape.Color(hitPosition, hitNormal), Light.Color).Times((float)Math.Max(0.0, angleToLight));
                finalColor = finalColor.Add(diffuseColor);

                // specular
                if (hitShape.Shininess > 0)
                {
                    double specularRatio = Vector3d.Dot(lightReflection, ray.Direction);
                    if (specularRatio > 0)
                    {
                        var specularColor = Color4Extension.Multiply(hitShape.ColorSpecular, Light.Color).Times((float)Math.Pow(specularRatio, hitShape.Shininess));
                        finalColor = finalColor.Add(specularColor);
                    }
                }
            }

            // reflected ray (recursion)
            if (hitShape.Reflectance > 0 && depth > 0)
            {
                Color4 reflectedColor = TraceRay(reflectedRay, depth - 1, Color4.Black);

                finalColor = finalColor.Add(Color4Extension.Multiply(reflectedColor, hitShape.Reflectance));
            }

            return finalColor;
        }

        private void Index1d(int x, int y, int width, out int idx)
        {
            idx = y * width + x;
        }

        private void Index2d(int idx, int width, out int x, out int y)
        {
            x = idx % width;
            y = idx / width;
        }

        /// <summary>
        /// Renders image of scene using raytracing.
        /// </summary>
        /// <param name="cancelToken">Cancelation token which can be used to end computation.</param>
        /// <param name="reportFunc">Function to report progress of rendering.</param>
        /// <returns></returns>
        public Bitmap RenderImage(CancellationToken cancelToken, Action<Tuple<int, int>> reportFunc)
        {
            var sourceImg = new Bitmap(_widthInPixels, _heightInPixels, PixelFormat.Format32bppArgb);
            var image = new LockBitmap(sourceImg);
            image.LockBits();
            if (image.ColorComponents != ComponentsPerPixel)
                throw new InvalidOperationException("The bitmap doesn't have 3 components, check system!");

            var totalPixels = _widthInPixels*_heightInPixels;
            var progressLock = new Object();
            var progress = 0;

            var options = new ParallelOptions();
            options.MaxDegreeOfParallelism = NumberOfThreads;

#if PARALLEL
            Parallel.For(0, _rayCache.Count, options, (i, loopState) =>
#else
            for (var i = 0; i < _rayCache.Count; i++)
#endif
            {
                if (cancelToken.IsCancellationRequested)
                {
#if PARALLEL
                    loopState.Stop();
#else
                    break;
#endif
                }

                var ray = _rayCache[i];
                _colorCache[ray.Fragment] = TraceRay(ray, ReflectionDepth, Background);
#if PARALLEL
                lock (progressLock)
                {
#endif
                progress++;
                //reportFunc(new Tuple<int, int>(progress, totalPixels));
#if PARALLEL
                }
            });
#else
            }
#endif
            // Reduce colorCache to antialiased image
            for (int i = 0; i < totalPixels; i++)
            {
                var color = Color4.Black;
                int x, y, idx;
                Index2d(i, _widthInPixels, out x, out y);
                for (var dx = 0; dx < AntialiasFactor; dx++)
                {
                    for (var dy = 0; dy < AntialiasFactor; dy++)
                    {
                        Index1d(x * AntialiasFactor + dx, y * AntialiasFactor + dy, AntialiasedWidth, out idx);
                        color = color.Add(_colorCache[idx]);
                    }
                }
                color = color.Times((float)(1.0 / (AntialiasFactor * AntialiasFactor)));
                image.EfficientSetPixel(i * ComponentsPerPixel, color.ToColor());
            }
            image.UnlockBits();
            return sourceImg;
        }

        /// <summary>
        /// Builds cache for rays so that they don't have to be computed for each rendering.
        /// </summary>
        public void BuildRayCache()
        {
            var rightIncrement = Eye.RightVector*(PixelSize / AntialiasFactor);
            var rightIncrementHalf = rightIncrement*0.5f;
            var downIncrement = Eye.UpVector*-(PixelSize / AntialiasFactor);
            var downIncrementHalf = downIncrement*0.5f;
            var nearPlaneHeightHalf = NearPlaneHeight*0.5f;
            var nearPlaneWidthtHalf = NearPlaneWidth*0.5f;

            // (0,0) in pixels is top left corner
            var firstPixelCenter = (Eye.Position + Eye.UpVector*nearPlaneHeightHalf) +
                                   (Eye.RightVector*-nearPlaneWidthtHalf);

            // Move the plane at NearPlaneDist from eye
            firstPixelCenter += Eye.ViewVector*NearPlaneDist;

            // Move half pixel from top left to its center
            firstPixelCenter += rightIncrementHalf + downIncrementHalf;

            var firstX = 0;
            for (var y = 0; y < AntialiasedHeight; y++)
            {
                var pixelCenter = new Vector3d(firstPixelCenter);
                for (var x = 0; x < AntialiasedWidth; x++)
                {
                    var direction = pixelCenter - Eye.Position;
                    direction.Normalize();

                    var ray = _rayCache[firstX + x];
                    ray.Origin = Eye.Position;
                    ray.Direction = direction;

                    pixelCenter += rightIncrement;
                }

                firstX += AntialiasedWidth;
                firstPixelCenter += downIncrement;
            }
        }
    }
}