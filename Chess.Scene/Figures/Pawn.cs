using System.Collections.Generic;
using CSG;
using CSG.Shapes;
using OpenTK;

namespace Chess.Scene.Figures
{
    public class Pawn : FigureObject
    {
        public Pawn()
        {
            MaxY = 62;
        }

        public override CSGNode Build(Vector3d center, SceneObject sceneObject)
        {
            var dir = new Vector3d(0, 1, 0);

            var base1 = new Cylinder(center, dir, 30, 10, sceneObject);

            var conePosition = center + new Vector3d(0, base1.Height, 0);
            var cone = new Cone(conePosition, dir, 20, 30, sceneObject);

            var spherePosition = conePosition + new Vector3d(0, cone.Height + 6, 0);
            var sphere = new Sphere(spherePosition, 16, sceneObject);

            var mainUnion = new CSGNode(CSGNode.Operations.Union, cone, sphere);

            return new CSGNode(CSGNode.Operations.Union, base1, mainUnion);
        }

        public override List<Box> BuildMinorBoundingBoxes(Vector3d pos)
        {
            var bboxes = new List<Box>();

            var minX = pos.X - 16;
            var minZ = pos.Z - 16;
            var minY = pos.Y + 30;

            var maxX = pos.X + 16;
            var maxZ = pos.Z + 16;
            var maxY = pos.Y + 46 + 16;

            bboxes.Add(new Box(new Vector3d(minX, minY, minZ), new Vector3d(maxX, maxY, maxZ), null));

            minX = pos.X - 30;
            minZ = pos.Z - 30;
            minY = pos.Y;

            maxX = pos.X + 30;
            maxZ = pos.Z + 30;
            maxY = pos.Y + 30;

            bboxes.Add(new Box(new Vector3d(minX, minY, minZ), new Vector3d(maxX, maxY, maxZ), null));

            return bboxes;
        }
    }
}