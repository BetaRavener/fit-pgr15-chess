using CSG;

namespace Chess.Scene
{
    public class Figure : SceneObject
    {
        private ChessboardPosition position;
        private FigureType type;

        public Figure(ChessboardPosition position, FigureType type)
        {
            Type = type;
            Position = position;
        }

        public Figure()
        {
        }

        public ChessboardPosition Position
        {
            get { return position; }
            set
            {
                position = value;

                if (Type != FigureType.Unknown)
                {
                    CreateFigureOnScene();
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
                    CreateFigureOnScene();
                }
            }
        }

        private void CreateFigureOnScene()
        {
            var builder = ObjectBuilderResolver.GetBuilder(Type);
            CsgTree = builder.Build(position.RealPosition, this);
            MasterBoundingBox = builder.BuildMasterBoundingBox(position.RealPosition);
            MinorBoundingBoxes = builder.BuildMinorBoundingBoxes(position.RealPosition);
        }
    }
}