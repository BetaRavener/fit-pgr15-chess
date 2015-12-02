using System;
using Newtonsoft.Json;
using OpenTK;

namespace Chess.Scene
{
    public class ChessboardPosition
    {
        private uint x;

        private uint y;

        public ChessboardPosition()
        {
        }

        public ChessboardPosition(uint x, uint y)
        {
            X = x;
            Y = y;
        }

        public uint X
        {
            get { return x; }
            set
            {
                if (value >= 8)
                    throw new Exception("Invalid position set");

                x = value;
            }
        }

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

        [JsonIgnore]
        public Vector3d RealPosition
        {
            get
            {
                var realPos = new Vector3d
                {
                    X = X*Chessboard.CroftWidth + Chessboard.CroftWidth/2,
                    Z = Y*Chessboard.CroftHeight + Chessboard.CroftHeight/2,
                    Y = Chessboard.CroftThickness
                };
                return realPos;
            }
        }
    }
}