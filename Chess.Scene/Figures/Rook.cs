using CSG;
using CSG.Shapes;
using OpenTK;

namespace Chess.Scene.Figures
{
    public class Rook : FigureObject
    {
        public Rook()
        {
            MaxX = 48;
            MaxY = 85;
            MaxZ = 48;
        }

        public override CSGNode Build(Vector3d center, SceneObject sceneObject)
        {
            var base1Position1 = center + new Vector3d(-20, 0, -20);
            var base1Position2 = center + new Vector3d(20, 10, 20);
            var base1 = new Box(base1Position1, base1Position2, sceneObject);


            var base2Position1 = center + new Vector3d(-16, 10, -16);
            var base2Position2 = center + new Vector3d(16, 60, 16);
            var base2 = new Box(base2Position1, base2Position2, sceneObject);

            var baseUnion = new CSGNode(CSGNode.Operations.Union, base1, base2);

            var topPosition1 = center + new Vector3d(-20, 60, -20);
            var topPosition2 = center + new Vector3d(20, 80, 20);
            var top = new Box(topPosition1, topPosition2, sceneObject);

            var topMinusPosition1 = center + new Vector3d(-14, 70, -14);
            var topMinusPosition2 = center + new Vector3d(14, 85, 14);
            var topMinus = new Box(topMinusPosition1, topMinusPosition2, sceneObject);

            var top2 = new CSGNode(CSGNode.Operations.Difference, top, topMinus);

            // Horizontal 
            var sliceBox1Position1 = center + new Vector3d(24, 75, -11.2);
            var sliceBox1Position2 = center + new Vector3d(-24, 85, -15.2);
            var sliceBox1 = new Box(sliceBox1Position1, sliceBox1Position2, sceneObject);

            var topDiff1 = new CSGNode(CSGNode.Operations.Difference, top2, sliceBox1);

            var sliceBox2Position1 = center + new Vector3d(24, 75, -2.4);
            var sliceBox2Position2 = center + new Vector3d(-24, 85, -6.4);
            var sliceBox2 = new Box(sliceBox2Position1, sliceBox2Position2, sceneObject);

            var topDiff2 = new CSGNode(CSGNode.Operations.Difference, topDiff1, sliceBox2);

            var sliceBox3Position1 = center + new Vector3d(24, 75, 6.4);
            var sliceBox3Position2 = center + new Vector3d(-24, 85, 2.4);
            var sliceBox3 = new Box(sliceBox3Position1, sliceBox3Position2, sceneObject);

            var topDiff3 = new CSGNode(CSGNode.Operations.Difference, topDiff2, sliceBox3);

            var sliceBox4Position1 = center + new Vector3d(24, 75, 15.2);
            var sliceBox4Position2 = center + new Vector3d(-24, 85, 11.2);
            var sliceBox4 = new Box(sliceBox4Position1, sliceBox4Position2, sceneObject);

            var topDiff4 = new CSGNode(CSGNode.Operations.Difference, topDiff3, sliceBox4);

            // Vertical
            var sliceBox1Position1V = center + new Vector3d(-11.2, 75, 24);
            var sliceBox1Position2V = center + new Vector3d(-15.2, 85, -24);
            var sliceBox1V = new Box(sliceBox1Position1V, sliceBox1Position2V, sceneObject);

            var topDiff1V = new CSGNode(CSGNode.Operations.Difference, topDiff4, sliceBox1V);

            var sliceBox2Position1V = center + new Vector3d(-2.4, 75, 24);
            var sliceBox2Position2V = center + new Vector3d(-6.4, 85, -24);
            var sliceBox2V = new Box(sliceBox2Position1V, sliceBox2Position2V, sceneObject);

            var topDiff2V = new CSGNode(CSGNode.Operations.Difference, topDiff1V, sliceBox2V);

            var sliceBox3Position1V = center + new Vector3d(6.4, 75, 24);
            var sliceBox3Position2V = center + new Vector3d(2.4, 85, -24);
            var sliceBox3V = new Box(sliceBox3Position1V, sliceBox3Position2V, sceneObject);

            var topDiff3V = new CSGNode(CSGNode.Operations.Difference, topDiff2V, sliceBox3V);

            var sliceBox4Position1V = center + new Vector3d(15.2, 75, 24);
            var sliceBox4Position2V = center + new Vector3d(11.2, 85, -24);
            var sliceBox4V = new Box(sliceBox4Position1V, sliceBox4Position2V, sceneObject);

            var topDiff4V = new CSGNode(CSGNode.Operations.Difference, topDiff3V, sliceBox4V);

            return new CSGNode(CSGNode.Operations.Union, topDiff4V, baseUnion);
        }
    }
}