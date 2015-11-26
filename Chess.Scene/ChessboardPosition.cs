using System;
using Newtonsoft.Json;
using OpenTK;

namespace Chess.Scene
{
    public class ChessboardPosition
    {
        private uint x;
        public uint X {
            get { return x; }
            set
            {
                if (value >= 8)
                    throw new Exception("Invalid position set");

                x = value;
            }
        }

        private uint y;
        public uint Y
        {
            get { return y; }
            set
            {
                if (value >= 8)
                    throw new Exception("Invalid position set");

                y = value;
            }
        }

        public ChessboardPosition()
        {
        }

        public ChessboardPosition(uint x, uint y)
        {
            X = x;
            Y = y;
        }

        [JsonIgnore]
        public Vector3d RealPosition
        {
            get
            {
                var realPos = new Vector3d();
                realPos.X = X*Chessboard.CroftWidth + Chessboard.CroftWidth/2;
                realPos.Z = Y * Chessboard.CroftHeight + Chessboard.CroftHeight / 2;
                realPos.Y = Chessboard.CroftThickness;
                return realPos;
            }
        }
    }
}