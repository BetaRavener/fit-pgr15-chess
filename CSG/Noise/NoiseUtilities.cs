using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace CSG.Noise
{

    public class NoiseUtilities
    {
        private const double CR00 = -0.5;
        private const double CR01 = 1.5;
        private const double CR02 = -1.5;
        private const double CR03 = 0.5;
        private const double CR10 = 1.0;
        private const double CR11 = -2.5;
        private const double CR12 = 2.0;
        private const double CR13 = -0.5;
        private const double CR20 = -0.5;
        private const double CR21 = 0.0;
        private const double CR22 = 0.5;
        private const double CR23 = 0.0;
        private const double CR30 = 0.0;
        private const double CR31 = 1.0;
        private const double CR32 = 0.0;
        private const double CR33 = 0.0;

        public static double Fade(double t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }
        public static double Lerp(double t, double a, double b)
        {
            return a + t * (b - a);
        }
        public static double Grad(int hash, double x, double y, double z)
        {
            int h = hash & 15;       // CONVERT LO 4 BITS OF HASH CODE
            double u = h < 8 ? x : y;   // INTO 12 GRADIENT DIRECTIONS.
            double v = h < 4 ? y : h == 12 || h == 14 ? x : z;
            return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
        }
        public static double Clamp(double x, double a, double b)
        {
            return (x < a ? a : (x > b ? b : x));
        }

        public static double Spline(double x, int nknots, double[] knot)
        {
            int nspans = nknots - 3;
            double c0, c1, c2, c3;   /* coefficients of the cubic.*/

            if (nspans < 1)
            {  /* illegal */
                return 0;
            }

            /* Find the appropriate 4-point span of the spline. */
            x = Clamp(x, 0, 1) * nspans;
            int span = (int)x;
            if (span >= nknots - 3) span = nknots - 3;
            x -= span;

            /* Evaluate the span cubic at x using Horner's rule. */
            c3 = CR00 * knot[span + 0] + CR01 * knot[span + 1] + CR02 * knot[span + 2] + CR03 * knot[span + 3];
            c2 = CR10 * knot[span + 0] + CR11 * knot[span + 1] + CR12 * knot[span + 2] + CR13 * knot[span + 3];
            c1 = CR20 * knot[span + 0] + CR21 * knot[span + 1] + CR22 * knot[span + 2] + CR23 * knot[span + 3];
            c0 = CR30 * knot[span + 0] + CR31 * knot[span + 1] + CR32 * knot[span + 2] + CR33 * knot[span + 3];

            return ((c3 * x + c2) * x + c1) * x + c0;
        }


    }
}
