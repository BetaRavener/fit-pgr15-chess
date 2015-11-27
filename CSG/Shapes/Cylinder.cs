using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
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
        private Plane _topCap;
        private Plane _bottomCap;

        public Cylinder(Vector3d start, Vector3d dir, double radius, double height, Color4 color) : base(color)
        {
            Start = start;
            Direction = Vector3d.Normalize(dir);
            Radius = radius;
            Height = height;
            _topCap = new Plane(Direction, Start + Direction * Height, Color);
            _bottomCap = new Plane(-Direction, Start, Color);
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
                
                //Plane cap1 = null;
                //Plane cap2 = null;
                //Intersection cap1I = null;
                //Intersection cap2I = null;
                //if (t1 == RangeShape.Inf || t2 == RangeShape.Inf)
                //{
                //    var cap1Itmp = (_topCap).IntersectFirst(ray);
                //    var cap2Itmp = (_bottomCap).IntersectFirst(ray);
                //    if (cap1Itmp.Distance < cap2Itmp.Distance)
                //    {
                //        cap1 = _topCap;
                //        cap2 = _bottomCap;
                //        cap1I = cap1Itmp;
                //        cap2I = cap2Itmp;
                //    }
                //    else
                //    {
                //        cap1 = _bottomCap;
                //        cap2 = _topCap;
                //        cap1I = cap2Itmp;
                //        cap2I = cap1Itmp;
                //    }
                //}

                //if (t1 != RangeShape.Inf)
                //{
                //    if (t2 != RangeShape.Inf)
                //    { // Both intersections with cylinder
                //        ranges.Add(new RangeShape(t1, t2, this, this, RangeShape.Sides.Exterior, RangeShape.Sides.Interior));
                //    }
                //    else
                //    { // First intersection with cylinder, second with cap
                //        //No cap: ranges.Add(new RangeShape(t1, t1, this, this, RangeShape.Sides.Exterior, RangeShape.Sides.Interior));
                //        ranges.Add(new RangeShape(t1, cap2I.Distance, this, cap2, RangeShape.Sides.Exterior, RangeShape.Sides.Interior));
                //    }
                //}
                //else
                //{
                //    if (t2 != RangeShape.Inf)
                //    { // First intersection with cap, second with cylinder 
                //        //No cap: ranges.Add(new RangeShape(t2, t2, this, this, RangeShape.Sides.Interior, RangeShape.Sides.Exterior));
                //        ranges.Add(new RangeShape(cap1I.Distance, t2, cap1, this, RangeShape.Sides.Exterior, RangeShape.Sides.Interior));
                //    }
                //    else
                //    { // Both intersections with cap
                //        // Needs additional test if the intersection is in radius on the plane
                //        var cap1v = (ray.Origin + ray.Direction * cap1I.Distance) - cap1.;
                //        var cap2v = ray.Origin + ray.Direction * cap2I.Distance;
                //        if ()
                //        ranges.Add(new RangeShape(cap1I.Distance, cap2I.Distance, cap1, cap2, RangeShape.Sides.Exterior, RangeShape.Sides.Interior));
                //    }
                //}

                ranges.Add(new RangeShape(t1, t2, this, this, RangeShape.Sides.Exterior, RangeShape.Sides.Interior));

                var rangesCap1 = _topCap.Intersect(ray);
                var rangesCap2 = _bottomCap.Intersect(ray);
                rangesCap1.Union(rangesCap2);
                rangesCap1.Inverse();
                ranges.Intersection(rangesCap1);
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
