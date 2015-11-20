using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSG.Shapes;

namespace CSG
{
    public class Intersection
    {
        public enum IntersectionKind
        {
            None,
            Into,
            Outfrom
        };

        public IntersectionKind Kind { get; private set; }
        public Shape Shape { get; private set; }
        public double Distance;

        public Intersection(IntersectionKind kind, Shape shape = null, double dist = Double.MaxValue)
        {
            Kind = kind;
            Shape = shape;
            Distance = dist; 
        }
    }
}
