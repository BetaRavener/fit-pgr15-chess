using CSG;
using CSG.Materials;
using CSG.Shapes;
using OpenTK;
using OpenTK.Graphics;

namespace Chess.Scene
{
    public class ObjectBuilder
    {
        public static CSGNode BuildChessboard(SceneObject sceneObject)
        {
            var min = new Vector3d(0, 0, 0);
            var max = new Vector3d(min.X + Chessboard.CroftHeight*8,
                Chessboard.CroftThickness,
                min.Z + Chessboard.CroftWidth*8);

            var box = new Box(min, max, sceneObject);

            var borderMin1 = min + new Vector3d(-10, 0, -10);
            var borderMax1 = max + new Vector3d(10, -1, 10);

            var border1 = new Box(borderMin1, borderMax1, sceneObject)
            {
                LocalMaterial = new ConstMaterial(new PhongInfo(Color4.Red, Color4.Red, 0.5, 100))
            };

            var borderMin2 = min + new Vector3d(-Chessboard.CroftHeight, 0, -Chessboard.CroftWidth);
            var borderMax2 = max + new Vector3d(Chessboard.CroftHeight, -2, Chessboard.CroftWidth);

            var border2 = new Box(borderMin2, borderMax2, sceneObject)
            {
                LocalMaterial =
                    new ConstMaterial(new PhongInfo(new Color4(61, 21, 8, 0), new Color4(61, 21, 8, 0), 0, 100))
            };


            var union = new CSGNode(CSGNode.Operations.Union, box, border1);

            return new CSGNode(CSGNode.Operations.Union, union, border2);
        }
    }
}