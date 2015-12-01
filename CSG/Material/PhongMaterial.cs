using Chess.Scene.Color;
using CSG.Color;
using OpenTK;

namespace CSG.Material
{
    // Source: Aurelius
    public class PhongMaterial : Material
    {
        private PhongInfo phong;
        public PhongMaterial(Color.Color diffuse, Color.Color specular, Color.Color ambient, double shininess, double reflectance = 0.0)
        {
            phong = new PhongInfo(diffuse, specular, ambient, shininess, reflectance);
        }

        public override PhongInfo GetPhongInfo(Vector3d position, Vector3d normal)
        {
            return phong;
        }
    }
}