using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using OpenTK.Graphics;

namespace RayTracer
{
    public static class Color4Extension
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color ToColor(this Color4 left)
        {
            var r = (byte)Math.Floor(left.R >= 1.0 ? 255 : left.R * 256);
            var g = (byte)Math.Floor(left.G >= 1.0 ? 255 : left.G * 256);
            var b = (byte)Math.Floor(left.B >= 1.0 ? 255 : left.B * 256);

            return Color.FromArgb(r, g, b);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color4 Times(this Color4 left, Color4 right)
        {
            left.R *= right.R;
            left.G *= right.G;
            left.B *= right.B;

            return left;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color4 Times(this Color4 left, float scale)
        {
            left.R *= scale;
            left.G *= scale;
            left.B *= scale;

            return left;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color4 Add(this Color4 left, Color4 right)
        {
            left.R += right.R;
            left.G += right.G;
            left.B += right.B;

            return left;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color4 Add(this Color4 left, float scale)
        {
            left.R += scale;
            left.G += scale;
            left.B += scale;

            return left;
        }

        /// <summary>
        /// Multiplies 2 colors
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color4 Multiply(Color4 left, Color4 right)
        {
            return new Color4
            {
                R = left.R * right.R,
                G = left.G * right.G,
                B = left.B * right.B
            };
        }

        public static Color4 Multiply(Color4 left, double scale)
        {
            return new Color4
            {
                R = (float)(left.R * scale),
                G = (float)(left.G * scale),
                B = (float)(left.B * scale)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color4 Sum(Color4 left, Color4 right)
        {
            return new Color4
            {
                R = left.R + right.R,
                G = left.G + right.G,
                B = left.B + right.B
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Color4 Sum(Color4 left, double scale)
        {
            return new Color4
            {
                R = (float) (left.R + scale),
                G = (float) (left.G + scale),
                B = (float) (left.B + scale)
            };
        }
    }
}