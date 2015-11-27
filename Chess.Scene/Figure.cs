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
        public const int MaxY = 50;
        public const int MaxX = 50;
        public const int MaxZ = 50;

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

        public Player Player { get; set; }

        public Figure(ChessboardPosition position, FigureType type)
        {
            Type = type;
            Position = position;
        }

        public Figure()
        {
        }

        public override Color4 ComputeColor(Vector3d pos, Vector3d normal)
        {
            return Player.Color;
        }

        private void CreateBoundingBox(Vector3d pos)
        {
            var minX = pos.X - (MaxX / 2);
            var minZ = pos.Z - (MaxZ / 2);
            var minY = pos.Y;

            var maxX = pos.X + (MaxX / 2);
            var maxZ = pos.Z + (MaxZ / 2);
            var maxY = pos.Y + MaxY;

            BoundingBox = new BoundingBox(minX, minY, minZ, maxX, maxY, maxZ);
        }

        private void CreateFigureOnScene()
        {
            SetCsgTree(ObjectBuilderResolver.BuildFigure(Type, Position.RealPosition));
            CreateBoundingBox(Position.RealPosition);
        }
    }
}