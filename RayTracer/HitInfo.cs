using CSG.Shapes;
using OpenTK;

namespace RayTracer
{
    internal class HitInfo
    {
        private readonly Shape _normalShape;


        private HitInfo(double dist, Shape normalShape = null, Material material = null)
        {
            _normalShape = normalShape;
            Distance = dist;
            Material = material;
        }

        public double Distance { get; private set; }
        public Material Material { get; set; }

        public Vector3d Normal(Vector3d pos)
        {
            return _normalShape.Normal(pos);
        }
    }
}