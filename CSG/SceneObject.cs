using CSG.Materials;
using CSG.Shapes;
using Newtonsoft.Json;
using OpenTK;
using OpenTK.Graphics;
using RayMath;

namespace CSG
{
    /// <summary>
    /// Class holding the shape and its boundary box
    /// </summary>
    public class SceneObject
    {
        [JsonIgnore]
        public CSGNode CsgTree { get; set; }
        [JsonIgnore]
        public Box BoundingBox { get; set; }

        public SceneObject(CSGNode tree, Box bbox = null)
        {
            BoundingBox = bbox;
            CsgTree = tree;
        }

        protected SceneObject()
        {

        }

        public Material Material { get; set; }


        /// <summary>
        /// Finds first intersection with this scene object.
        /// </summary>
        /// <param name="ray">Tracing ray</param>
        /// <param name="renderBBox">If true, the bounding box is rendered</param>
        /// <returns>Intersection with scene object.</returns>
        public Intersection IntersectFirst(Ray ray, bool renderBBox = false)
        {
            if (BoundingBox == null)
            {
                return CsgTree.IntersectFirst(ray);
            }


            double t;
            if (BoundingBox.Intersects(ray, out t))
            {
                return renderBBox 
                    ? new Intersection(IntersectionKind.Into, BoundingBox, t) 
                    : CsgTree.IntersectFirst(ray);
            }

            return null;
        }

    }
}
