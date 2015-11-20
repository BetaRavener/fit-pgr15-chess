using OpenTK;
using RayMath;

namespace CSG.Shapes
{
    public abstract class Shape
    {

        public virtual Intersection IntersectFirst(Ray ray)
        {
            Ranges<Shape> r = Intersect(ray);
            RangeEdge<Shape> re = r.FirstEdgeGreater(0);
            switch (re.Kind)
            {
                case Intersection.IntersectionKind.Into: return new Intersection(Intersection.IntersectionKind.Into, re.Node, re.Distance);
                case Intersection.IntersectionKind.Outfrom: return new Intersection(Intersection.IntersectionKind.Outfrom, re.Node, re.Distance);
                default: return new Intersection(Intersection.IntersectionKind.None);
            }
        }

        public abstract Ranges<Shape> Intersect(Ray ray);

        public abstract Vector3d Normal(Vector3d pos);       
    }
}
