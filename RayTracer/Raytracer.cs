using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using RayTracer;

namespace Raytracer
{
    public class Raytracer
    {
        private const double NearPlaneDistZ = 1.0;
        private const double FovY = Math.PI/4.0;

        private int _heightInPixels;
        private int _widthInPixels;
        private double _pixelSize;

        double Ratio
        {
            get { return (double)_widthInPixels / (double)_heightInPixels; }
        }

        private double NearPlaneHeight
        {
            get { return 2 * Math.Tan(FovY / 2.0) * NearPlaneDistZ; }
        }

        private double NearPlaneWidth
        {
            get { return Ratio*NearPlaneHeight; }
        }

        private double PixelSize
        {
            get { return (1.0/_heightInPixels)*NearPlaneHeight; }
        }


        public Raytracer()
        {
            _heightInPixels = 0;
            _widthInPixels = 0;
        }

        public void Resize(int widthInPixels, int heightInPixels)
        {
            _heightInPixels = heightInPixels;
            _widthInPixels = widthInPixels;
        }

        public Bitmap RenderImage()
        {
            const int componentsPerPixel = 4;
            var sourceImg = new Bitmap(_widthInPixels, _heightInPixels, PixelFormat.Format32bppArgb);
            var image = new LockBitmap(sourceImg);
            image.LockBits();
            if (image.ColorComponents != componentsPerPixel)
                throw new InvalidOperationException("The bitmap doesn't have 3 components, check system!");
            
            var totalComponents = _widthInPixels * _heightInPixels * componentsPerPixel;
            for (var i = 0; i < totalComponents; i+= componentsPerPixel)
            {
                image.EfficientSetPixel(i, 127, 255, 0);
            }
            image.UnlockBits();
            return sourceImg;
        }
    }
}
