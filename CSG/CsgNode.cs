using RayMath;

namespace CSG
{
    public class CSGNode
    {
        public enum Operations
        {
            Union,
            Intersection,
            Difference
        }

        /// <summary>
        ///     Constructs CSG node from 2 previous nodes and operation between them.
        /// </summary>
        /// <param name="operation">Operation that is performed between the nodes.</param>
        /// <param name="left">Left node (Operand A)</param>
        /// <param name="right">Right node (Operand B)</param>
        public CSGNode(Operations operation, CSGNode left, CSGNode right)
        {
            Operation = operation;
            Left = left;
            Right = right;
        }

        /// <summary>
        ///     Constructs a CSG leaf.
        /// </summary>
        public CSGNode()
        {
            Left = null;
            Right = null;
        }

        public Operations Operation { get; set; }
        public CSGNode Left { get; set; }
        public CSGNode Right { get; set; }

        /// <summary>
        ///     Find first intersection with ray.
        /// </summary>
        /// <param name="ray">Tracing ray.</param>
        /// <returns>First intersection.</returns>
        public virtual Intersection IntersectFirst(Ray ray)
        {
            var r = Intersect(ray);
            var re = r.FirstEdgeGreater(0);
            switch (re.Kind)
            {
                case IntersectionKind.Into:
                    return new Intersection(IntersectionKind.Into, re.Node, re.Distance);
                case IntersectionKind.Outfrom:
                    return new Intersection(IntersectionKind.Outfrom, re.Node, re.Distance);
                default:
                    return null;
            }
        }

        /// <summary>
        ///     Find set of spans at which the ray intersects this shape.
        ///     This is an abstract method and must be implemented.
        /// </summary>
        /// <param name="ray">Tracing ray.</param>
        /// <returns>Set of spans.</returns>
        public virtual RangesShape Intersect(Ray ray)
        {
            var rangesLeft = Left.Intersect(ray);
            var rangesRight = Right.Intersect(ray);

            switch (Operation)
            {
                case Operations.Union:
                    rangesLeft.Union(rangesRight);
                    break;
                case Operations.Intersection:
                    rangesLeft.Intersection(rangesRight);
                    break;
                case Operations.Difference:
                    rangesRight.Inverse();
                    rangesLeft.Intersection(rangesRight);
                    break;
            }

            return rangesLeft;
        }
    }
}