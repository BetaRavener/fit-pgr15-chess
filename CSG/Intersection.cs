using CSG.Shapes;
using OpenTK;

namespace CSG
{
    /// <summary>
    ///     Represents an intersection with shape.
    /// </summary>
    public class Intersection
    {
        /// <summary>
        ///     Distance from origin along ray at which the intersection occured.
        /// </summary>
        public double Distance;

        public Intersection(IntersectionKind kind, Shape shape = null, double dist = double.MaxValue)
        {
            Kind = kind;
            Shape = shape;
            Distance = dist;
        }

        /// <summary>
        ///     Kind of intersection.
        /// </summary>
        public IntersectionKind Kind { get; }

        /// <summary>
        ///     Intersected shape.
        /// </summary>
        public Shape Shape { get; }

        /// <summary>
        ///     Gets normal from the shape in the point of intersection, normalizes it and recalculates from inside if necessary
        /// </summary>
        /// <param name="hitPosition"></param>
        /// <returns></returns>
        public Vector3d ShapeNormal(Vector3d hitPosition)
        {
            var normal = Shape.Normal(hitPosition).Normalized();
            if (Kind == IntersectionKind.Outfrom)
            {
                normal = -normal;
            }

            return normal;
        }
    }
}