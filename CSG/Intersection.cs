using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSG.Shapes;
using OpenTK;

namespace CSG
{
    /// <summary>
    /// Represents an intersection with shape.
    /// </summary>
    public class Intersection
    {

        /// <summary>
        /// Kind of intersection.
        /// </summary>
        public IntersectionKind Kind { get; private set; }

        /// <summary>
        /// Intersected shape.
        /// </summary>
        public Shape Shape { get; private set; }

        /// <summary>
        /// Distance from origin along ray at which the intersection occured.
        /// </summary>
        public double Distance;

        public Intersection(IntersectionKind kind, Shape shape = null, double dist = double.MaxValue)
        {
            Kind = kind;
            Shape = shape;
            Distance = dist; 
        }

        public Vector3d ShapeNormal(Vector3d position)
        {
            var normal = Shape.Normal(position);
            // If the intersection is from the inside, inverse normal vector
            if (Kind == IntersectionKind.Outfrom)
                normal = -normal;

            return normal;
        }
    }
}
