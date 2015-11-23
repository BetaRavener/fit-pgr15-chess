using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using RayMath;

namespace CSG.Shapes
{
    /// <summary>
    /// Represents a prametric cylinder.
    /// </summary>
    public class Cylinder : Shape
    {
        public Vector3d Start { get; protected set; }
        public Vector3d Direction { get; protected set; }
        public double Radius { get; protected set; }   
        public double Height { get; set; }

        public Cylinder(Vector3d start, Vector3d dir, double radius, Vector3d color) : base(color)
        {
            Start = start;
            Direction = Vector3d.Normalize(dir);
            Radius = radius;
            Height = 999; // TODO
        }


        /// <summary>
        /// Find set of intersection spans between ray and cylinder.
        /// </summary>
        /// <param name="ray">Tracing ray.</param>
        /// <returns>Intersection spans.</returns>
        public override RangesShape Intersect(Ray ray)
        {
            RangesShape ranges = new RangesShape();
            
            var b = ray.Direction;
            var c = ray.Origin - Start;
            var d = Direction;

            double bd = Vector3d.Dot(b, d);
            double b2 = ray.DirDot;
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
                ranges.Add(new RangeShape(t1, t2, this, this));
            }
            return ranges;
        }

        /// <summary>
        /// Find surface normal for cylinder at specified position.
        /// </summary>
        /// <param name="pos">Position at cylinder surface.</param>
        /// <returns>Surface normal vector.</returns>
        public override Vector3d Normal(Vector3d pos)
        {
            var proj = Start + ((pos - Start) * Direction) * Direction;
            return (pos - proj).Normalized();
        }
    }
}
