using OpenTK;
using RayMath;

namespace CSG.Shapes
{
    /// <summary>
    /// Represents general traceable shape.
    /// </summary>
    public abstract class Shape
    {

        /// <summary>
        /// Find first intersection with ray.
        /// </summary>
        /// <param name="ray">Tracing ray.</param>
        /// <returns>First intersection.</returns>
        public virtual Intersection IntersectFirst(Ray ray)
        {
            RangesShape r = Intersect(ray);
            RangeEdgeShape re = r.FirstEdgeGreater(0);
            switch (re.Kind)
            {
                case Intersection.IntersectionKind.Into: return new Intersection(Intersection.IntersectionKind.Into, re.Node, re.Distance);
                case Intersection.IntersectionKind.Outfrom: return new Intersection(Intersection.IntersectionKind.Outfrom, re.Node, re.Distance);
                default: return new Intersection(Intersection.IntersectionKind.None);
            }
        }

        /// <summary>
        /// Find set of spans at which the ray intersects this shape.
        /// </summary>
        /// <param name="ray">Tracing ray.</param>
        /// <returns>Set of spans.</returns>
        public abstract RangesShape Intersect(Ray ray);

        /// <summary>
        /// Find surface normal of the shape at specified position.
        /// </summary>
        /// <param name="pos">Position - the normal origin.</param>
        /// <returns>Surface normal vector.</returns>
        public abstract Vector3d Normal(Vector3d pos);       
    }
}
