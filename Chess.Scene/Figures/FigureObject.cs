using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSG;
using CSG.Shapes;
using OpenTK;

namespace Chess.Scene.Figures
{
    public abstract class FigureObject
    {
        public double MaxY = 100;
        public double MaxX = 60;
        public double MaxZ = 60;

        public abstract CSGNode Build(Vector3d center, SceneObject sceneObject);

        public virtual List<Box> BuildBoundingBox(Vector3d pos)
        {
            var bboxes = new List<Box>();

            var minX = pos.X - (MaxX / 2);
            var minZ = pos.Z - (MaxZ / 2);
            var minY = pos.Y;

            var maxX = pos.X + (MaxX / 2);
            var maxZ = pos.Z + (MaxZ / 2);
            var maxY = pos.Y + MaxY;

            bboxes.Add(new Box(new Vector3d(minX, minY, minZ), new Vector3d(maxX, maxY, maxZ), null));

            return bboxes;
        }
    }
}
