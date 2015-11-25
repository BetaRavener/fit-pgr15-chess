using CSG;
using CSG.Shapes;
using OpenTK;
using OpenTK.Graphics;
using RayMath;

namespace Chess.Scene
{
    public class ObjectBuilders
    {
        public static CsgNode BuildChessboard()
        {
            var min = new Vector3d(0, 0, 0);
            var max = new Vector3d(min.X + Chessboard.CroftHeight * 8, 50, min.Z + Chessboard.CroftWidth * 8);


            return new Box(min, max);
        }
    }
}