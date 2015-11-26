﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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

        public Vector3d LightPosition { get; set; }


        private static Color Background
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
            _camera = new Camera(-100, 400, -400);
            _lightSource = new LightSource(-300, 300, -700);
            _sceneObjects = new List<SceneObject>();

            //var sphere1 = new Sphere(new Vector3d(-30, 0, 0), 40, new Vector3d(0.6,0,0.8));
            //var sphere2 = new Sphere(new Vector3d(10, 0, 0), 60, new Vector3d(0.5,1,0));
            //var csgNode = new CsgNode(CsgNode.Operations.Difference, sphere2, sphere1);
            //var sceneObject = new SceneObject(csgNode, Color4.Azure);

            //_sceneObjects.Add(sceneObject);

            //var box1 = new Box(new Vector3d(-200, 0, -200), new Vector3d(200, 20, 200), new Vector3d(0.5, 0.5, 0));
            //var box2 = new Box(new Vector3d(-90, -10, -90), new Vector3d(90, 30, 90), new Vector3d(0.5, 0.5, 0));
            //csgNode =  new CsgNode(CsgNode.Operations.Difference, box1, box2);
            //sceneObject = new SceneObject(csgNode, Color4.Azure);

            //_sceneObjects.Add(sceneObject);

            //var sphere = new Sphere(new Vector3d(0, 60, 0), 60, new Vector3d(0.2, 0, 0.8));
            //var cylinder1 = new Cylinder(Vector3d.Zero, Vector3d.UnitY, 100, 60, new Vector3d(0.2, 0.8, 0.1));
            //var csgNode1 = new CsgNode(CsgNode.Operations.Union, sphere, cylinder1);
            //var cylinder2 = new Cylinder(new Vector3d(0,40,0), new Vector3d(0, 0.8, -0.2), 40, 100, new Vector3d(0.8, 0.3, 0.3));
            //var csgNode2 = new CsgNode(CsgNode.Operations.Difference, csgNode1, cylinder2);
            //var sceneObject = new SceneObject(csgNode2, Color4.Azure, new BoundingBox(-100, 0, -100, 100, 100, 100));
            ////_sceneObjects.Add(sceneObject);

            //var csgNode3 = new Box(new Vector3d(0, 0, 0), new Vector3d(100, 50, 100), new Vector3d(0.0, 0, 0.0));
            //var csgNode4 = new Box(new Vector3d(100, 0, 100), new Vector3d(200, 50, 200), new Vector3d(0.0, 0, 0.0));
            //var csgNode5 = new Box(new Vector3d(0, 0, 100), new Vector3d(100, 50, 200), new Vector3d(1.0, 1, 1.0));
            //var csgNode6 = new Box(new Vector3d(100, 0, 0), new Vector3d(200, 50, 100), new Vector3d(1.0, 1, 1.0));


            //var csgNodefin =  new CsgNode(CsgNode.Operations.Union, csgNode3, csgNode4);
            //var csgNodefin2 = new CsgNode(CsgNode.Operations.Union, csgNodefin, csgNode5);
            //var csgNodefin3 = new CsgNode(CsgNode.Operations.Union, csgNodefin2, csgNode6);


            //var sceneObject2 = new SceneObject(csgNodefin2, Color4.Azure, new BoundingBox(0, 0, 0, 200, 50, 200));

            var player1 = new Player()
            {
                Color = Color4.Black,
                Name = "Player1"
            };

            var player2 = new Player()
            {
                Color = Color4.White,
                Name = "Player2"
            };

            var figure1 = new Figure()
            {
                Player = player1,
                Type = FigureType.King,
                Position = new ChessboardPosition(0,0)
            };

            var figure2 = new Figure()
            {
                Player = player2,
                Type = FigureType.King,
                Position = new ChessboardPosition(1, 0)
            };

            var game = new Game()
            {
                Chessboard = new Chessboard(),
            };

            game.Figures.Add(figure1);
            game.Figures.Add(figure2);
            game.Start();

            var gameLoader = new GameLoader(@"C:\Users\adamj\Desktop\test");

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
        }


        private Intersection GetClosestIntersection(Ray ray)
        {
            // Search for closest intersection
            var closestIntersection = new Intersection(IntersectionKind.None);

            foreach (SceneObject sceneObject in _sceneObjects)
            {
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
        /// Traces single ray throughout the scene. 
        /// </summary>
        /// <param name="ray">Tracing ray.</param>
        /// <returns>Color of traced object at the intersection point.</returns>
        private Color TraceRay(Ray ray)
        {
            // Search for closest intersection
            var closestIntersection = GetClosestIntersection(ray);

            if (closestIntersection.Kind == IntersectionKind.None)
            {
                return Background;
            }

            var intensity = 0.0;
            var shapeColor = Color4.Black;

            //This is just to allow rendering of bounding boxes which do not have a shape
            if (closestIntersection.Shape != null)
            {
                // We have closest intersection, shoot shadow ray
                var hitPosition = ray.Origin + ray.Direction * closestIntersection.Distance;
                var lightDirection = Light.Position - hitPosition;
                lightDirection.Normalize();

                // Calculate intensity and final pixel color
                var normal = closestIntersection.Shape.Normal(hitPosition);
                // If the intersection is from the inside, inverse normal vector
                if (closestIntersection.Kind == IntersectionKind.Outfrom)
                    normal = -normal;

                intensity = Math.Max(0.0f, Vector3d.Dot(normal, lightDirection));
                shapeColor = closestIntersection.Shape.GetColor(hitPosition, normal);
            }
            else
                intensity = 1.0;

            // unitY is like rGb (0,1,0) color
            Vector3d vectorColor = new Vector3d(shapeColor.B, shapeColor.G, shapeColor.R);
            var color = vectorColor * Light.Color;
            color = color*intensity;

            var pixelColor = Color.FromArgb((int) Math.Round((255.0)*color.X),
                (int) Math.Round((255.0)*color.Y),
                (int) Math.Round((255.0)*color.Z));
            return pixelColor;
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
                image.EfficientSetPixel(ray.Component, color.R, color.G, color.B);
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
            _rayCache.Clear();

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

                    var ray = new Ray(Eye.Position, direction, true, component);

                    _rayCache.Add(ray);
                    pixelCenter += rightIncrement;
                }

                firstX += _widthInPixels;
                firstPixelCenter += downIncrement;
            }
        }
    }
}