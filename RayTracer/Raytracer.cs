using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using CSG;
using CSG.Shapes;
using MathNet.Numerics.Distributions;
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

        public int NumberOfThreads { get; set; }

        public static Vector3d ColorToVec(Color color)
        {
            return new Vector3d
            {
                X = color.R / 255.0,
                Y = color.G / 255.0,
                Z = color.B / 255.0,
            };
        }

        public Raytracer()
        {
            _heightInPixels = 0;
            _widthInPixels = 0;
            _camera = new Camera(-100, 200, -400);
            _lightSource = new LightSource(-300, 300, -700);
            _sceneObjects = new List<SceneObject>();
          
            var game = new Game
            {
                Chessboard = new Chessboard(),
                Player1 = new Player()
                {
                    Color = Color4.Black,
                    Name = "Player1"
                },
                Player2 = new Player()
                {
                    Color = Color4.White,
                    Name = "Player2"
                },
            };

            game.Player1.CreateFigure(new ChessboardPosition(4, 3), FigureType.King);
            game.Player2.CreateFigure(new ChessboardPosition(4, 4), FigureType.King);
            game.Start();

            var gameLoader = new GameLoader(@".");

            gameLoader.SaveGame(game, "test.txt");

            var loadedGame = gameLoader.LoadGame("test.txt");

            _sceneObjects.AddRange(loadedGame.GetSceneObjects());

            // debug
            //var y = new CsgNode();
            //var obj = new SceneObject(y, Color4.Green);
            //y.Operation = CsgNode.Operations.Union;
            //y.Left = new Sphere(new ChessboardPosition(4, 4).RealPosition + new Vector3d(0, 55, 0), 50, obj);
            //y.Right = obj.CsgTree.Left;

            //_sceneObjects.Add(obj);

            //var x = new CsgNode();
            //var obj2 = new SceneObject(x, Color4.Red);
            //x.Operation = CsgNode.Operations.Union;
            //x.Left = new Box(new ChessboardPosition(1, 2).RealPosition, new ChessboardPosition(2, 3).RealPosition + new Vector3d(0, 50, 0), obj2);
            //x.Right = obj.CsgTree.Left;

            //_sceneObjects.Add(obj2);


            _rayCache = new List<Ray>();

            NumberOfThreads = 1;
        }

        public void Resize(int widthInPixels, int heightInPixels)
        {
            _heightInPixels = heightInPixels;
            _widthInPixels = widthInPixels;

            _rayCache.Clear();
            var firstX = 0;
            for (var y = 0; y < _heightInPixels; y++)
            {
                for (var x = 0; x < _widthInPixels; x++)
                {
                    var component = (firstX + x)*ComponentsPerPixel;

                    var ray = new Ray(Eye.Position, Vector3d.UnitZ, component);

                    _rayCache.Add(ray);
        }
                firstX += _widthInPixels;
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
        /// <returns></returns>
        private bool IsInShadow(Ray ray)
        {
            for (int index = 0; index < _sceneObjects.Count; index++)
            {
                var sceneObject = _sceneObjects[index];
                if (sceneObject.IntersectFirst(ray).Kind != IntersectionKind.None)
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
        /// <returns>ColorAmbient of traced object at the intersection point.</returns>
        private Color4 TraceRay(Ray ray, int depth = 2)
        {
            // normalize here ray because of recursion
            ray.Direction.Normalize();

            // Search for closest intersection
            var closestIntersection = GetClosestIntersection(ray);
            if (closestIntersection.Kind == IntersectionKind.None) return Background;

            Shape hitShape = closestIntersection.Shape;

            Vector3d hitPosition = ray.PointAt(closestIntersection.Distance);
            Vector3d hitNormal = closestIntersection.ShapeNormal(hitPosition);

            Color4 finalColor = Color.Black;
            Vector3d lightDirection = (Light.Position - hitPosition).Normalized();

            Ray shadowRay = new Ray(hitPosition, lightDirection).Shift();
            var angleToLight = Vector3d.Dot(hitNormal, lightDirection);
            var tmp = 2*angleToLight*hitNormal;

            if (!IsInShadow(shadowRay))
            {
                // diffuse light (default shape color)
                //finalColor += hitShape.Color * Light.Color * Math.Max(0.0, angleToLight);
                var diffuseColor = Color4Extension.Multiply(hitShape.Color(hitPosition,hitNormal), Light.Color).Times((float) Math.Max(0.0, angleToLight));
               finalColor = finalColor.Add(diffuseColor);

               // specular
                if (hitShape.Shininess > 0)
                {
                    Vector3d reflectionDirection = tmp - lightDirection;
                    double specularRatio = -Vector3d.Dot(reflectionDirection, ray.Direction);
                    if (specularRatio > 0)
                    {
                        var specularColor = Color4Extension.Multiply(hitShape.ColorSpecular, Light.Color).Times((float) Math.Pow(specularRatio, hitShape.Shininess));
                        finalColor = finalColor.Add(specularColor);
                        //finalColor += hitShape.ColorSpecular*Light.Color*Math.Pow(specularRatio, hitShape.Shininess);
                    }
                }
            }

            // reflected ray (recursion)
            if (hitShape.Reflectance > 0 && depth > 0)
            {
                var reflectionDirection = ray.Direction - tmp;
                Ray reflectanceRay = new Ray(hitPosition, reflectionDirection).Shift();
                Color4 reflectedColor = TraceRay(reflectanceRay, depth - 1);

                finalColor = finalColor.Add(Color4Extension.Multiply(reflectedColor, hitShape.Reflectance));
                //finalColor += reflectedColor * hitShape.Reflectance;
            }

            return finalColor;
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

            //TODO: Antialiasing - Cast 4 rays for each pixel and save average color 

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
                var color = TraceRay(ray);
#if PARALLEL
                lock (progressLock)
                {
#endif
                image.EfficientSetPixel(ray.Component, color.ToColor());
                progress++;
                //reportFunc(new Tuple<int, int>(progress, totalPixels));

#if PARALLEL
                }
            });
#else
            }
#endif
            image.UnlockBits();
            return sourceImg;
        }

        /// <summary>
        /// Builds cache for rays so that they don't have to be computed for each rendering.
        /// </summary>
        public void BuildRayCache()
        {
            var rightIncrement = Eye.RightVector*PixelSize;
            var rightIncrementHalf = rightIncrement*0.5f;
            var downIncrement = Eye.UpVector*-PixelSize;
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
            for (var y = 0; y < _heightInPixels; y++)
            {
                var pixelCenter = new Vector3d(firstPixelCenter);
                for (var x = 0; x < _widthInPixels; x++)
                {
                    var component = (firstX + x)*ComponentsPerPixel;
                    var direction = pixelCenter - Eye.Position;
                    direction.Normalize();

                    var ray = _rayCache[firstX + x];
                    ray.Origin = Eye.Position;
                    ray.Direction = direction;

                    pixelCenter += rightIncrement;
                }

                firstX += _widthInPixels;
                firstPixelCenter += downIncrement;
            }
        }
    }
}