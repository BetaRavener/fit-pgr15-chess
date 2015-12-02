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
    }
}