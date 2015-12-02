using System.Collections.Generic;
using CSG.Materials;
using OpenTK.Graphics;

namespace Chess.Scene
{
    public class Player
    {
        public Color4 Color { get; set; }

        public string Name { get; set; }

        public IList<Figure> Figures { get; private set; }

        public Player()
        {
            Figures = new List<Figure>();
        }

        public void CreateFigure(ChessboardPosition position, FigureType type)
        {
            var figure = new Figure(position, type)
            {
                Material = new ConstMaterial(new PhongInfo(Color, Color, Color4.White, 6, 0.1))
            };

            Figures.Add(figure);
        }
    }
}