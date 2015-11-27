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
            }
            return null;
        }

    }
}