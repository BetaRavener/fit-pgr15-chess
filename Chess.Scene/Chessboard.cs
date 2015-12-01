using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using CSG;
using CSG.Color;
using CSG.Material;
using OpenTK;
using OpenTK.Graphics;

namespace Chess.Scene
{
    public class Chessboard : SceneObject
    {
        public const int CroftWidth = 100;
        public const int CroftHeight = 100;
        public const int CroftThickness = 50;


        public static readonly Color4 BackgroundColor = Color4.Brown;
        public static readonly Color4 WhiteColor = Color4.White;
        public static readonly Color4 BlackColor = Color4.Black;


        public Chessboard() 
        {
            CsgTree = ObjectBuilder.BuildChessboard(this);
            var chessBox = (CSG.Shapes.Box)CsgTree;
            BoundingBox = new CSG.Shapes.Box(chessBox.Min, chessBox.Max, null);

            var phong1 = new PhongInfo(new CSG.Color.Color(WhiteColor), new CSG.Color.Color(WhiteColor), 0.5, 100);
            var phong2 = new PhongInfo(new CSG.Color.Color(BlackColor), new CSG.Color.Color(BlackColor), 0.5, 100);
            Material = new Checker(phong1, phong2, CroftWidth, CroftHeight);
            
    }

        public override Color4 ComputeColor(Vector3d position, Vector3d normal)
        {
            // ToDo: implement faster algorithm
            // Normal vector is pointing to the top
            if (normal.Y > 0 && (int)normal.X == 0)
            {
                var evenX = (int)(position.X / CroftWidth) % 2 == 0;
                var evenZ = (int)(position.Z / CroftWidth) % 2 == 0;
                if((evenX && evenZ) || (!evenX && !evenZ))
                {
                    return BlackColor;
                }
                else
                {
                    return WhiteColor;
                }
            }

            return BackgroundColor;
        }
    }
}
