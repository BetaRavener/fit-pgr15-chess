using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayMath;

namespace CSG
{
    //public class RangeEdge <T> where T :class
    //{
    //    public double Distance { get; private set; }

    //    public T Node { get; private set; }

    //    public Intersection.IntersectionKind Kind { get; private set; }

    //    public RangeEdge(double dist, T node, Intersection.IntersectionKind kind)
    //    {
    //        Distance = dist;
    //        Node = node;
    //        Kind = kind;
    //    }
    //}

    //public class RangeEdgeShape :RangeEdge<Shapes.Shape> {
    //    public RangeEdgeShape(double dist, Shapes.Shape node, Intersection.IntersectionKind kind) :
    //        base(dist, node, kind)
    //    { }
    //}

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
        public Intersection.IntersectionKind Kind { get; private set; }

        public RangeEdgeShape(double dist, Shapes.Shape node, Intersection.IntersectionKind kind)
        {
            Distance = dist;
            Node = node;
            Kind = kind;
        }
    }
}
