using System;
using System.Collections.Generic;
using System.Linq;
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
        public Box MasterBoundingBox { get; set; }

        [JsonIgnore]
        public List<Box> MinorBoundingBoxes { get; set; }

        public SceneObject(CSGNode tree, Box bbox = null)
        {
            MasterBoundingBox = bbox;
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
            if (MasterBoundingBox == null)
            {
                return CsgTree.IntersectFirst(ray);
            }


            double t = double.PositiveInfinity;
            double min = double.PositiveInfinity;
            Box bb = null;
            if (MasterBoundingBox.Intersects(ray, out t))
            {
                if (MinorBoundingBoxes == null)
                { 
                    bb = MasterBoundingBox;
                    min = t;
                }
                else
                {
                    foreach (var boundingbox in MinorBoundingBoxes.Where(boundingbox => boundingbox.Intersects(ray, out t)))
                    {
                        if (t < min)
                        {
                            bb = boundingbox;
                            min = t;
                        }
                    }
                }
            }

            if (bb != null)
            {
                return renderBBox
                    ? new Intersection(IntersectionKind.Into, bb, min)
                    : CsgTree.IntersectFirst(ray);
            }

            return null;
        }
    }
}
