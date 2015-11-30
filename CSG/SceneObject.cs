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
        public Color4 Color { get; set; }
        [JsonIgnore]
        public CsgNode CsgTree { get; set; }
        [JsonIgnore]
        public Box BoundingBox { get; set; }

        public SceneObject(CsgNode tree, Color4 color, Box bbox = null)
        {
            Color = color;
            BoundingBox = bbox;
            CsgTree = tree;
        }

        protected SceneObject()
        {

        }

        /// <summary>
        /// Compute color for given position
        /// </summary>
        /// <param name="position">position of hit on surface of object</param>
        /// <param name="normal">normal vector of hit</param>
        /// <returns>Computed color for position.</returns>
        public virtual Color4 ComputeColor(Vector3d position, Vector3d normal)
        {
            return Color;
        }

        public virtual double Shininess => 0;

        public virtual double Reflectance => 0;

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


            return new Intersection(IntersectionKind.None);
        }

    }
}
