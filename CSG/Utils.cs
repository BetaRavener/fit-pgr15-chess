using System.Drawing;
using OpenTK;
using OpenTK.Graphics;

namespace CSG
{
    public class Utils
    {
        public static Vector3d ColorToVector(Color4 color)
        {
            return new Vector3d
            {
                X = color.R / 255.0,
                Y = color.G / 255.0,
                Z = color.B / 255.0,
            };
        }
    }
}