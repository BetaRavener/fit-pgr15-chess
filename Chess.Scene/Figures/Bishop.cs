using System.Collections.Generic;
using CSG;
using CSG.Shapes;
using OpenTK;

namespace Chess.Scene.Figures
{
    public class Bishop : FigureObject
    {
        public Bishop()
        {
            MaxY = 86;
        }

        public override CSGNode Build(Vector3d center, SceneObject sceneObject)
        {
            var dir = new Vector3d(0, 1, 0);

            var base1 = new Cylinder(center, dir, 30, 6, sceneObject);

            var base2Position = center + new Vector3d(0, base1.Height, 0);
            var base2 = new Cylinder(base2Position, dir, 26, 6, sceneObject);

            var base3Position = base2Position + new Vector3d(0, base2.Height, 0);
            var base3 = new Cylinder(base3Position, dir, 22, 6, sceneObject);

            var baseUniun1 = new CSGNode(CSGNode.Operations.Union, base1, base2);

            var baseUniun = new CSGNode(CSGNode.Operations.Union, baseUniun1, base3);

            var conePosition = base3Position + new Vector3d(0, base3.Height, 0);
            var cone = new Cone(conePosition, dir, 16, 40, sceneObject);

            var colarPosition = conePosition + new Vector3d(0, cone.Height - 4, 0);
            var collar = new Cylinder(colarPosition, dir, 12, 6, sceneObject);

            var mainUnion1 = new CSGNode(CSGNode.Operations.Union, cone, collar);

            var spherePosition = conePosition + new Vector3d(0, cone.Height + 10, 0);
            var sphere = new Sphere(spherePosition, 12, sceneObject);

            var mainUnion2 = new CSGNode(CSGNode.Operations.Union, mainUnion1, sphere);

            var conePosition2 = spherePosition + new Vector3d(0, sphere.Radius - 4, 0);
            var cone2 = new Cone(conePosition2, dir, 6, 10, sceneObject);

            var mainUnion3 = new CSGNode(CSGNode.Operations.Union, mainUnion2, cone2);

            return new CSGNode(CSGNode.Operations.Union, baseUniun, mainUnion3);
        }

        public override List<Box> BuildMinorBoundingBoxes(Vector3d pos)
        {
            var bboxes = new List<Box>();

            var minX = pos.X - 16;
            var minZ = pos.Z - 16;
            var minY = pos.Y + 18;

            var maxX = pos.X + 16;
            var maxZ = pos.Z + 16;
            var maxY = pos.Y + 86;

            bboxes.Add(new Box(new Vector3d(minX, minY, minZ), new Vector3d(maxX, maxY, maxZ), null));

            minX = pos.X - 30;
            minZ = pos.Z - 30;
            minY = pos.Y;

            maxX = pos.X + 30;
            maxZ = pos.Z + 30;
            maxY = pos.Y + 18;

            bboxes.Add(new Box(new Vector3d(minX, minY, minZ), new Vector3d(maxX, maxY, maxZ), null));

            return bboxes;
        }
    }
}