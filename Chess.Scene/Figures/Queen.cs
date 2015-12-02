using CSG;
using CSG.Shapes;
using OpenTK;

namespace Chess.Scene.Figures
{
    public class Queen : FigureObject
    {
        public Queen()
        {
            MaxY = 92;
        }

        public override CSGNode Build(Vector3d center, SceneObject sceneObject)
        {
            var dir = new Vector3d(0, 1, 0);

            var base1 = new Cylinder(center, dir, 30, 6, sceneObject);

            var conePosition1 = center + new Vector3d(0, base1.Height, 0);
            var cone1 = new Cone(conePosition1, dir, 26, 50, sceneObject);

            var planePosition = conePosition1 + new Vector3d(0, cone1.Height - 10, 0);
            var plane = new Plane(dir, planePosition, sceneObject);

            var cutedCone = new CSGNode(CSGNode.Operations.Difference, cone1, plane);

            var baseUniun = new CSGNode(CSGNode.Operations.Union, base1, cutedCone);

            var cylinder = new Cylinder(planePosition, dir, 12, 8, sceneObject);

            var bottomBodyUnion = new CSGNode(CSGNode.Operations.Union, baseUniun, cylinder);


            var oppositeDir = new Vector3d(0, -1, 0);
            var conePosition2 = planePosition + new Vector3d(0, cylinder.Height + 16, 0);
            var cone2 = new Cone(conePosition2, oppositeDir, 14, 26, sceneObject);

            var planePosition2 = planePosition + new Vector3d(0, cylinder.Height, 0);
            var plane2 = new Plane(oppositeDir, planePosition2, sceneObject);

            var cutedCone2 = new CSGNode(CSGNode.Operations.Difference, cone2, plane2);


            var bodyUnion = new CSGNode(CSGNode.Operations.Union, bottomBodyUnion, cutedCone2);


            var spherePosition = conePosition2 + new Vector3d(0, 8, 0);
            var sphere = new Sphere(spherePosition, 12, sceneObject);


            var cylinderPosition1 = spherePosition + new Vector3d(0, 6, 0);
            var cylinder1 = new Cylinder(cylinderPosition1, dir, 10, 6, sceneObject);

            var cylinderPosition2 = cylinderPosition1 + new Vector3d(0, -2, 0);
            var cylinder2 = new Cylinder(cylinderPosition2, dir, 8, 10, sceneObject);

            var crownDiff = new CSGNode(CSGNode.Operations.Difference, cylinder1, cylinder2);

            var bodyWithHeadUnion = new CSGNode(CSGNode.Operations.Union, bodyUnion, sphere);

            return new CSGNode(CSGNode.Operations.Union, bodyWithHeadUnion, crownDiff);
        }
    }
}