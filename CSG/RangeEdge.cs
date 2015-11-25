using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayMath;

namespace CSG
{
    /// <summary>
    /// Represents an edge of span.
    /// </summary>
    public class RangeEdgeShape
    {
        /// <summary>
        /// Distance from origin along ray at which the edge is.
        /// </summary>
        public double Distance { get; private set; }

        /// <summary>
        /// Shape that creates the edge.
        /// </summary>
        public Shapes.Shape Node { get; private set; }

        /// <summary>
        /// The kind of edge. 
        /// </summary>
        public IntersectionKind Kind { get; private set; }

        public RangeEdgeShape(double dist, Shapes.Shape node, IntersectionKind kind)
        {
            Distance = dist;
            Node = node;
            Kind = kind;
        }
    }
}
