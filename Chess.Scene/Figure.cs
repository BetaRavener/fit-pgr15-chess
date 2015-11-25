using CSG;
using OpenTK;

namespace Chess.Scene
{
    public class Figure : SceneObject
    {
        public const int Height = 50;
        public ChessboardPosition Position { get; private set; } 

        public FigureType Type { get; private set; }

        public Figure(ChessboardPosition position, FigureType type)
            : base(ObjectBuilderResolver.BuildFigure(type, position.RealPosition))
        {
            Type = type;
            Position = position;
        }

    }
}