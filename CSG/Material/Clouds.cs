using Chess.Scene.Color;
using CSG.Color;
using CSG.Noise;
using OpenTK;

namespace CSG.Material
{
    public class Clouds : Material
    {
        PhongMap pmap;
        double k;
        ValueNoise noise = new ValueNoise();

        public Clouds(PhongMap map, double coef = 1.0)
        {
            pmap = map;
            k = coef;
        }
        public override PhongInfo GetPhongInfo(Vector3d position, Vector3d normal)
        {
            double x = (1.0 + noise.Eval(k * position.X, k * position.Y, k * position.Z)) / 2;
            if (x < 0) x = 0;
            if (x > 1) x = 1;
            return pmap.Eval(x);
        }
    };
}