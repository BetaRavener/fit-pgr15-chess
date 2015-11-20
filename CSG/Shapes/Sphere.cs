using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using RayMath;

namespace CSG.Shapes
{
    public class Sphere : Shape
    {
        public Vector3d Center { get; protected set; }
        public double Radius { get; protected set; }

        public Sphere(Vector3d center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public override Intersection IntersectFirst(Ray ray)
        {
            double aa = Vector3d.Dot(ray.Direction, ray.Direction);
            double bb = 2 * Vector3d.Dot(ray.Direction, (ray.Origin - Center));
            double cc = Vector3d.Dot((ray.Origin - Center), (ray.Origin - Center)) - Radius * Radius;
            double D = bb * bb - 4 * aa * cc;
            if (D > 0)
            {
                double sD = Math.Sqrt(D);
                double t1 = (-bb - sD) / (2 * aa);
                if (t1 > 0) return new Intersection(Intersection.IntersectionKind.Into, this, t1);
                double t2 = (-bb + sD) / (2 * aa);
                if (t2 > 0) return new Intersection(Intersection.IntersectionKind.Outfrom, this, t2);
            }
            return new Intersection(Intersection.IntersectionKind.None);
        }

        public override Ranges<Shape> Intersect(Ray ray)
        {
            Ranges<Shape> ranges = new Ranges<Shape>();

            double aa = Vector3d.Dot(ray.Direction, ray.Direction);
            double bb = 2 * Vector3d.Dot(ray.Direction, (ray.Origin - Center));
            double cc = Vector3d.Dot((ray.Origin - Center), (ray.Origin - Center)) - Radius * Radius;
            double D = bb * bb - 4 * aa * cc;
            if (D > 0)
            {
                double sD = Math.Sqrt(D);
                double t2 = (-bb + sD) / (2 * aa);
                double t1 = (-bb - sD) / (2 * aa);
                ranges.Add(new Range<Shape>(t1, t2, this, this));
            }
            return ranges;
        }

        public override Vector3d Normal(Vector3d pos)
        {
            return Vector3d.Normalize(pos - Center);
        }
    }
}
