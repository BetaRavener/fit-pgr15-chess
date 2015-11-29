using CSG;
using CSG.Shapes;
using OpenTK;
using OpenTK.Graphics;
using RayMath;

namespace Chess.Scene
{
    public class ObjectBuilder
    {
        public static CsgNode BuildChessboard(ISceneObject sceneObject)
        {
            var min = new Vector3d(0, 0, 0);
            var max = new Vector3d(min.X + Chessboard.CroftHeight * 8,
                Chessboard.CroftThickness,
                min.Z + Chessboard.CroftWidth * 8);


            var box = new Box(min, max, sceneObject) {Shininess = 100};
            return box;
        }

        public static CsgNode BuildKing(Vector3d center, ISceneObject sceneObject)
        {
            var dir = new Vector3d(0, 1, 0);

            var base1 = new Cylinder(center, dir, 30, 10, sceneObject);

            var conePosition1 = center + new Vector3d(0, base1.Height, 0);
            var cone1 = new Cone(conePosition1, dir, 20, 80, sceneObject);

            var planePosition = conePosition1 + new Vector3d(0, cone1.Height - 40, 0);
            var plane = new Plane(dir, planePosition, sceneObject);

            var cutedCone = new CsgNode(CsgNode.Operations.Difference, cone1, plane);

            var baseUniun = new CsgNode(CsgNode.Operations.Union, base1, cutedCone);

            var cylinder = new Cylinder(planePosition, dir, 18, 8, sceneObject);

            var coneUnion = new CsgNode(CsgNode.Operations.Union, baseUniun, cylinder);

            var spherePosition = planePosition + new Vector3d(0, cylinder.Height + 8, 0);
            var sphere = new Sphere(spherePosition, 12, sceneObject);

            var bodyUnion = new CsgNode(CsgNode.Operations.Union, coneUnion, sphere);

            var cylinderPosition1 = spherePosition + new Vector3d(0, 6, 0);
            var cylinder1 = new Cylinder(cylinderPosition1, dir, 16, 10, sceneObject);

            var cylinderPosition2 = cylinderPosition1 + new Vector3d(0, -2, 0);
            var cylinder2 = new Cylinder(cylinderPosition2, dir, 10, 14, sceneObject);

            var cross1 = new Box(cylinderPosition2 + new Vector3d(-3, 0, -3), cylinderPosition2 + new Vector3d(3, 26, 3), sceneObject);

            var cross2 = new Box(cylinderPosition2 + new Vector3d(-7, 18, -3), cylinderPosition2 + new Vector3d(7, 22, 3), sceneObject);

            var cross = new CsgNode(CsgNode.Operations.Union, cross1, cross2);

            var mainUnion = new CsgNode(CsgNode.Operations.Difference, cylinder1, cylinder2);


            var headUnion = new CsgNode(CsgNode.Operations.Union, mainUnion, cross);

            return new CsgNode(CsgNode.Operations.Union, bodyUnion, headUnion);
        }

        public static CsgNode BuildPawn(Vector3d center, ISceneObject sceneObject)
        {
            var dir = new Vector3d(0, 1, 0);

            var base1 = new Cylinder(center, dir, 30, 10, sceneObject);

            var conePosition = center + new Vector3d(0, base1.Height, 0);
            var cone = new Cone(conePosition, dir, 20, 30, sceneObject);

            var spherePosition = conePosition + new Vector3d(0, cone.Height + 6, 0);
            var sphere = new Sphere(spherePosition, 16, sceneObject);

            var mainUnion = new CsgNode(CsgNode.Operations.Union, cone, sphere);

            return new CsgNode(CsgNode.Operations.Union, base1, mainUnion);
        }

