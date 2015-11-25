using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSG;
using CSG.Shapes;

namespace Chess.Scene
{
    public class Utilities
    {
        public static void SetParentTree(CsgNode root, ISceneObject parent)
        {
            if (root == null)
                return;

            ((Shape)root).Parent = parent;

            SetParentTree(root.Left, parent);
            SetParentTree(root.Right, parent);
        }

    }
}
