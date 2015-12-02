using Chess.Scene.Color;
using CSG.Color;
using OpenTK;

namespace CSG.Material
{
    // Source: Aurelius
    public class Wood : Material
    {
        private PhongMap pmap;
        private Vector3d centre;
        private Vector3d direction;
        private double k;
        public Wood(PhongMap map, Vector3d centre, Vector3d dir, double coef = 1.0)
        {
            pmap = map;
            this.centre = centre;
            direction = dir;
            direction.Normalize();
            k = coef;
        }
        public Wood(PhongMap map)
        {
            pmap = map;
            centre = new Vector3d(500, 50, 500);
            direction = Vector3d.UnitX;
            k = 1.0;
        }
        public override PhongInfo GetPhongInfo(Vector3d position, Vector3d normal)
        {
            var test = position / 50;

            Vector3d a = test - centre;
            Vector3d proj = (a * direction) * direction;
            Vector3d dist = a - proj;
            double x = k * dist.Length;
            x = x % 1.0;
            return pmap.Eval(x);
        }
    }
}