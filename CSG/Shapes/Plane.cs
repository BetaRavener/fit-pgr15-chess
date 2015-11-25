using OpenTK;
using OpenTK.Graphics;
using RayMath;

namespace CSG.Shapes
{
    public class Plane : Shape
    {
        protected Vector3d _normal;
        protected double _d;

        public Plane(Vector3d normal, Vector3d point, Color4 color) : base(color)
        {
            normal.Normalize();
            _normal = normal;
            _d = -(Vector3d.Dot(_normal, point));
        }

        public override Intersection IntersectFirst(Ray ray)
        {
            double np = Vector3d.Dot(_normal, ray.Direction);
            if (np == 0) return new Intersection(IntersectionKind.None);
            double t = -(_d + Vector3d.Dot(_normal, ray.Origin)) / np;
            return t > 0 ? new Intersection((np > 0) ? IntersectionKind.Into : IntersectionKind.Outfrom, this, t) : new Intersection(IntersectionKind.None);
        }

        public override RangesShape Intersect(Ray ray)
        {
            RangesShape ranges = new RangesShape();
            var np = Vector3d.Dot(_normal, ray.Direction);
            if (np != 0)
            {
                var t = -(_d + Vector3d.Dot(_normal, ray.Origin)) / np;
                ranges.Add(np < 0
                    ? new RangeShape(-RangeShape.Inf, t, this, this, RangeShape.Sides.Exterior, RangeShape.Sides.Exterior)
                    : new RangeShape(t, RangeShape.Inf, this, this, RangeShape.Sides.Interior, RangeShape.Sides.Interior));
            }
            return ranges;
        }

        public override Vector3d Normal(Vector3d pos)
        {
            return _normal;
        }
    }
}