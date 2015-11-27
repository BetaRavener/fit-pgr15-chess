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


            return new Box(min, max, sceneObject);
        }

        public static CsgNode BuildKing(Vector3d center, ISceneObject sceneObject)
        {
            var dir = new Vector3d(0, 1, 0);

            return new Cylinder(center, dir, 20, Figure.MaxY, sceneObject);
        }
    }
}