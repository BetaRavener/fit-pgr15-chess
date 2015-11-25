using CSG;
using OpenTK;

namespace Chess.Scene
{
    public class ObjectBuilderResolver
    {
        public static CsgNode BuildFigure(FigureType type, Vector3d position)
        {
            switch (type)
            {
                case FigureType.King:
                    return ObjectBuilder.BuildKing(position);
            }
            return null;
        }

    }
}