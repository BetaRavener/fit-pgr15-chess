using System.Collections.Generic;
using CSG.Materials;
using OpenTK.Graphics;

namespace Chess.Scene
{
    public class Player
    {
        private Color4 _color;

        private Material _figureMaterial;

        public Player()
        {
            Figures = new List<Figure>();
        }

        public Color4 Color
        {
            get { return _color; }
            set
            {
                _color = value;
                _figureMaterial = new ConstMaterial(new PhongInfo(_color, _color, Color4.White, 6, 0.1));
            }
        }

        public string Name { get; set; }

        public IList<Figure> Figures { get; }


        public void CreateFigure(ChessboardPosition position, FigureType type)
        {
            var figure = new Figure(position, type)
            {
                Material = _figureMaterial
            };

            Figures.Add(figure);
        }
    }
}