using OpenTK.Graphics;

namespace CSG.Color
{
    // Source: Aurelius
    public class PhongInfo
    {
        public Color Diffuse { get; private set; }
        public Color Specular { get; private set; }
        public Color Ambient { get; private set; }
        public double Shininess { get; private set; }
        public double Reflectance { get; private set; }

        public PhongInfo(Color diffuse, CSG.Color.Color specular, CSG.Color.Color ambient, double shininess, double reflectance = 0.0)
        {
            Diffuse = diffuse;
            Specular = specular;
            Ambient = ambient;
            Shininess = shininess;
            Reflectance = reflectance;
        }
        public PhongInfo(Color diffuse, Color ambient, double reflectance = 0.0, double shininess = 0.0)
        {
            Diffuse = diffuse;
            Ambient = ambient;
            Shininess = shininess;
            Reflectance = reflectance;
            Specular = new Color(Color4.White);

        }
        public PhongInfo() { Shininess = 0; Reflectance = 0; }


    }
}
