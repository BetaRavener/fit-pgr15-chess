using OpenTK;

namespace CSG.Materials
{
    public class ConstMaterial : Material
    {
        public ConstMaterial(PhongInfo a)
        {
            PhongInfo = a;
        }

        public ConstMaterial()
        {
        }

        public PhongInfo PhongInfo { get; set; }

        public override PhongInfo GetPhongInfo(Vector3d position, Vector3d normal)
        {
            return PhongInfo;
        }
    }
}