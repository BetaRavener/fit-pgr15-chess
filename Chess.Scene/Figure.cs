using System;
using System.Media;
using CSG;
using OpenTK;
using OpenTK.Graphics;
using RayMath;

namespace Chess.Scene
{
    public class Figure : SceneObject
    {
        public const int MaxY = 100;
        public const int MaxX = 60;
        public const int MaxZ = 60;

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

        private void CreateBoundingBox(Vector3d pos)
        {
            var minX = pos.X - (MaxX / 2);
            var minZ = pos.Z - (MaxZ / 2);
            var minY = pos.Y;

            var maxX = pos.X + (MaxX / 2);
            var maxZ = pos.Z + (MaxZ / 2);
            var maxY = pos.Y + MaxY;

            BoundingBox = new CSG.Shapes.Box(new Vector3d(minX, minY, minZ), new Vector3d(maxX, maxY, maxZ), null);
        }

        private void CreateFigureOnScene()
        {
            CsgTree = ObjectBuilderResolver.BuildFigure(Type, Position.RealPosition, this);
            CreateBoundingBox(Position.RealPosition);
        }
    }
}