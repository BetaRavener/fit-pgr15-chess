using Chess.Scene.Color;
using CSG.Color;
using OpenTK;

namespace CSG.Material
{
    // Source: Aurelius
    public class NoiseDisplace : Material {
        Noise.Noise noise;
        Material material;
        double strength;

        public NoiseDisplace(Material m, Noise.Noise n, double strength)
        {
            material = m; noise = n; this.strength = strength;
        }
        public override PhongInfo GetPhongInfo(Vector3d position, Vector3d normal) {
            double dx = strength * noise.Eval(position.X, position.Y, position.Z);
            double dy = strength * noise.Eval(position.Y + 3.5, position.Z + 7.3, position.X + 2.1);
            double dz = strength * noise.Eval(position.Z - 0.3, position.X + 6.8, position.Y + 1.2);
            return material.GetPhongInfo(new Vector3d(position.X + dx, position.Y + dy, position.Z + dz), normal);
        }
    }
}