using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Math3d
{
    public class Ray
    {
        private Vector3 _dir;
        private Vector3 _dirfrac;

        public Vector3 Origin { get; set; }

        public Vector3 Direction
        {
            get { return _dir; }
            set
            {
                _dir = value;
                _dirfrac = new Vector3(1.0f / _dir.X, 1.0f / _dir.Y, 1.0f / _dir.Z);
            }
        }

        public Vector3 Dirfrac { get { return _dirfrac; } }

        public Ray(Vector3 origin, Vector3 direction)
        {
            Origin = origin;
            Direction = direction;
        }
    }
}
