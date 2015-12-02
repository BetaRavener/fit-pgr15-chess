using System;
using OpenTK;

namespace CSG.Materials
{
    public class ConstMaterial : Material
    {
        public PhongInfo PhongInfo { get; set; }

        public ConstMaterial(PhongInfo a)
        {
            PhongInfo = a;
        }

        public ConstMaterial()
        {
        }

        public override PhongInfo GetPhongInfo(Vector3d position, Vector3d normal)
        {
            return PhongInfo;
        }
    }
}