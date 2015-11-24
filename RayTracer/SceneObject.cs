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
            // could happen that bbox is not used
            if (BoundingBox == null) return CsgTree.IntersectFirst(ray);
            // if hitting bounding box, recurse to try to hit shape ..otherwise none
            double t;
            return ray.Intersects(BoundingBox, out t) ? CsgTree.IntersectFirst(ray) : new Intersection(Intersection.IntersectionKind.None);
        }
    }
}
