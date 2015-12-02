using Chess.Scene.Figures;

namespace Chess.Scene
{
    public class ObjectBuilderResolver
    {
        public static FigureObject GetBuilder(FigureType type)
        {
            switch (type)
            {
                case FigureType.King:
                    return new King();
                case FigureType.Bishop:
                    return new Bishop();
                case FigureType.Pawn:
                    return new Pawn();
                case FigureType.Queen:
                    return new Queen();
                case FigureType.Rook:
                    return new Rook();
                case FigureType.Knight:
                    return new Knight();
            }
            return null;
        }
    }
}