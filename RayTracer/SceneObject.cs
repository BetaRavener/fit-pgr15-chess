using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSG;
using CSG.Shapes;
using RayMath;

namespace RayTracer
{
    /// <summary>
    /// Class holding the shape and its boundary box
    /// </summary>
    class SceneObject
    {
        public Shape Shape { get; set; }
        public BoundingBox BoundingBox { get; set; }

        public SceneObject(Shape shape, BoundingBox bbox)
        {
            Shape = shape;
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
            if (ray.Intersects(BoundingBox, out t))
            {
                if (renderBBox)
                    return new Intersection(Intersection.IntersectionKind.Outfrom, null, t);
                else
                    return Shape.IntersectFirst(ray);
            }

            return new Intersection(Intersection.IntersectionKind.None);
        }
    }
}
