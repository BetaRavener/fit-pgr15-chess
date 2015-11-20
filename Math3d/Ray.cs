using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using RayMath;

namespace RayMath
{
    public class Ray
    {
        private Vector3d _dir;
        private Vector3d _dirfrac;

        public Vector3d Origin { get; set; }

        public Vector3d Direction
        {
            get { return _dir; }
            set
            {
                _dir = value;
                _dirfrac = new Vector3d(1.0f / _dir.X, 1.0f / _dir.Y, 1.0f / _dir.Z);
            }
        }

        public Vector3d Dirfrac { get { return _dirfrac; } }

        public Ray(Vector3d origin, Vector3d direction, bool normalize = true)
        {
            Origin = origin;
            Direction = direction;
            if (normalize)
                Direction.Normalize();
        }

        public Vector3d PointAt(double t)
        {
            return Origin + Direction * t;
        }

        public void Shift(double shift = 1e-5f)
        {
            Origin += Direction*shift;
        }
    }
}
