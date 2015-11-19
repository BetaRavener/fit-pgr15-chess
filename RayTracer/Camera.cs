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
        public Vector3 Position { get; set; }
        public Vector3 ViewVector { get; set; }
        public Vector3 UpVector { get; set; }
        public Vector3 RightVector { get; set; }

        public Camera()
        {
            Position = Vector3.Zero;
            ViewVector = new Vector3(0,0,1);
            UpVector = new Vector3(0,1,0);
            RightVector = new Vector3(1,0,0);
        }

        public Camera(int x, int y, int z)
        {
            Position = new Vector3(x, y, z);
            RightVector = new Vector3(1, 0, 0);

            var viewVector = Vector3.Subtract(Vector3.Zero, Position);
            viewVector.Normalize();
            ViewVector = viewVector;

            UpVector = Vector3.Cross(ViewVector, new Vector3(1, 0, 0));        
        }
    }
}
