using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RayMath;
using OpenTK;
using RayTracer;
using CSG.Shapes;

namespace Raytracer
{
    public class Raytracer
    {
        private const double NearPlaneDist = 1.0f;
        private const double FovY = (double)Math.PI/4.0f;

        private int _heightInPixels;
        private int _widthInPixels;

        private LightSource _lightSource;
        private Camera _camera;
        private List<Shape> _sceneShapes;

        public Vector3d LightPosition {get; set; }


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
            get { return _lightSource;}
            set { _lightSource = value; }
        }

        double Ratio
        {
            get { return (double)_widthInPixels / (double)_heightInPixels; }
        }

        private double NearPlaneHeight
        {
            get { return 2 * (double)Math.Tan((double)FovY / 2.0) * NearPlaneDist; }
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
            _sceneShapes = new List<Shape>();
            _sceneShapes.Add(new Cylinder(new Vector3d(0,0,100), new Vector3d(0,10,0), 20));
            _sceneShapes.Add(new Sphere(new Vector3d(100, 0, 0), 30));

            NumberOfThreads = 1;
        }

        public void Resize(int widthInPixels, int heightInPixels)
        {
            _heightInPixels = heightInPixels;
            _widthInPixels = widthInPixels;
        }

        private Color TraceRay(Ray ray)
        {
            // Search for closest intersection
            var closestIntersection = new CSG.Intersection(CSG.Intersection.IntersectionKind.None);
            
            foreach (var shape in _sceneShapes)
            {
                var intersection = shape.IntersectFirst(ray);
                if (intersection.Kind != CSG.Intersection.IntersectionKind.None && intersection.Distance < closestIntersection.Distance)
                    closestIntersection = intersection;
            }

            if (closestIntersection.Kind == CSG.Intersection.IntersectionKind.None)
                return Background;

            // We have closest intersection, shoot shadow ray
            var hitPosition = ray.Origin + ray.Direction * closestIntersection.Distance;
            var lightDirection = Light.Position - hitPosition;
            lightDirection.Normalize();

            // Calculate intensity and final pixel color
            var normal = closestIntersection.Shape.Normal(hitPosition);
            var intensity = Math.Max(0.0f, Vector3d.Dot(normal, lightDirection));
            var color = new Vector3d(0.5f, 1.0f, 0.0f) * Light.Color;
            color = color * intensity;

            var pixelColor = Color.FromArgb((int)Math.Round((255.0) * color.X),
                                            (int)Math.Round((255.0) * color.Y),
                                            (int)Math.Round((255.0) * color.Z));
            return pixelColor;
        }

        public Bitmap RenderImage(CancellationToken cancelToken, Action<Tuple<int, int>> reportFunc)
        {
            const int componentsPerPixel = 4;
            var sourceImg = new Bitmap(_widthInPixels, _heightInPixels, PixelFormat.Format32bppArgb);
            var image = new LockBitmap(sourceImg);
            image.LockBits();
            if (image.ColorComponents != componentsPerPixel)
                throw new InvalidOperationException("The bitmap doesn't have 3 components, check system!");

            var rightIncrement = Eye.RightVector * PixelSize;
            var rightIncrementHalf = rightIncrement * 0.5f;
            var downIncrement = Eye.UpVector * -PixelSize;
            var downIncrementHalf = downIncrement * 0.5f;
            var nearPlaneHeightHalf = NearPlaneHeight * 0.5f;
            var nearPlaneWidthtHalf = NearPlaneWidth * 0.5f;

            // (0,0) in pixels is top left corner
            var firstPixelCenter = (Eye.Position + Eye.UpVector * nearPlaneHeightHalf) + (Eye.RightVector * -nearPlaneWidthtHalf);

            // Move the plane at NearPlaneDist from eye
            firstPixelCenter = firstPixelCenter + Eye.ViewVector * NearPlaneDist;

            // Move half pixel from top left to its center
            firstPixelCenter = firstPixelCenter + rightIncrementHalf + downIncrementHalf;

            //TODO: Antialiasing - Cast 4 rays for each pixel and save average color 

            //TODO: Possible optimization - overload Add operation to add another vector to already
            //TODO: existing instance instead of creating new one

            //TODO: Possible optimization - generate rays into list, precompute values like dirfrac and use this list
            //TODO: for next renering.. The list only needs to change if camera has changed

            var totalPixels = _widthInPixels * _heightInPixels;
            var progressLock = new Object();
            var progress = 0;

            var options = new ParallelOptions();
            options.MaxDegreeOfParallelism = NumberOfThreads;

#if PARALLEL
            Parallel.For(0, totalPixels, options, (i, loopState) =>
#else
            for (var i = 0; i < totalPixels; i++)
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
                var x = i % _widthInPixels;
                var y = i / _widthInPixels;
                var component = i * componentsPerPixel;

                var pixelCenter = firstPixelCenter + rightIncrement * x + downIncrement * y;
                var direction = pixelCenter - Eye.Position;

                var ray = new Ray(Eye.Position, direction);

                var color = TraceRay(ray);

#if PARALLEL
                lock (progressLock)
                {
#endif
                    image.EfficientSetPixel(component, color.R, color.G, color.B);
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
    }
}
