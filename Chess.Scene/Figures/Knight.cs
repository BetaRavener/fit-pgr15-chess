using System.Collections.Generic;
using CSG;
using CSG.Shapes;
using OpenTK;

namespace Chess.Scene.Figures
{
    public class Knight : FigureObject
    {
        public Knight()
        {
            MaxY = 86;
        }

        public override CSGNode Build(Vector3d center, SceneObject sceneObject)
        {
            const int horseWidth = 10;

            // Base
            var base1 = new Cylinder(center, Vector3d.UnitY, 30, 6, sceneObject);

            var base2Position = center + new Vector3d(0, base1.Height, 0);
            var base2 = new Cone(base2Position, Vector3d.UnitY, 26, 60, sceneObject);

            var base3Position = base2Position + new Vector3d(0, 30, 0);
            var base3 = new Plane(Vector3d.UnitY, base3Position, sceneObject);

            var baseDiff = new CSGNode(CSGNode.Operations.Difference, base2, base3);

            var baseUnion = new CSGNode(CSGNode.Operations.Union, base1, baseDiff);

            // Body
            var box1Position1 = base3Position + new Vector3d(-18, 0, -5);
            var box1Position2 = base3Position + new Vector3d(18, 30, 5); ;
            var box1 = new Box(box1Position1, box1Position2, sceneObject);

            var cylinderPosition = base3Position + new Vector3d(36, 16, -10);
            var cylinder = new Cylinder(cylinderPosition, Vector3d.UnitZ, 22, horseWidth + 10, sceneObject);

            var bodyDiff1 = new CSGNode(CSGNode.Operations.Difference, box1, cylinder);

            var cylinder3Position = base3Position + new Vector3d(-20, 18, -10);
            var cylinder3 = new Cylinder(cylinder3Position, Vector3d.UnitZ, 15, horseWidth + 10, sceneObject);

            var bodyDiff2 = new CSGNode(CSGNode.Operations.Difference, bodyDiff1, cylinder3);

            var box2Position1 = base3Position + new Vector3d(-18, 30, -5);
            var box2Position2 = base3Position + new Vector3d(18, 50, 5); ;
            var box2 = new Box(box2Position1, box2Position2, sceneObject);

            var cylinder2Position = base3Position + new Vector3d(0, 30, -10);
            var cylinder2 = new Cylinder(cylinder2Position, Vector3d.UnitZ, 18, horseWidth + 10, sceneObject);

            var bodyIner1 = new CSGNode(CSGNode.Operations.Intersection, box2, cylinder2);


            var bodyUnion1 = new CSGNode(CSGNode.Operations.Union, bodyDiff2, bodyIner1);

            // head
            var box3Position1 = base3Position + new Vector3d(-26, 20, -5);
            var box3Position2 = base3Position + new Vector3d(-6, 48, 5); ;
            var box3 = new Box(box3Position1, box3Position2, sceneObject);

            var plane1Dir = new Vector3d(-1, -1.7, 0);
            var plane1Position = box3Position1 + new Vector3d(12, 0, 0);
            var plane1 = new Plane(plane1Dir, plane1Position, sceneObject);

            var horseHeadDiff1 = new CSGNode(CSGNode.Operations.Difference, box3, plane1);

            var plane2Dir = new Vector3d(2, -1, 0);
            var plane2Position = box3Position1 + new Vector3d(12, 0, 0);
            var plane2 = new Plane(plane2Dir, plane2Position, sceneObject);

            var horseHeadDiff2 = new CSGNode(CSGNode.Operations.Difference, horseHeadDiff1, plane2);

            var plane3Dir = new Vector3d(-3, 1, 0);
            var plane3Position = box3Position1 + new Vector3d(-2, 0, 0);
            var plane3 = new Plane(plane3Dir, plane3Position, sceneObject);

            var horseHeadDiff3 = new CSGNode(CSGNode.Operations.Difference, horseHeadDiff2, plane3);


            var sphereEye1Position = box3Position1 + new Vector3d(12, 22, 1);
            var sphereEye1 = new Sphere(sphereEye1Position, 2, sceneObject);

            var headUnion1 = new CSGNode(CSGNode.Operations.Union, sphereEye1, horseHeadDiff3);

            var sphereEye2Position = box3Position1 + new Vector3d(12, 22, 9);
            var sphereEye2 = new Sphere(sphereEye2Position, 2, sceneObject);

            var headUnion2 = new CSGNode(CSGNode.Operations.Union, sphereEye2, headUnion1);

            // head rounding 1
            var cylinder1Position = base3Position + new Vector3d(-15, 23, -5);
            var cylinder1 = new Cylinder(cylinder1Position, Vector3d.UnitZ, 2.2, horseWidth, sceneObject);

            //var plane4Dir = new Vector3d(-3, 1, 0);
            var plane4Position = box3Position1 + new Vector3d(0, 1.5, 0);
            var plane4 = new Plane(-Vector3d.UnitY, plane4Position, sceneObject);


            var headRounding1 = new CSGNode(CSGNode.Operations.Difference, headUnion2, plane4);

            var headRounding1Union = new CSGNode(CSGNode.Operations.Union, headRounding1, cylinder1);

            // head rounding 2
            var cylinder4Position = box3Position1 + new Vector3d(3, 7.8, 0);
            var cylinder4 = new Cylinder(cylinder4Position, Vector3d.UnitZ, 2.2, horseWidth, sceneObject);

            var plane5Dir = new Vector3d(-3, -2, 0);
            var plane5Position = box3Position1 + new Vector3d(6.5, 0, 0);
            var plane5 = new Plane(plane5Dir, plane5Position, sceneObject);


            var headRounding2 = new CSGNode(CSGNode.Operations.Difference, headRounding1Union, plane5);

            var headRounding2Union = new CSGNode(CSGNode.Operations.Union, headRounding2, cylinder4);

            // head rounding 3
            var cylinder5Position = box3Position2 + new Vector3d(-11, -2.2, -10);
            var cylinder5 = new Cylinder(cylinder5Position, Vector3d.UnitZ, 2.2, horseWidth, sceneObject);

            var plane6Dir = new Vector3d(-1, 1, 0);
            var plane6Position = box3Position2 + new Vector3d(-11.5, 0, 0);
            var plane6 = new Plane(plane6Dir, plane6Position, sceneObject);


            var headRounding3 = new CSGNode(CSGNode.Operations.Difference, headRounding2Union, plane6);

            var headRounding3Union = new CSGNode(CSGNode.Operations.Union, headRounding3, cylinder5);

            // horns
            var coneHorn1Position = box3Position1 + new Vector3d(8, 25, 2);
            var coneHorn1Dir = new Vector3d(-2, 0, 1);
            var coneHorn1 = new Cone(coneHorn1Position, coneHorn1Dir, 2, 5, sceneObject);

            var headUnion3 = new CSGNode(CSGNode.Operations.Union, coneHorn1, headRounding3Union);

            var coneHorn2Position = coneHorn1Position + new Vector3d(0, 0, 6);
            var coneHorn2 = new Cone(coneHorn2Position, coneHorn1Dir, 2, 5, sceneObject);

            var headUnion4 = new CSGNode(CSGNode.Operations.Union, coneHorn2, headUnion3);


            // Main Union

            // Body Head union
            var bodyUnion2 = new CSGNode(CSGNode.Operations.Union, bodyUnion1, headUnion4);

            var box4Position1 = base3Position + new Vector3d(-6, 40, -5);
            var box4Position2 = base3Position + new Vector3d(0, 48, 5); ;
            var box4 = new Box(box4Position1, box4Position2, sceneObject);

            var bodyUnion3 = new CSGNode(CSGNode.Operations.Union, bodyUnion2, box4);

            return new CSGNode(CSGNode.Operations.Union, baseUnion, bodyUnion3);
        }

        public override List<Box> BuildBoundingBox(Vector3d pos)
        {
            var bboxes = new List<Box>();

            var minX = pos.X - 26;
            var minZ = pos.Z - 13;
            var minY = pos.Y + 36;

            var maxX = pos.X + 22;
            var maxZ = pos.Z + 13;
            var maxY = pos.Y + 86;

            bboxes.Add(new Box(new Vector3d(minX, minY, minZ), new Vector3d(maxX, maxY, maxZ), null));

            minX = pos.X - 30;
            minZ = pos.Z - 30;
            minY = pos.Y;

            maxX = pos.X + 30;
            maxZ = pos.Z + 30;
            maxY = pos.Y + 36;

            bboxes.Add(new Box(new Vector3d(minX, minY, minZ), new Vector3d(maxX, maxY, maxZ), null));

            return bboxes;
        }
    }
}