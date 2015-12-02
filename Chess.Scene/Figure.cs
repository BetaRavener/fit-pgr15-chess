using System;
using System.Media;
using CSG;
using CSG.Materials;
using OpenTK;
using OpenTK.Graphics;
using RayMath;

namespace Chess.Scene
{
    public class Figure : SceneObject
    {
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

        public Figure(ChessboardPosition position, FigureType type)
        {
            Type = type;
            Position = position;
        }

        public Figure()
        {
        }

        private void CreateFigureOnScene()
        {
            var builder = ObjectBuilderResolver.GetBuilder(Type);
            CsgTree = builder.Build(position.RealPosition, this);
            BoundingBoxes.AddRange(builder.BuildBoundingBox(position.RealPosition));
        }
    }
}