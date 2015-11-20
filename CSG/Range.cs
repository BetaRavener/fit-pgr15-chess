using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSG
{
    public class Range <T> where T : class 
    {
        public const double Inf = double.MaxValue;

        public double Left { get; private set; }
        public double Right { get; private set; }

        public T LeftNode { get; private set; }
        public T RightNode { get; private set; }

        public Range(double a, double b, T aNode = null, T bNode = null)
        {
            if (a <= b)
            {
                Left = a;
                Right = b;
                LeftNode = aNode;
                RightNode = bNode;
            }
            else
            {
                Left = b;
                Right = a;
                LeftNode = bNode;
                RightNode = aNode;
            }
        }

        public void SetLeft(double value, T node)
        {
            Left = value;
            LeftNode = node;
        }

        public void SetRight(double value, T node)
        {
            Right = value;
            RightNode = node;
        }

        public static Range<T> operator *(Range<T> a, Range<T> b)
        {
            if (a.Left > b.Left)
            {
                if (a.Right <= b.Right)
                {
                    return new Range<T>(a.Left, a.Right, a.LeftNode, a.RightNode);
                }
                else
                {
                    return new Range<T>(a.Left, b.Right, a.LeftNode, b.RightNode);
                }
            }
            else
            {
                if (a.Right <= b.Right)
                {
                    return new Range<T>(b.Left, a.Right, b.LeftNode, a.RightNode);
                }
                else
                {
                    return new Range<T>(b.Left, b.Right, b.LeftNode, b.RightNode);
                }
            }
        } 
    }
}
