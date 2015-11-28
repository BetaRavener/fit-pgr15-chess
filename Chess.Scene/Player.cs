using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;

namespace Chess.Scene
{
    public class Player
    {
        public Vector3d Color { get; set; }

        public string Name { get; set; }

        public IList<Figure> Figures { get; private set; }

        public Player()
        {
            Figures = new List<Figure>();
        }

        public void CreateFigure(ChessboardPosition position, FigureType type)
        {
            var figure = new Figure(position, type) {Color = Color};

            Figures.Add(figure);
        }
    }
}