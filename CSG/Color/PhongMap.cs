using CSG.Color;

namespace Chess.Scene.Color
{
    // Source: Aurelius
    public class PhongMap
    {
        ColorMap diffuse;
        ColorMap specular;
        ColorMap ambient;
        double shininess;
        double reflectance;
        public PhongMap(ColorMap diffuse, ColorMap specular, ColorMap ambient, double shininess, double reflectance = 0.0)
        {
            this.diffuse = diffuse; this.specular = specular; this.ambient = ambient; this.shininess = shininess;
            this.reflectance = reflectance;
        }
        public PhongMap(ColorMap diffuse, ColorMap ambient, double reflectance = 0.0)
        {
            this.diffuse = diffuse; this.ambient = ambient;
            shininess = 0;
            this.reflectance = reflectance;
            specular = new ColorMap();
        }
        public PhongInfo Eval(double x)
        {
            return new PhongInfo(diffuse.GetColor(x), specular.GetColor(x), ambient.GetColor(x), shininess, reflectance);
        }
    };
}
