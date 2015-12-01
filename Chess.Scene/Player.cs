using System.Collections.Generic;
using CSG.Material;
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
            var material = new GlassMaterial(new CSG.Color.Color(Color));

            var figure = new Figure(position, type) {
                Material= material
            };

            Figures.Add(figure);
        }
    }
}