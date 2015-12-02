using OpenTK;
using OpenTK.Graphics;

namespace RayTracer
{
    public class LightSource
    {
        private Color4 _color;

        public LightSource() : this(0, 0, 0)
        {
        }

        public LightSource(int x, int y, int z)
        {
            Position = new Vector3d(x, y, z);
            Color = Color4.White;
        }

        public Vector3d Position { get; set; }

        public Color4 Color
        {
            get { return _color; }
            set
            {
                _color = value;
                AmbientColor = value.Times(0.1f);
            }
        }

        public Color4 AmbientColor { get; private set; }
    }
}