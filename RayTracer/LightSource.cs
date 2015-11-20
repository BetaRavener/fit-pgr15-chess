using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RayTracer
{
    public class LightSource
    {
        public Vector3d Position { get; set; }
        public Vector3d Color { get; set; }

        public LightSource()
        {
            Position = Vector3d.Zero;
            Color = Vector3d.One;
        }

        public LightSource(int x, int y, int z)
        {
            Position = new Vector3d(x, y, z);
            Color = Vector3d.One;
        }
    }
}
