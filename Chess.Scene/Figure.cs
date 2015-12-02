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
        public double MaxY = 100;
        public double MaxX = 60;
        public double MaxZ = 60;

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
            switch (Type)
            {
                case FigureType.Pawn:
                    MaxY = 62;
                    break;
                case FigureType.Rook:
                    MaxX = 48;
                    MaxY = 85;
                    MaxZ = 48;
                    break;
                case FigureType.Knight:
                    MaxY = 86;
                    break;
                case FigureType.Bishop:
                    MaxY = 86;
                    break;
                case FigureType.King:
                    MaxY = 96;
                    break;
                case FigureType.Queen:
                    MaxY = 92;
                    break;
            }

            Position = position;
        }

        public Figure()
        {
        }

        public override Color4 ComputeColor(Vector3d pos, Vector3d normal)
        {
            return Color;
        }

        public override double Shininess => 6;

        public override double Reflectance => 0.5;

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