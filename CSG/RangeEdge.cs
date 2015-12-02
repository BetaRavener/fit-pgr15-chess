using CSG.Shapes;

namespace CSG
{
    /// <summary>
    ///     Represents an edge of span.
    /// </summary>
    public class RangeEdgeShape
    {
        public RangeEdgeShape(double dist, Shape node, IntersectionKind kind)
        {
            Distance = dist;
            Node = node;
            Kind = kind;
        }

        public RangeEdgeShape(double dist)
        {
            Distance = dist;
        }

        /// <summary>
        ///     Distance from origin along ray at which the edge is.
        /// </summary>
        public double Distance { get; private set; }

        /// <summary>
        ///     Shape that creates the edge.
        /// </summary>
        public Shape Node { get; private set; }

        /// <summary>
        ///     The kind of edge.
        /// </summary>
        public IntersectionKind Kind { get; private set; }
    }
}