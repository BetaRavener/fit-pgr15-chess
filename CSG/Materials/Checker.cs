using OpenTK;

namespace CSG.Materials
{
    public class Checker : Material
    {
        public PhongInfo PhongInfo1 { get; set; }
        public PhongInfo PhongInfo2 { get; set; }
        public uint Width { get; set; }
        public uint Height { get; set; }

        public Checker(PhongInfo a, PhongInfo b, uint width, uint height)
        {
            PhongInfo1 = a;
            PhongInfo2 = b;
            Width = width;
            Height = height;
        }

        public Checker()
        {
        }

        public override PhongInfo GetPhongInfo(Vector3d position, Vector3d normal)
        {
            if (normal == Vector3d.UnitY)
            {
                return (((int)(position.X / Width) ^ (int)(position.Z / Height)) & 1) == 1 ? PhongInfo1 : PhongInfo2;
            }

            // Return whatever
            return PhongInfo1;
        }
    }
}