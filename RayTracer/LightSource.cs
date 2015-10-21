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
        public Vector3 Position { get; set; }
        public Vector3 Color { get; set; }

        public LightSource()
        {
            Position = Vector3.Zero;
            Color = Vector3.One;
        }
    }
}
