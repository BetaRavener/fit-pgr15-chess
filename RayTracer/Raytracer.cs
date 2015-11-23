using System;
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
            _camera = new Camera(0, 70, -150);
            _lightSource = new LightSource(0, 100, 5);
            _sceneObjects = new List<SceneObject>();
            _sceneObjects.Add(new SceneObject(
                new List<Shape>(new[]
                {
                    new Cylinder(
                        new Vector3d(0, 0, 100),
                        new Vector3d(0, 10, 0), 20, Vector3d.UnitZ),
                }),
                Color4.Chocolate,
                new BoundingBox(-20, -1000, -20, 20, 1000, 20))
                );

            _sceneObjects.Add(new SceneObject(
                new List<Shape>(new[]
                {
                    new Sphere(new Vector3d(100, 0, 0), 30, Vector3d.UnitY)
                }),
                Color4.Chocolate,
                new BoundingBox(70, -30, -30, 130, 30, 30)
                )
                );

            _sceneObjects.Add(new SceneObject(
                new List<Shape>(new[]
                {
                    new Sphere(new Vector3d(50, 0, 0), 10, Vector3d.UnitY)
                }),
                Color4.Chocolate,
                new BoundingBox(40, -10, -10, 60, 10, 10)
                )
                );

            _sceneObjects.Add(new SceneObject(
                new List<Shape>(new [] {
                    new Plane(
                        new Vector3d(0, 1, 0), 
                        new Vector3d(0, -100, 0),
                        Vector3d.UnitX
                        )
                }),
                Color4.AliceBlue
                ));

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
            var closestIntersection = new Intersection(Intersection.IntersectionKind.None);

            foreach (SceneObject sceneObject in _sceneObjects)
            {
                var intersection = sceneObject.IntersectFirst(ray);
                if (intersection.Kind != Intersection.IntersectionKind.None &&
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

            if (closestIntersection.Kind == Intersection.IntersectionKind.None)
            {
                return Background;
            }

            var intensity = 0.0;

            if (closestIntersection.Shape != null)
            {
                // We have closest intersection, shoot shadow ray
                var hitPosition = ray.Origin + ray.Direction*closestIntersection.Distance;
                var lightDirection = Light.Position - hitPosition;
                lightDirection.Normalize();

                // Calculate intensity and final pixel color
                var normal = closestIntersection.Shape.Normal(hitPosition);
                intensity = Math.Max(0.0f, Vector3d.Dot(normal, lightDirection));
            }
            else
                intensity = 1.0;

            // unitY is like rGb (0,1,0) color
            var color =  closestIntersection.Shape.Color * Light.Color;
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