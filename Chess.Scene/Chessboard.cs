using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using CSG;
using CSG.Materials;
using OpenTK;
using OpenTK.Graphics;

namespace Chess.Scene
{
    public class Chessboard : SceneObject
    {
        public const int CroftWidth = 100;
        public const int CroftHeight = 100;
        public const int CroftThickness = 50;


        public static readonly Color4 BackgroundColor = Color4.Black;
        public static readonly Color4 WhiteColor = Color4.White;
        public static readonly Color4 BlackColor = Color4.Black;


        public Chessboard() 
        {
            CsgTree = ObjectBuilder.BuildChessboard(this);
            BoundingBox = new CSG.Shapes.Box(new Vector3d(-100, 0, -100), new Vector3d(900, 50, 900), null);
            Material = new Checker(
                new PhongInfo(WhiteColor, WhiteColor, 0.5, 100),
                new PhongInfo(BlackColor, BlackColor, 0.5, 100),
                CroftWidth,
                CroftHeight);
        }
    }
}
