using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using RayMath;

namespace CSG.Shapes
{
    public class Box : Shape
    {
        public Vector3d Min { get; protected set; }
        public Vector3d Max { get; protected set; }

        public Box(Vector3d min, Vector3d max, Vector3d color) :
            base(color)
        {
            Min = min;
            Max = max;
        }

        public override RangesShape Intersect(Ray ray)
        {
            var origin = ray.Origin;
            var dirfrac = ray.Dirfrac;
            RangesShape ranges = new RangesShape();

            // lb is the corner of AABB with minimal coordinates - left bottom, rt is maximal corner
            // r.org is origin of ray
            var t1 = (Min.X - origin.X) * dirfrac.X;
            var t2 = (Max.X - origin.X) * dirfrac.X;
            var t3 = (Min.Y - origin.Y) * dirfrac.Y;
            var t4 = (Max.Y - origin.Y) * dirfrac.Y;
            var t5 = (Min.Z - origin.Z) * dirfrac.Z;
            var t6 = (Max.Z - origin.Z) * dirfrac.Z;

            var tmin = Math.Max(Math.Max(Math.Min(t1, t2), Math.Min(t3, t4)), Math.Min(t5, t6));
            var tmax = Math.Min(Math.Min(Math.Max(t1, t2), Math.Max(t3, t4)), Math.Max(t5, t6));

            //TODO: Should, if tmax < 0, the exterior and interior be swaped?
            if (tmin <= tmax)
                ranges.Add(new RangeShape(tmin, tmax, this, this, RangeShape.Sides.Exterior, RangeShape.Sides.Interior));

            return ranges;
        }

        public override Vector3d Normal(Vector3d pos)
        {
            const double eps = 1e-8;
            var minDif = pos - Min;
            var maxDif = pos - Max;

            if (Math.Abs(minDif.X) < eps)
                return -Vector3d.UnitX;
            else if (Math.Abs(maxDif.X) < eps)
                return Vector3d.UnitX;
            else if (Math.Abs(minDif.Z) < eps)
                return -Vector3d.UnitZ;
            else if (Math.Abs(maxDif.Z) < eps)
                return Vector3d.UnitZ;
            else if (Math.Abs(minDif.Y) < eps)
                return -Vector3d.UnitY;
            else
                return Vector3d.UnitY;
        }
    }
}
