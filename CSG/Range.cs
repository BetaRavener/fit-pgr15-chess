using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSG
{
    /// <summary>
    /// Represents a single span on the ray.
    /// </summary>
    public class RangeShape
    {
        public enum Sides
        {
            Exterior,
            Interior
        }

        public const double Inf = double.MaxValue;

        /// <summary>
        /// Distance from origin along ray at which the span begins.
        /// </summary>
        public double Left { get; private set; }

        /// <summary>
        /// Distance from origin along ray at which the span ends.
        /// </summary>
        public double Right { get; private set; }

        /// <summary>
        /// Node that limits the span closer to origin.
        /// </summary>
        public Shapes.Shape LeftNode { get; private set; }

        /// <summary>
        /// Node that limits the span further from origin.
        /// </summary>
        public Shapes.Shape RightNode { get; private set; }

        public Sides LeftSide { get; private set; }

        public Sides RightSide { get; private set; }

        public RangeShape(double a, double b, Shapes.Shape aNode, Shapes.Shape bNode, Sides aSide, Sides bSide)
        {
            if (a <= b)
            {
                Left = a;
                Right = b;
                LeftNode = aNode;
                RightNode = bNode;
                LeftSide = aSide;
                RightSide = bSide;
            }
            else
            {
                Left = b;
                Right = a;
                LeftNode = bNode;
                RightNode = aNode;
                LeftSide = bSide;
                RightSide = aSide;
            }
        }

        /// <summary>
        /// Sets the left edge of this span.
        /// </summary>
        /// <param name="value">New distance from origin along ray at which the span begins.</param>
        /// <param name="node">Node that creates edge.</param>
        public void SetLeft(double value, Shapes.Shape node, Sides side)
        {
            Left = value;
            LeftNode = node;
            LeftSide = side;
        }

        /// <summary>
        /// Sets the right edge of this span.
        /// </summary>
        /// <param name="value">New distance from origin along ray at which the span ends.</param>
        /// <param name="node">Node that creates edge.</param>
        public void SetRight(double value, Shapes.Shape node, Sides side)
        {
            Right = value;
            RightNode = node;
            RightSide = side;
        }

        /// <summary>
        /// Finds intersection of two spans (their common interval).
        /// </summary>
        /// <param name="a">First span.</param>
        /// <param name="b">Second span.</param>
        /// <returns>Intersection of spans.</returns>
        public static RangeShape operator *(RangeShape a, RangeShape b)
        {
            if (a.Left > b.Left)
            {
                if (a.Right <= b.Right)
                {
                    return new RangeShape(a.Left, a.Right, a.LeftNode, a.RightNode, a.LeftSide, a.RightSide);
                }
                else
                {
                    return new RangeShape(a.Left, b.Right, a.LeftNode, b.RightNode, a.LeftSide, b.RightSide);
                }
            }
            else
            {
                if (a.Right <= b.Right)
                {
                    return new RangeShape(b.Left, a.Right, b.LeftNode, a.RightNode, b.LeftSide, a.RightSide);
                }
                else
                {
                    return new RangeShape(b.Left, b.Right, b.LeftNode, b.RightNode, b.LeftSide, b.RightSide);
                }
            }
        }
    }
}
