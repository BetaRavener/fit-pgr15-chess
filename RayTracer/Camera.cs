using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RayTracer
{
    class Camera
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
    }
}
