using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSG;
using CSG.Shapes;
using OpenTK;
using OpenTK.Graphics;
using RayMath;

namespace RayTracer
{
    /// <summary>
    /// Class holding the shape and its boundary box
    /// </summary>
    class SceneObject
    {
        public Color4 Color { get; set; }
        public CsgNode CsgTree { get; set; }
        public BoundingBox BoundingBox { get; set; }

        public List<Shape> Shapes { get; }

        public SceneObject(CsgNode tree, Color4 color, BoundingBox bbox = null)
        {
            Color = color;
            CsgTree = tree;
            BoundingBox = bbox;
        }

        /// <summary>
        /// Finds first intersection with this scene object.
        /// </summary>
        /// <param name="ray">Tracing ray</param>
        /// <param name="renderBBox">If true, the bounding box is rendered</param>
        /// <returns>Intersection with scene object.</returns>
        public Intersection IntersectFirst(Ray ray, bool renderBBox = false)
        {
            double t;
            // Test bounding box first, then the shape itself

            if (BoundingBox != null && ray.Intersects(BoundingBox, out t))
            {
                return renderBBox ? new Intersection(Intersection.IntersectionKind.Outfrom, null, t) : CsgTree.IntersectFirst(ray);
            }

            return CsgTree.IntersectFirst(ray);
        }
    }
}
