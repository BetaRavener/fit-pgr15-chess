using System;
using System.Runtime.Remoting.Messaging;
using OpenTK.Graphics;

namespace CSG.Color
{
    // Source: Aurelius
    public class Color
    {
        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }
        public Color() { Red = 0; Green = 0; Blue = 0; }
        public Color(double red, double green, double blue)
        { Red = red; Green = green; Blue = blue; }

        public Color(Color4 color)
        {
            Red = color.R;
            Green = color.G;
            Blue = color.B;
        }

        public void Accumulate(Color x, double scale = 1.0)
        {
            Red += scale * x.Red; Green += scale * x.Green; Blue += scale * x.Blue;
        }

        public void Mult(double scale = 1.0)
        {
            Red *= scale;
            Green *= scale;
            Blue *= scale;
        }

        public Color Times(float scale)
        {
 

            return new Color(Red * scale, Green * scale, Blue * scale) ;
        }

        public static Color Blend(double ka, double kb, Color a, Color b)
        {
            return new Color(ka * a.Red + kb * b.Red, ka * a.Green + kb * b.Green, ka * a.Blue + kb * b.Blue);
        }
        //Color &operator *= (float x) { red *= x; green *= x; blue *= x; return *this; }

        public Color4 ToColor4()
        {
            return new Color4((float)Red, (float)Green, (float)Blue, (float)0.0);
        }

        public static Color Mult(Color a, Color b)
        {
            return new Color(a.Red * b.Red, a.Green * b.Green, a.Blue * b.Blue);
        }
        public static double Diff(Color a, Color b)
        {
            var rr = a.Red - b.Red;
            var gg = a.Green - b.Green;
            var bb = a.Blue - b.Blue;
            return rr * rr + gg * gg + bb * bb;
        }
    }
}
