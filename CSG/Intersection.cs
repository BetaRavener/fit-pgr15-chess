using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSG.Shapes;

namespace CSG
{
    /// <summary>
    /// Represents an intersection with shape.
    /// </summary>
    public class Intersection
    {
        public enum IntersectionKind
        {
            /// <summary>
            /// There was no intersection.
            /// </summary>
            None,
            
            /// <summary>
            /// The intersection after which the ray continues into shape.
            /// </summary>
            Into,

            /// <summary>
            /// The intersection after which the ray leaves shape.
            /// </summary>
            Outfrom
        };

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

        public Intersection(IntersectionKind kind, Shape shape = null, double dist = Double.MaxValue)
        {
            Kind = kind;
            Shape = shape;
            Distance = dist; 
        }
    }
}
