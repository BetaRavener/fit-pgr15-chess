using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RayTracer
{
    public class Camera
    {
        public Vector3d Position { get; set; }
        public Vector3d ViewVector { get; set; }
        public Vector3d UpVector { get; set; }
        public Vector3d RightVector { get; set; }

        public Camera()
        {
            Position = Vector3d.Zero;
            ViewVector = Vector3d.UnitZ;
            UpVector = Vector3d.UnitY;
            RightVector = Vector3d.UnitX;
        }

        public Camera(int x, int y, int z)
        {
            Position = new Vector3d(x, y, z);
            UpVector = new Vector3d(0, -1, 0);

            var viewVector = Vector3d.Subtract(Vector3d.Zero, Position);
            viewVector.Normalize();
            ViewVector = viewVector;

            RightVector = Vector3d.Cross(ViewVector, UpVector);
            UpVector = Vector3d.Cross(ViewVector, RightVector);        
        }
    }
}
