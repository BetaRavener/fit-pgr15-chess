using Chess.Scene.Color;
using CSG.Color;
using OpenTK;

namespace CSG.Material
{
    // Source: Aurelius
    public abstract class Material
    {
        public abstract PhongInfo GetPhongInfo(Vector3d position, Vector3d normal);
    }
}
