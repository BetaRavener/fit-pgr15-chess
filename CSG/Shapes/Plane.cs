﻿using OpenTK;
using RayMath;

namespace CSG.Shapes
{
    public class Plane : Shape
    {
        protected double _d;
        protected Vector3d _normal;

        public Plane(Vector3d normal, Vector3d point, SceneObject sceneObject)
            : base(sceneObject)
        {
            normal.Normalize();
            _normal = normal;
            _d = -Vector3d.Dot(_normal, point);
        }

        public override Intersection IntersectFirst(Ray ray)
        {
            var np = Vector3d.Dot(_normal, ray.Direction);
            if (np == 0) return null;
            var t = -(_d + Vector3d.Dot(_normal, ray.Origin))/np;
            return t > 0 ? new Intersection(np > 0 ? IntersectionKind.Into : IntersectionKind.Outfrom, this, t) : null;
        }

        public override RangesShape Intersect(Ray ray)
        {
            var ranges = new RangesShape();
            var np = Vector3d.Dot(_normal, ray.Direction);
            if (np != 0)
            {
                var t = -(_d + Vector3d.Dot(_normal, ray.Origin))/np;
                ranges.Add(np < 0
                    ? new RangeShape(-RangeShape.Inf, t, this, this, RangeShape.Sides.Exterior,
                        RangeShape.Sides.Exterior)
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