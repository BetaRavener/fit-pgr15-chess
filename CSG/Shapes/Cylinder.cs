using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using RayMath;

namespace CSG.Shapes
{
    public class Cylinder : Shape
    {
        public Vector3d Start { get; protected set; }
        public Vector3d Direction { get; protected set; }
        public double Radius { get; protected set; }

        public Cylinder(Vector3d start, Vector3d dir, double radius)
        {
            Start = start;
            Direction = Vector3d.Normalize(dir);
            Radius = radius;
        }

        public override Ranges<Shape> Intersect(Ray ray)
        {
            Ranges<Shape> ranges = new Ranges<Shape>();
            
            var b = ray.Direction;
            var c = ray.Origin - Start;
            var d = Direction;

            double bd = Vector3d.Dot(b, d);
            double b2 = Vector3d.Dot(b, b);
            double cb = Vector3d.Dot(c, b);
            double cd = Vector3d.Dot(c, d);
            double c2 = Vector3d.Dot(c, c);

            double A = b2 - bd * bd;
            double B = 2 * cb - 2 * cd * bd;
            double C = c2 - cd * cd - Radius * Radius;

            double D = B * B - 4 * A * C;

            if (D > 0)
            {
                double sD = Math.Sqrt(D);
                double t1 = (-B - sD) / (2 * A);
                double t2 = (-B + sD) / (2 * A);
                ranges.Add(new Range<Shape>(t1, t2, this, this));
            }
            return ranges;
        }

        public override Vector3d Normal(Vector3d pos)
        {
            var proj = Start + ((pos - Start) * Direction) * Direction;
            return (pos - proj).Normalized();
        }
    }
}
