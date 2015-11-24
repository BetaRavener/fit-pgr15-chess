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

        public virtual RangesShape Intersect(Ray ray)
        {
            if (Left == Right)
            { // This is leaf of the tree
                return Left.Intersect(ray);
            }
            else
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
}
