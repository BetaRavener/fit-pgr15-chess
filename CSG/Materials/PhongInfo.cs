using OpenTK.Graphics;

namespace CSG.Materials
{
    // Source: Aurelius
    public class PhongInfo
    {
        public PhongInfo(Color4 diffuse, Color4 ambient, Color4 specular, double shininess, double reflectance = 0.0)
        {
            Diffuse = diffuse;
            Specular = specular;
            Ambient = ambient;
            Shininess = shininess;
            Reflectance = reflectance;
        }

        public PhongInfo(Color4 diffuse, Color4 ambient, double reflectance = 0.0, double shininess = 0.0)
        {
            Diffuse = diffuse;
            Ambient = ambient;
            Shininess = shininess;
            Reflectance = reflectance;
            Specular = Color4.White;
        }

        public PhongInfo()
        {
            Shininess = 0;
            Reflectance = 0;
        }

        public Color4 Diffuse { get; set; }
        public Color4 Specular { get; set; }
        public Color4 Ambient { get; set; }
        public double Shininess { get; set; }
        public double Reflectance { get; set; }
    }
}