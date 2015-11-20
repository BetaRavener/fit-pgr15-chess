using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSG.Shapes;
using OpenTK;

namespace RayTracer
{
    class HitInfo
    {
        private Shape _normalShape;

        public double Distance { get; private set; }
        public Material Material { get; set; }


        HitInfo(double dist, Shape normalShape = null, Material material = null)
        {
            _normalShape = normalShape;
            Distance = dist;
            Material = material;
        }

        public Vector3d Normal(Vector3d pos)
        {
            return _normalShape.Normal(pos);
        }
    }
}
