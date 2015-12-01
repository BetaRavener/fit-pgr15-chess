using RayMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace CSG
{
    public class CsgNode
    {
        public enum Operations
        {
            Union,
            Intersection,
            Difference
        }

        public Operations Operation { get; set; }
        public CsgNode Left { get; set; }
        public CsgNode Right { get; set; }

        /// <summary>
        /// Constructs CSG node from 2 previous nodes and operation between them. 
        /// </summary>
        /// <param name="operation">Operation that is performed between the nodes.</param>
        /// <param name="left">Left node (Operand A)</param>
        /// <param name="right">Right node (Operand B)</param>
        public CsgNode(Operations operation, CsgNode left, CsgNode right)
        {
            Operation = operation;
            Left = left;
            Right = right;
        }

        /// <summary>
        /// Constructs a CSG leaf.
        /// </summary>
        public CsgNode()
        {
            Left = null;
            Right = null;
        }

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
                case IntersectionKind.Into: return new Intersection(IntersectionKind.Into, re.Node, re.Distance);
                case IntersectionKind.Outfrom: return new Intersection(IntersectionKind.Outfrom, re.Node, re.Distance);
                default:
                    return null;
            }
        }

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
