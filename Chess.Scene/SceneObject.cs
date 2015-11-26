using CSG;
using CSG.Shapes;
using Newtonsoft.Json;
using OpenTK;
using OpenTK.Graphics;
using RayMath;

namespace Chess.Scene
{
    /// <summary>
    /// Class holding the shape and its boundary box
    /// </summary>
    public class SceneObject : ISceneObject
    {
        [JsonIgnore]
        public Color4 Color { get; set; }
        [JsonIgnore]
        public CsgNode CsgTree { get; set; }
        [JsonIgnore]
        public BoundingBox BoundingBox { get; set; }

        public SceneObject(CsgNode tree, Color4 color, BoundingBox bbox = null)
        {
            Color = color;
            BoundingBox = bbox;

            SetCsgTree(tree);
        }

        public SceneObject(CsgNode tree)
        {
            SetCsgTree(tree);
        }

        protected SceneObject()
        {

        }

        public virtual Color4 ComputeColor(Vector3d position, Vector3d normal)
        {
            return Color;
        }

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
            if (ray.Intersects(BoundingBox, out t))
            {
                return renderBBox 
                    ? new Intersection(IntersectionKind.Outfrom, null, t) 
                    : CsgTree.IntersectFirst(ray);
            }


            return new Intersection(IntersectionKind.None);
        }

        protected void SetCsgTree(CsgNode tree)
        {
            CsgTree = tree;

            Utilities.SetParentTree(tree, this);
        }

    }
}
