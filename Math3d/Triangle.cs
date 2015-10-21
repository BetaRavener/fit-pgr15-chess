using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Math3d
{
    public struct Triangle
    {
        public Vector3 V1 { get; private set; }
        public Vector3 V2 { get; private set; }
        public Vector3 V3 { get; private set; }

        public Vector3 Normal { get; private set; }

        public Triangle(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 n)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
            Normal = n;
        } 
    }
}
