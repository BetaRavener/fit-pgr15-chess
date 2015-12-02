using CSG;
using CSG.Shapes;
using OpenTK;

namespace Chess.Scene.Figures
{
    public class King : FigureObject
    {
        public King()
        {
            MaxY = 96;
        }

        public override CSGNode Build(Vector3d center, SceneObject sceneObject)
        {
            var dir = new Vector3d(0, 1, 0);

            var base1 = new Cylinder(center, dir, 30, 10, sceneObject);

            var conePosition1 = center + new Vector3d(0, base1.Height, 0);
            var cone1 = new Cone(conePosition1, dir, 20, 80, sceneObject);

            var planePosition = conePosition1 + new Vector3d(0, cone1.Height - 40, 0);
            var plane = new Plane(dir, planePosition, sceneObject);

            var cutedCone = new CSGNode(CSGNode.Operations.Difference, cone1, plane);

            var baseUniun = new CSGNode(CSGNode.Operations.Union, base1, cutedCone);

            var cylinder = new Cylinder(planePosition, dir, 18, 8, sceneObject);

            var coneUnion = new CSGNode(CSGNode.Operations.Union, baseUniun, cylinder);

            var spherePosition = planePosition + new Vector3d(0, cylinder.Height + 8, 0);
            var sphere = new Sphere(spherePosition, 12, sceneObject);

            var bodyUnion = new CSGNode(CSGNode.Operations.Union, coneUnion, sphere);

            var cylinderPosition1 = spherePosition + new Vector3d(0, 6, 0);
            var cylinder1 = new Cylinder(cylinderPosition1, dir, 16, 10, sceneObject);

            var cylinderPosition2 = cylinderPosition1 + new Vector3d(0, -2, 0);
            var cylinder2 = new Cylinder(cylinderPosition2, dir, 10, 14, sceneObject);

            var cross1 = new Box(cylinderPosition2 + new Vector3d(-3, 0, -3), cylinderPosition2 + new Vector3d(3, 26, 3), sceneObject);

            var cross2 = new Box(cylinderPosition2 + new Vector3d(-7, 18, -3), cylinderPosition2 + new Vector3d(7, 22, 3), sceneObject);

            var cross = new CSGNode(CSGNode.Operations.Union, cross1, cross2);

            var mainUnion = new CSGNode(CSGNode.Operations.Difference, cylinder1, cylinder2);


            var headUnion = new CSGNode(CSGNode.Operations.Union, mainUnion, cross);

            return new CSGNode(CSGNode.Operations.Union, bodyUnion, headUnion);
        }
    }
}