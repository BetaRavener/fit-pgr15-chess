using System.Collections.Generic;
using System.Linq;

namespace CSG.Color
{
    // Source: Aurelius
    public class ColorMap
    {
        public class Segment
        {
            double ax, bx;
            CSG.Color.Color ac, bc;

            internal Segment(double l, double r, Color cl, CSG.Color.Color cr)
            { ax = l; bx = r; ac = cl; bc = cr; }
            public bool Covers(double x) => ax <= x && x <= bx;
            public CSG.Color.Color Eval(double x)
            {
                var kb = (x - ax) / (bx - ax);
                var ka = 1 - kb;
                return CSG.Color.Color.Blend(ka, kb, ac, bc);
            }
        };
        private CSG.Color.Color onecolor;
        private bool justone;
        private IList<Segment> cmap = new List<Segment>();

        public ColorMap(CSG.Color.Color c = null)
        {
            onecolor = c ?? new CSG.Color.Color();
            justone = true;
        }
        void Add(float a, float b, CSG.Color.Color ca, CSG.Color.Color cb)
        {
            cmap.Add(new Segment(a, b, ca, cb));
            justone = false;
        }

        public Color GetColor(double x)
        {
            if (!justone)
            {
                foreach (var segment in cmap.Where(segment => segment.Covers(x)))
                {
                    return segment.Eval(x);
                }
            }

            return onecolor;
        }

        public ColorMap AddSegment(Segment s)
        {
            cmap.Add(s);
            justone = false;
            return this;
        }
    }
}