        public static CsgNode BuildBishop(Vector3d center, ISceneObject sceneObject)
        {
            var dir = new Vector3d(0, 1, 0);

            var base1 = new Cylinder(center, dir, 30, 6, sceneObject);

            var base2Position = center + new Vector3d(0, base1.Height, 0);
            var base2 = new Cylinder(base2Position, dir, 26, 6, sceneObject);

            var base3Position = base2Position + new Vector3d(0, base2.Height, 0);
            var base3 = new Cylinder(base3Position, dir, 22, 6, sceneObject);

            var baseUniun1 = new CsgNode(CsgNode.Operations.Union, base1, base2);

            var baseUniun = new CsgNode(CsgNode.Operations.Union, baseUniun1, base3);

            var conePosition = base3Position + new Vector3d(0, base3.Height, 0);
            var cone = new Cone(conePosition, dir, 16, 40, sceneObject);

            var colarPosition = conePosition + new Vector3d(0, cone.Height - 4, 0);
            var collar = new Cylinder(colarPosition, dir, 12, 6, sceneObject);

            var mainUnion1 = new CsgNode(CsgNode.Operations.Union, cone, collar);

            var spherePosition = conePosition + new Vector3d(0, cone.Height + 10, 0);
            var sphere = new Sphere(spherePosition, 12, sceneObject);

            var mainUnion2 = new CsgNode(CsgNode.Operations.Union, mainUnion1, sphere);

            var conePosition2 = spherePosition + new Vector3d(0, sphere.Radius - 4, 0);
            var cone2 = new Cone(conePosition2, dir, 6, 10, sceneObject);

            var mainUnion3 = new CsgNode(CsgNode.Operations.Union, mainUnion2, cone2);

            return new CsgNode(CsgNode.Operations.Union, baseUniun, mainUnion3);
        }

        public static CsgNode BuildQueen(Vector3d center, ISceneObject sceneObject)
        {
            var dir = new Vector3d(0, 1, 0);

            var base1 = new Cylinder(center, dir, 30, 6, sceneObject);

            var conePosition1 = center + new Vector3d(0, base1.Height, 0);
            var cone1 = new Cone(conePosition1, dir, 26, 50, sceneObject);

            var planePosition = conePosition1 + new Vector3d(0, cone1.Height - 10, 0);
            var plane = new Plane(dir, planePosition, sceneObject);

            var cutedCone = new CsgNode(CsgNode.Operations.Difference, cone1, plane);

            var baseUniun = new CsgNode(CsgNode.Operations.Union, base1, cutedCone);

            var cylinder = new Cylinder(planePosition, dir, 12, 8, sceneObject);

            var bottomBodyUnion = new CsgNode(CsgNode.Operations.Union, baseUniun, cylinder);


            var oppositeDir = new Vector3d(0, -1, 0);
            var conePosition2 = planePosition + new Vector3d(0, cylinder.Height + 16, 0);
            var cone2 = new Cone(conePosition2, oppositeDir, 14, 26, sceneObject);

            var planePosition2 = planePosition + new Vector3d(0, cylinder.Height, 0);
            var plane2 = new Plane(oppositeDir, planePosition2, sceneObject);

            var cutedCone2 = new CsgNode(CsgNode.Operations.Difference, cone2, plane2);


            var bodyUnion = new CsgNode(CsgNode.Operations.Union, bottomBodyUnion, cutedCone2);


            var spherePosition = conePosition2 + new Vector3d(0, 8, 0);
            var sphere = new Sphere(spherePosition, 12, sceneObject);


            var cylinderPosition1 = spherePosition + new Vector3d(0, 6, 0);
            var cylinder1 = new Cylinder(cylinderPosition1, dir, 10, 6, sceneObject);

            var cylinderPosition2 = cylinderPosition1 + new Vector3d(0, -2, 0);
            var cylinder2 = new Cylinder(cylinderPosition2, dir, 8, 10, sceneObject);

            var crownDiff = new CsgNode(CsgNode.Operations.Difference, cylinder1, cylinder2);

            var bodyWithHeadUnion = new CsgNode(CsgNode.Operations.Union, bodyUnion, sphere);

            return new CsgNode(CsgNode.Operations.Union, bodyWithHeadUnion, crownDiff);
        }

