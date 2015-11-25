using CSG;
using CSG.Shapes;
using OpenTK;
using OpenTK.Graphics;
using RayMath;

namespace Chess.Scene
{
    public class ObjectBuilder
    {
        public static CsgNode BuildChessboard()
        {
            var min = new Vector3d(0, 0, 0);
            var max = new Vector3d(min.X + Chessboard.CroftHeight * 8, 
                Chessboard.CroftThickness, 
                min.Z + Chessboard.CroftWidth * 8);


            return new Box(min, max);
        }

        public static CsgNode BuildKing(Vector3d center)
        {
            var dir = new Vector3d(0, 1, 0);

            return new Cylinder(center, dir, 20, Figure.Height, Color4.AliceBlue);
        }
    }
}