using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Math3D
{
    class Ray
    {
        public Vector3d Origin { get; set; }
        public Vector3d Direction { get; set; }

        Ray(Vector3d origin, Vector3d direction)
        {
            Origin = new Vector3d(origin);
            Direction = new Vector3d(direction);
            Direction.Normalize();
        }
    }
}