        public static CsgNode BuildRook(Vector3d center, ISceneObject sceneObject)
        {
            var base1Position1 = center + new Vector3d(-20, 0, -20);
            var base1Position2 = center + new Vector3d(20, 10, 20);
            var base1 = new Box(base1Position1, base1Position2, sceneObject);


            var base2Position1 = center + new Vector3d(-16, 10, -16);
            var base2Position2 = center + new Vector3d(16, 60, 16);
            var base2 = new Box(base2Position1, base2Position2, sceneObject);

            var baseUnion = new CsgNode(CsgNode.Operations.Union, base1, base2);

            var topPosition1 = center + new Vector3d(-20, 60, -20);
            var topPosition2 = center + new Vector3d(20, 80, 20);
            var top = new Box(topPosition1, topPosition2, sceneObject);

            // Horizontal 
            var sliceBox1Position1 = center + new Vector3d(24, 75, -11.2);
            var sliceBox1Position2 = center + new Vector3d(-24, 85, -15.2);
            var sliceBox1 = new Box(sliceBox1Position1, sliceBox1Position2, sceneObject);

            var topDiff1 = new CsgNode(CsgNode.Operations.Difference, top, sliceBox1);

            var sliceBox2Position1 = center + new Vector3d(24, 75, -2.4);
            var sliceBox2Position2 = center + new Vector3d(-24, 85, -6.4);
            var sliceBox2 = new Box(sliceBox2Position1, sliceBox2Position2, sceneObject);

            var topDiff2 = new CsgNode(CsgNode.Operations.Difference, topDiff1, sliceBox2);

            var sliceBox3Position1 = center + new Vector3d(24, 75, 6.4);
            var sliceBox3Position2 = center + new Vector3d(-24, 85, 2.4);
            var sliceBox3 = new Box(sliceBox3Position1, sliceBox3Position2, sceneObject);

            var topDiff3 = new CsgNode(CsgNode.Operations.Difference, topDiff2, sliceBox3);

            var sliceBox4Position1 = center + new Vector3d(24, 75, 15.2);
            var sliceBox4Position2 = center + new Vector3d(-24, 85, 11.2);
            var sliceBox4 = new Box(sliceBox4Position1, sliceBox4Position2, sceneObject);

            var topDiff4 = new CsgNode(CsgNode.Operations.Difference, topDiff3, sliceBox4);

            // Vertical
            var sliceBox1Position1V = center + new Vector3d(-11.2, 75, 24);
            var sliceBox1Position2V = center + new Vector3d(-15.2, 85, -24);
            var sliceBox1V = new Box(sliceBox1Position1V, sliceBox1Position2V, sceneObject);

            var topDiff1V = new CsgNode(CsgNode.Operations.Difference, topDiff4, sliceBox1V);

            var sliceBox2Position1V = center + new Vector3d(-2.4, 75, 24);
            var sliceBox2Position2V = center + new Vector3d(-6.4, 85, -24);
            var sliceBox2V = new Box(sliceBox2Position1V, sliceBox2Position2V, sceneObject);

            var topDiff2V = new CsgNode(CsgNode.Operations.Difference, topDiff1V, sliceBox2V);

            var sliceBox3Position1V = center + new Vector3d(6.4, 75, 24);
            var sliceBox3Position2V = center + new Vector3d(2.4, 85, -24);
            var sliceBox3V = new Box(sliceBox3Position1V, sliceBox3Position2V, sceneObject);

            var topDiff3V = new CsgNode(CsgNode.Operations.Difference, topDiff2V, sliceBox3V);

            var sliceBox4Position1V = center + new Vector3d(15.2, 75, 24);
            var sliceBox4Position2V = center + new Vector3d(11.2, 85, -24);
            var sliceBox4V = new Box(sliceBox4Position1V, sliceBox4Position2V, sceneObject);

            var topDiff4V = new CsgNode(CsgNode.Operations.Difference, topDiff3V, sliceBox4V);

            return new CsgNode(CsgNode.Operations.Union, topDiff4V, baseUnion);
        }

        public static CsgNode BuildKnight(Vector3d center, ISceneObject sceneObject)
        {
            return new Box(center + new Vector3d(-10, 0, -10), center + new Vector3d(10, 40, 10), sceneObject);
        }
    }
}