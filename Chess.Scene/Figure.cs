using System;
using System.Media;
using CSG;
using OpenTK;
using OpenTK.Graphics;

namespace Chess.Scene
{
    public class Figure : SceneObject
    {
        public const int Height = 50;

        private ChessboardPosition position;
        private FigureType type;

        public ChessboardPosition Position
        {
            get { return position; }
            set
            {
                position = value;

                if (Type != FigureType.Unknown)
                {
                    SetCsgTree(ObjectBuilderResolver.BuildFigure(Type, Position.RealPosition));
                }
            }
        }

        public FigureType Type
        {
            get { return type; }
            set
            {
                type = value;

                if (Position != null)
                {
                    SetCsgTree(ObjectBuilderResolver.BuildFigure(Type, position.RealPosition));
                }
            }
        }

        public Player Player { get; set; }

        public Figure(ChessboardPosition position, FigureType type)
        {
            Type = type;
            Position = position;
        }

        public Figure()
        {

        }

        public override Color4 ComputeColor(Vector3d position, Vector3d normal)
        {
            return Player.Color;
        }
    }
}