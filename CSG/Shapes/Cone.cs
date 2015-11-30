using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using RayMath;

namespace CSG.Shapes
{
    public class Cone : Shape
    {
        public Vector3d Start { get; protected set; }
        public Vector3d Direction { get; protected set; }
        public double Radius { get; protected set; }
        public double Height { get; set; }
        private Plane _cap;
        private Vector3d _apex;
        private double _alpha;
        private double _tanAlpha;
        private double _sin2Alpha;
        private double _cos2Alpha;

        public Cone(Vector3d start, Vector3d dir, double radius, double height, SceneObject sceneObject)
            : base(sceneObject)
        {
            Start = start;
            Direction = Vector3d.Normalize(dir);
            Radius = radius;
            Height = height;
            _cap = new Plane(-Direction, Start, sceneObject);

            _apex = Start + Direction * Height;

            _tanAlpha = Radius / Height;
            _alpha = Math.Atan(_tanAlpha);
            var sinA = Math.Sin(_alpha);
            var cosA = Math.Cos(_alpha);
            _sin2Alpha = sinA * sinA;
            _cos2Alpha = cosA * cosA; 
        }

        public override RangesShape Intersect(Ray ray)
        {
            RangesShape ranges = new RangesShape();

            // Compute quadratic cone coefficients
            var deltaP = ray.Origin - _apex;

            var inverseDir = -Direction;
            var c1 = Vector3d.Dot(ray.Direction, inverseDir);
            var c2 = inverseDir * c1;
            var c3 = Vector3d.Dot(deltaP, inverseDir);
            var c4 = inverseDir * c3;

            double A = _cos2Alpha * (ray.Direction - c2).LengthSquared - _sin2Alpha * c1 * c1;
            double B = 2 * _cos2Alpha * Vector3d.Dot(ray.Direction - c2, deltaP - c4) - 2 * _sin2Alpha * (c1 * c3);
            double C = _cos2Alpha * (deltaP - c4).LengthSquared - _sin2Alpha * (c3 * c3);

            // Solve quadratic equation for _t_ values
            double D = B * B - 4 * A * C;

            if (D > 0)
            {
                double sD = Math.Sqrt(D);
                double t1 = (-B - sD) / (2 * A);
                double t2 = (-B + sD) / (2 * A);
                bool withCone1 = true;
                bool withCone2 = true;

                if (Vector3d.Dot(Direction, ray.PointAt(t1) - Start) > 0 && Vector3d.Dot(Direction, ray.PointAt(t1) - _apex) > 0)
                {
                    if (Vector3d.Dot(Direction, ray.Direction) < 0)
                        t1 = RangeShape.Inf;
                    else
                        t1 = -RangeShape.Inf;
                }
                if (Vector3d.Dot(Direction, ray.PointAt(t2) - Start) > 0 && Vector3d.Dot(Direction, ray.PointAt(t2) - _apex) > 0)
                {
                    if (Vector3d.Dot(Direction, ray.Direction) < 0)
                        t2 = RangeShape.Inf;
                    else
                        t2 = -RangeShape.Inf;
                }

                if (Vector3d.Dot(Direction, ray.PointAt(t1) - Start) < 0 && Vector3d.Dot(Direction, ray.PointAt(t1) - _apex) < 0)
                {
                    withCone1 = false;
                }
                if (Vector3d.Dot(Direction, ray.PointAt(t2) - Start) < 0 && Vector3d.Dot(Direction, ray.PointAt(t2) - _apex) < 0)
                {
                    withCone2 = false;
                }

                //if (!(Vector3d.Dot(Direction, ray.PointAt(t1) - Start) > 0 && Vector3d.Dot(Direction, ray.PointAt(t1) - _apex) < 0))
                //{
                //    if (Vector3d.Dot(Direction, Start - ray.Origin) > 0)
                //        t1 = -RangeShape.Inf;
                //    else
                //        t1 = RangeShape.Inf;
                //}  
                //if (!(Vector3d.Dot(Direction, ray.PointAt(t2) - Start) > 0 && Vector3d.Dot(Direction, ray.PointAt(t2) - _apex) < 0))
                //{
                //    if (Vector3d.Dot(Direction, Start - ray.Origin) > 0)
                //        t2 = -RangeShape.Inf;
                //    else
                //        t2 = RangeShape.Inf;
                //}

                //var minT = Math.Min(t1, t2);
                //var maxT = Math.Max(t1, t2);
                //// If looking from bottom and there's only one intersection, we need to cap the cone
                //if (Vector3d.Dot(Direction, Start - ray.Origin) > 0)
                //{
                //    if (minT != RangeShape.Inf && maxT == RangeShape.Inf )
                //    {
                //        var inters = _cap.IntersectFirst(ray);
                //        ranges.Add(new RangeShape(inters.Distance, minT, _cap, this, RangeShape.Sides.Exterior, RangeShape.Sides.Interior));
                //    }
                //    else if (minT != RangeShape.Inf && maxT != RangeShape.Inf)
                //        ranges.Add(new RangeShape(minT, maxT, this, this, RangeShape.Sides.Exterior, RangeShape.Sides.Interior));
                //}
                //else if (minT != RangeShape.Inf)
                //    ranges.Add(new RangeShape(minT, maxT, this, this, RangeShape.Sides.Exterior, RangeShape.Sides.Interior));
                if (withCone1 || withCone2)
                {
                    ranges.Add(new RangeShape(t1, t2, this, this, RangeShape.Sides.Exterior, RangeShape.Sides.Interior));
                    var capRanges = _cap.Intersect(ray);
                    capRanges.Inverse();
                    ranges.Intersection(capRanges);
                }
            }

            return ranges;
        }
        public override Vector3d Normal(Vector3d pos)
        {
            var proj = Start + ((pos - Start) * Direction) * Direction;
            var cylinderNormal = (pos - proj).Normalized();
            return (cylinderNormal + _tanAlpha * Direction).Normalized();
        }
    }
}
