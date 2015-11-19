using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Math3d;
using OpenTK;
using RayTracer;

namespace Raytracer
{
    public class Raytracer
    {
        private const float NearPlaneDist = 1.0f;
        private const float FovY = (float)Math.PI/4.0f;

        private int _heightInPixels;
        private int _widthInPixels;

        private LightSource _lightSource;
        private Camera _camera;
        private List<SceneObject> _sceneObjects;

        public Vector3 LightPosition {get; set; }


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

        float Ratio
        {
            get { return (float)_widthInPixels / (float)_heightInPixels; }
        }

        private float NearPlaneHeight
        {
            get { return 2 * (float)Math.Tan((double)FovY / 2.0) * NearPlaneDist; }
        }

        private float NearPlaneWidth
        {
            get { return Ratio*NearPlaneHeight; }
        }

        private float PixelSize
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
            _sceneObjects = new List<SceneObject> {SceneObject.LoadFromFile("models/teapot")};            

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
            float minDistance = float.MaxValue;
            SceneObject minSceneObject = null;
            Triangle minTriangle = new Triangle();

            foreach (var sceneObject in _sceneObjects)
            {
                float distance;
                if (!ray.Intersects(sceneObject.BoundingBox, out distance)) continue;

                foreach (var triangle in sceneObject.Triangles)
                {
                    if (!ray.Intersects(triangle, out distance)) continue;
                    if (!(distance < minDistance)) continue;

                    minDistance = distance;
                    minSceneObject = sceneObject;
                    minTriangle = triangle;
                }
            }

            if (minSceneObject == null)
                return Background;

            // We have closest intersection, shoot shadow ray
            var hitPosition = Vector3.Add(ray.Origin, Vector3.Multiply(ray.Direction, minDistance));
            var lightDirection = Vector3.Subtract(Light.Position, hitPosition);
            lightDirection.Normalize();

            // Calculate intensity and final pixel color
            var intensity = Math.Max(0.0f, Vector3.Dot(minTriangle.Normal, lightDirection));
            var color = Vector3.Multiply(new Vector3(0.5f, 1.0f, 0.0f), Light.Color);
            color = Vector3.Multiply(color, intensity);

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

            var rightIncrement = Vector3.Multiply(Eye.RightVector, PixelSize);
            var rightIncrementHalf = Vector3.Multiply(rightIncrement, 0.5f);
            var downIncrement = Vector3.Multiply(Eye.UpVector, -PixelSize);
            var downIncrementHalf = Vector3.Multiply(downIncrement, 0.5f);
            var nearPlaneHeightHalf = NearPlaneHeight*0.5f;
            var nearPlaneWidthtHalf = NearPlaneWidth*0.5f;

            // (0,0) in pixels is top left corner
            var firstPixelCenter =
                Vector3.Add(Vector3.Add(Eye.Position, Vector3.Multiply(Eye.UpVector, nearPlaneHeightHalf)),
                    Vector3.Multiply(Eye.RightVector, -nearPlaneWidthtHalf));

            // Move the plane at NearPlaneDist from eye
            firstPixelCenter = Vector3.Add(firstPixelCenter, Vector3.Multiply(Eye.ViewVector, NearPlaneDist));

            // Move half pixel from top left to its center
            firstPixelCenter = Vector3.Add(Vector3.Add(firstPixelCenter, rightIncrementHalf), downIncrementHalf);

            //TODO: Possible optimization - overload Add operation to add another vector to already
            //TODO: existing instance instead of creating new one

            //TODO: Possible optimization - generate rays into list, precompute values like dirfrac and use this list
            //TODO: for next renering.. The list only needs to change if camera has changed

            var totalPixels = _widthInPixels*_heightInPixels;
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
                var x = i%_widthInPixels;
                var y = i/_widthInPixels;
                var component = i*componentsPerPixel;

                var pixelCenter = new Vector3(firstPixelCenter);
                pixelCenter = Vector3.Add(pixelCenter, Vector3.Multiply(rightIncrement, x));
                pixelCenter = Vector3.Add(pixelCenter, Vector3.Multiply(downIncrement, y));
                var direction = Vector3.Subtract(pixelCenter, Eye.Position);
                direction.Normalize();
                var ray = new Ray(Eye.Position, direction);

                var color = TraceRay(ray);

#if PARALLEL
                lock (progressLock)
                {
#endif
                    image.EfficientSetPixel(component, color.R, color.G, color.B);
                    progress++;
                    reportFunc(new Tuple<int, int>(progress, totalPixels));

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
