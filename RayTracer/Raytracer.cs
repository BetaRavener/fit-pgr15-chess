﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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

        private static Vector3d Background
        {
            get { return Utils.ColorToVector(Color4.MidnightBlue); }
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
                    Color = Utils.ColorToVector(Color4.Black),
                    Name = "Player1"
                },
                Player2 = new Player()
                {
                    Color = Utils.ColorToVector(Color4.White),
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

            // for should be faster than foreach/linq
            for (int index = 0; index < _sceneObjects.Count; index++)
            {
                SceneObject sceneObject = _sceneObjects[index];
                var intersection = sceneObject.IntersectFirst(ray);
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
            // for should be faster than foreach/linq
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
        /// <returns>color of traced object at the intersection point.</returns>
        private Vector3d TraceRay(Ray ray, int depth = 2)
        {
            // Search for closest intersection
            var closestIntersection = GetClosestIntersection(ray);
            if (closestIntersection.Kind == IntersectionKind.None) return Background;

            ray.Direction.Normalize();

            Shape hitShape = closestIntersection.Shape;

            Vector3d hitPosition = ray.PointAt(closestIntersection.Distance);
            Vector3d hitNormal = closestIntersection.ShapeNormal(hitPosition);

            Vector3d finalColor = hitShape.ColorAmbient; // TODO should use ambient color? or just black
            Vector3d lightDirection = (Light.Position - hitPosition).Normalized();

            Ray shadowRay = new Ray(hitPosition, lightDirection).Shift();
            var angleToLight = Vector3d.Dot(hitNormal, lightDirection);
            var tmp = 2*angleToLight*hitNormal;

            if (!IsInShadow(shadowRay))
            {
                // diffuse light (default shape color)
                finalColor += hitShape.Color(hitPosition, hitNormal)*Light.Color*Math.Max(0.0, angleToLight);

                // specular
                if (hitShape.Shininess > 0)
                {
                    Vector3d reflectionDirection = tmp - lightDirection;
                    double specularRatio = -Vector3d.Dot(reflectionDirection, ray.Direction);
                    if (specularRatio > 0)
                    {
                        finalColor += hitShape.ColorSpecular*Light.Color*Math.Pow(specularRatio, hitShape.Shininess);
                    }
                }
            }

            // reflected ray (recursion)
            if (hitShape.Reflectance > 0 && depth > 0)
            {
                var reflectionDirection = ray.Direction - tmp;
                Ray reflectanceRay = new Ray(hitPosition, reflectionDirection).Shift();
                Vector3d reflectedColor = TraceRay(reflectanceRay, depth - 1);
                finalColor += reflectedColor*hitShape.Reflectance;
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
                var colorVector = TraceRay(ray);

                var r = (int) Math.Round((255.0)*colorVector.X);
                var g = (int) Math.Round((255.0)*colorVector.Y);
                var b = (int) Math.Round((255.0)*colorVector.Z);

                // TODO better checking for overflow/underflow!
                if (r > 255) r = 255;
                if (g > 255) g = 255;
                if (b > 255) b = 255;

                if (r < 0) r = 0;
                if (g < 0) g = 0;
                if (b < 0) b = 0;

                var pixelColor = Color.FromArgb(r, g, b);
#if PARALLEL
                lock (progressLock)
                {
#endif
                image.EfficientSetPixel(ray.Component, pixelColor);
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