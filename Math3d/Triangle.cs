using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RayMath
{
    public struct Triangle
    {
        public Vector3d V1 { get; private set; }
        public Vector3d V2 { get; private set; }
        public Vector3d V3 { get; private set; }

        public Vector3d Normal { get; private set; }

        public Triangle(Vector3d v1, Vector3d v2, Vector3d v3, Vector3d n)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
            Normal = n;
        } 
    }
}
