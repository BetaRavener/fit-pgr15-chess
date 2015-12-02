using System;
using OpenTK;
using RayMath;

namespace CSG.Shapes
{
    /// <summary>
    ///     Represents a parametric sphere.
    /// </summary>
    public class Sphere : Shape
    {
        private readonly double _radiusSqr;

        public Sphere(Vector3d center, double radius, SceneObject sceneObject)
            : base(sceneObject)
        {
            Center = center;
            Radius = radius;
            _radiusSqr = radius*radius;
        }

        public Vector3d Center { get; protected set; }
        public double Radius { get; protected set; }

        /// <summary>
        ///     Find first intersection with this sphere.
        /// </summary>
        /// <param name="ray">Tracing ray.</param>
        /// <returns>First intersection.</returns>
        public override Intersection IntersectFirst(Ray ray)
        {
            var aa = ray.DirDot;
            var originCenter = ray.Origin - Center;
            var bb = 2*Vector3d.Dot(ray.Direction, originCenter);
            var cc = Vector3d.Dot(originCenter, originCenter) - _radiusSqr;
            var D = bb*bb - 4*aa*cc;
            if (D > 0)
            {
                var sD = Math.Sqrt(D);
                var t1 = (-bb - sD)/(2*aa);
                if (t1 > 0) return new Intersection(IntersectionKind.Into, this, t1);
                var t2 = (-bb + sD)/(2*aa);
                if (t2 > 0) return new Intersection(IntersectionKind.Outfrom, this, t2);
            }
            return null;
        }

        /// <summary>
        ///     Find set of intersection spans between ray and sphere.
        /// </summary>
        /// <param name="ray">Tracing ray.</param>
        /// <returns>Intersection spans.</returns>
        public override RangesShape Intersect(Ray ray)
        {
            var ranges = new RangesShape();

            var aa = ray.DirDot;
            var originCenter = ray.Origin - Center;
            var bb = 2*Vector3d.Dot(ray.Direction, originCenter);
            var cc = Vector3d.Dot(originCenter, originCenter) - _radiusSqr;
            var D = bb*bb - 4*aa*cc;
            if (D > 0)
            {
                var sD = Math.Sqrt(D);
                var t2 = (-bb + sD)/(2*aa);
                var t1 = (-bb - sD)/(2*aa);
                ranges.Add(new RangeShape(t1, t2, this, this, RangeShape.Sides.Exterior, RangeShape.Sides.Interior));
            }
            return ranges;
        }

        /// <summary>
        ///     Finds surface normal for sphere at specified position.
        /// </summary>
        /// <param name="pos">Position at sphere surface.</param>
        /// <returns>Surface normal vector.</returns>
        public override Vector3d Normal(Vector3d pos)
        {
            return Vector3d.Normalize(pos - Center);
        }
    }
}