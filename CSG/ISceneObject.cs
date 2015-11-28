using OpenTK;
using OpenTK.Graphics;
using RayMath;

namespace CSG
{
    public interface ISceneObject
    {
        /// <summary>
        /// Compute color for given position
        /// </summary>
        /// <param name="position">position of hit on surface of object</param>
        /// <param name="normal">normal vector of hit</param>
        /// <returns>Computed color for position.</returns>
        Vector3d ComputeColor(Vector3d position, Vector3d normal);

        /// <summary>
        /// Finds first intersection with this scene object.
        /// </summary>
        /// <param name="ray">Tracing ray</param>
        /// <param name="renderBBox">If true, the bounding box is rendered</param>
        /// <returns>Intersection with scene object.</returns>
        Intersection IntersectFirst(Ray ray, bool renderBBox = false);
    }
}