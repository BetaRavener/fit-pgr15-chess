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
    }
}