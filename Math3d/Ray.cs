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
        private double _dirDot;

        public Vector3d Origin { get; set; }

        public Vector3d Direction
        {
            get { return _dir; }
            set
            {
                _dir = value;
                _dirfrac = new Vector3d(1.0f / _dir.X, 1.0f / _dir.Y, 1.0f / _dir.Z);
                _dirDot = Vector3d.Dot(_dir, _dir);
            }
        }

        public int Fragment { get; private set; }

        public Vector3d Dirfrac { get { return _dirfrac; } }

        public double DirDot { get { return _dirDot; } }

        public Ray(Vector3d origin, Vector3d direction, int fragment = -1)
        {
            Origin = origin;
            Direction = direction;

            Fragment = fragment;
        }

        public Vector3d PointAt(double t)
        {
            return Origin + Direction * t;
        }

        /// <summary>
        /// Shifts ray little bit so that it's not countet with intersection
        /// </summary>
        /// <param name="shift"></param>
        /// <returns></returns>
        public Ray Shift(double shift = 1e-5)
        {
            Origin += Direction*shift;
            return this;
        }
    }
}
