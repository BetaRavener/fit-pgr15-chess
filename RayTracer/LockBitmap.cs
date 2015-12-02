using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace RayTracer
{
    /// <summary>
    ///     Class designed for working with bitmaps efficiently
    ///     <Author>Vano Maisuradze</Author>
    ///     <Link>http://www.codeproject.com/Tips/240428/Work-with-bitmap-faster-with-Csharp</Link>
    /// </summary>
    public class LockBitmap
    {
        private byte[] _pixels;
        private BitmapData bitmapData;
        private IntPtr Iptr = IntPtr.Zero;
        private readonly Bitmap source;

        public LockBitmap(Bitmap source)
        {
            this.source = source;
        }

        public int Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int ColorComponents { get; private set; }

        /// <summary>
        ///     Lock bitmap data
        /// </summary>
        public void LockBits()
        {
            try
            {
                // Get width and height of bitmap
                Width = source.Width;
                Height = source.Height;

                // get total locked pixels count
                var pixelCount = Width*Height;

                // Create rectangle to lock
                var rect = new Rectangle(0, 0, Width, Height);

                // get source bitmap pixel format size
                Depth = Image.GetPixelFormatSize(source.PixelFormat);
                ColorComponents = Depth/8;

                // Check if bpp (Bits Per Pixel) is 8, 24, or 32
                if (Depth != 8 && Depth != 24 && Depth != 32)
                {
                    throw new ArgumentException("Only 8, 24 and 32 bpp images are supported.");
                }

                // Lock bitmap and return bitmap data
                bitmapData = source.LockBits(rect, ImageLockMode.ReadWrite,
                    source.PixelFormat);

                // create byte array to copy pixel values
                _pixels = new byte[pixelCount*ColorComponents];
                Iptr = bitmapData.Scan0;

                // Copy data from pointer to array
                Marshal.Copy(Iptr, _pixels, 0, _pixels.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///     Unlock bitmap data
        /// </summary>
        public void UnlockBits()
        {
            try
            {
                // Copy data from byte array to pointer
                Marshal.Copy(_pixels, 0, Iptr, _pixels.Length);

                // Unlock bitmap data
                source.UnlockBits(bitmapData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///     Get the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color GetPixel(int x, int y)
        {
            var clr = Color.Empty;

            // Get start index of the specified pixel
            var i = (y*Width + x)*ColorComponents;

            if (Depth == 32) // For 32 bpp get Red, Green, Blue and Alpha
            {
                var b = _pixels[i];
                var g = _pixels[i + 1];
                var r = _pixels[i + 2];
                var a = _pixels[i + 3]; // a
                clr = Color.FromArgb(a, r, g, b);
            }
            if (Depth == 24) // For 24 bpp get Red, Green and Blue
            {
                var b = _pixels[i];
                var g = _pixels[i + 1];
                var r = _pixels[i + 2];
                clr = Color.FromArgb(r, g, b);
            }
            if (Depth == 8)
                // For 8 bpp get color value (Red, Green and Blue values are the same)
            {
                var c = _pixels[i];
                clr = Color.FromArgb(c, c, c);
            }
            return clr;
        }

        /// <summary>
        ///     Set the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void SetPixel(int x, int y, Color color)
        {
            // Get start index of the specified pixel
            var i = (y*Width + x)*ColorComponents;

            if (Depth == 32) // For 32 bpp set Red, Green, Blue and Alpha
            {
                _pixels[i] = color.B;
                _pixels[i + 1] = color.G;
                _pixels[i + 2] = color.R;
                _pixels[i + 3] = color.A;
            }
            if (Depth == 24) // For 24 bpp set Red, Green and Blue
            {
                _pixels[i] = color.B;
                _pixels[i + 1] = color.G;
                _pixels[i + 2] = color.R;
            }
            if (Depth == 8)
                // For 8 bpp set color value (Red, Green and Blue values are the same)
            {
                _pixels[i] = color.B;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EfficientSetPixel(int idx, byte r, byte g, byte b)
        {
            _pixels[idx] = b;
            _pixels[idx + 1] = g;
            _pixels[idx + 2] = r;
            _pixels[idx + 3] = 255;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EfficientSetPixel(int idx, Color color)
        {
            _pixels[idx] = color.B;
            _pixels[idx + 1] = color.G;
            _pixels[idx + 2] = color.R;
            _pixels[idx + 3] = 255;
        }
    }
}