using CSG.Color;
using OpenTK;
using OpenTK.Graphics;

namespace CSG.Material
{
    public class GlassMaterial : Material
    {
        private readonly PhongInfo phongInfo;

        public GlassMaterial(Color.Color color)
        {
            phongInfo = new PhongInfo(color, color, 0.5, 6);
        }

        public override PhongInfo GetPhongInfo(Vector3d position, Vector3d normal)
        {
            return phongInfo;
        }
    }
}