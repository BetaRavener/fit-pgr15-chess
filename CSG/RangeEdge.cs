using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RayMath;

namespace CSG
{
    public class RangeEdge <T> where T :class
    {
        public double Distance { get; private set; }

        public T Node { get; private set; }

        public Intersection.IntersectionKind Kind { get; private set; }

        public RangeEdge(double dist, T node, Intersection.IntersectionKind kind)
        {
            Distance = dist;
            Node = node;
            Kind = kind;
        }
    }
}
