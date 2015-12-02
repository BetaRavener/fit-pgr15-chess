using Chess.Scene.Color;
using CSG.Color;
using OpenTK;

namespace CSG.Material
{
    public class Marble : Material
    {
        PhongMap pmap;
        Vector3d direction;
        public Marble(PhongMap map)
        {
            pmap = map;
            direction = new Vector3d(0, 1, 0);
        }
        public override PhongInfo GetPhongInfo(Vector3d position, Vector3d normal)
        {
            double x = position.X * direction.X + position.Y * direction.Y + position.Z + direction.Z;
            x = x % 1.0;
            if (x < 0) x += 1;
            //cout << x << endl;
            return pmap.Eval(x);
        }
    }
}