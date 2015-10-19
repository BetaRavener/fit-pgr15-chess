using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Math3D
{
    class Triangle
    {
        public Vector3d V1 { get; set; }
        public Vector3d V2 { get; set; }
        public Vector3d V3 { get; set; }

        public Triangle(Vector3d v1, Vector3d v2, Vector3d v3)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }
    }
}
