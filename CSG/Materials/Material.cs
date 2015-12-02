using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace CSG.Materials
{
    // Source: Aurelius
    public abstract class Material
    {
        public abstract PhongInfo GetPhongInfo(Vector3d position, Vector3d normal);
    }
}
