using CSG;
using OpenTK;

namespace Chess.Scene
{
    public class ObjectBuilderResolver
    {
        public static CsgNode BuildFigure(FigureType type, Vector3d position, ISceneObject sceneObject)
        {
            switch (type)
            {
                case FigureType.King:
                    return ObjectBuilder.BuildKing(position, sceneObject);
                case FigureType.Bishop:
                    return ObjectBuilder.BuildBishop(position, sceneObject);
                case FigureType.Pawn:
                    return ObjectBuilder.BuildPawn(position, sceneObject);
                case FigureType.Queen:
                    return ObjectBuilder.BuildQueen(position, sceneObject);
                case FigureType.Rook:
                    return ObjectBuilder.BuildRook(position, sceneObject);
                case FigureType.Knight:
                    return ObjectBuilder.BuildKnight(position, sceneObject);
            }
            return null;
        }

    }
}