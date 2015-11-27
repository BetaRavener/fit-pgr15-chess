using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.Diagnostics;

namespace RayTracer
{
    public class Camera
    {
        private const double GimLockBuffer = 1e-5;

        private Vector3d _position;
        private Vector3d _lookAt;

        public Vector3d Position { get { return _position; }
            set
            {
                _position = value;
                RecalcVectors();
            }
        }
        public Vector3d ViewVector { get; private set; }
        public Vector3d UpVector { get; private set; }
        public Vector3d RightVector { get; private set; }

        public Vector3d LookAt { get { return _lookAt; }
            set
            {
                _lookAt = value;
                RecalcVectors();
            }
        }

        public double OrbitDistance { get { return (LookAt - Position).Length; }
            set
            {
                double d = Math.Max(0, value);
                Position = LookAt + (Position - LookAt).Normalized() * d;
            }
        }

        public Camera()
        {
            _lookAt = Vector3d.Zero;
            Position = Vector3d.Zero;
        }

        public Camera(int x, int y, int z)
        {
            _lookAt = Vector3d.Zero;
            Position = new Vector3d(x, y, z);
        }

        private void RecalcVectors()
        {
            UpVector = new Vector3d(0, -1, 0);

            ViewVector = (LookAt - Position).Normalized();

            RightVector = Vector3d.Cross(ViewVector, UpVector).Normalized();
            UpVector = Vector3d.Cross(ViewVector, RightVector);
        }

        public void MoveAbsolute(Vector3d position)
        {
            var oldViewVec = ViewVector;
            var oldDistance = OrbitDistance;
            _lookAt = position;
            Position = _lookAt - ViewVector * OrbitDistance;
        }

        public void MoveRelative(Vector3d vec)
        {
            _lookAt += RightVector * vec.X;
            _lookAt += UpVector * vec.Y;
            _lookAt += ViewVector * vec.Z;

            Position = _lookAt - ViewVector * OrbitDistance;
        }

        public void RotateRelative(Vector2d vec)
        {
            const double halfPi = (Math.PI / 2);

            if (vec.X == 0 && vec.Y == 0)
                return;

            var positionVec = new Vector3d(-ViewVector);
            var positionVecZeroY = new Vector3d(positionVec);
            positionVecZeroY.Y = 0;
            positionVecZeroY.Normalize();
            var yDot = Vector3d.Dot(positionVec, positionVecZeroY);

            var currentRot = new Vector2d(Math.Atan2(positionVec.Y, yDot),
                                          Math.Atan2(positionVec.Z, positionVec.X));

            currentRot += vec;

            //check overruns
            while (currentRot.Y > 2*Math.PI)
                currentRot.Y -= 2 * Math.PI;
            while (currentRot.Y < 0.0f)
                currentRot.Y += 2 * Math.PI;
            if (currentRot.X > halfPi - GimLockBuffer)
                currentRot.X = (halfPi - GimLockBuffer);
            else if (currentRot.X < -halfPi + GimLockBuffer)
                currentRot.X = -(halfPi + GimLockBuffer);

            var rotMatZ = Matrix4d.CreateRotationZ(currentRot.X);
            var rotMatY = Matrix4d.CreateRotationY(-currentRot.Y);

            var newPos = Vector3d.Transform(Vector3d.UnitX, rotMatZ);
            newPos = Vector3d.Transform(newPos, rotMatY);
            Position = LookAt + newPos * OrbitDistance;
        }
    }
}
