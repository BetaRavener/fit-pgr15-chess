using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSG.Noise
{
    // Source: Aurelius
    public abstract class Noise
    {
        public abstract double Eval(double x, double y, double z);
    }
}
