using System;
using CSG.Color;
using OpenTK;

namespace CSG.Material
{
    public class Checker : Material
    {
        private readonly PhongInfo p1;
        private readonly PhongInfo p2;
        private readonly uint width;
        private readonly uint height;
        public Checker(PhongInfo a, PhongInfo b, uint width, uint height)
        {
            p1 = a;
            p2 = b;
            this.width = width;
            this.height = height;
        }
        public override PhongInfo GetPhongInfo(Vector3d position, Vector3d normal)
        {
            if (normal == Vector3d.UnitY)
            {
                return (((int)(position.X / width) ^ (int)(position.Z / height)) & 1) == 1 ? p1 : p2;
            }

            // Return whatever
            return p1;
            //var evenX = (int)(position.X / width) % 2 == 0;
            //var evenZ = (int)(position.Z / height) % 2 == 0;
            //if ((evenX && evenZ) || (!evenX && !evenZ))
            //{
            //    return p1;
            //}
            //else
            //{
            //    return p2;
            //}
        }
    }
}