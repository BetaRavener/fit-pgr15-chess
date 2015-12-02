using OpenTK;
using RayMath;
using System;
using CSG.Materials;
using OpenTK.Graphics;

namespace CSG.Shapes
{
    /// <summary>
    /// Represents general traceable shape.
    /// </summary>
    public abstract class Shape : CSGNode
    {
        private readonly Material fallbackMaterial = new ConstMaterial(new PhongInfo(Color4.Red, Color4.Red)); 

        public Material LocalMaterial { get; set; }

        public SceneObject Parent { get; set; }

        protected Shape(SceneObject sceneObject)
        {
            Parent = sceneObject;
        }

        public virtual Material GetMaterial()
        {  
            return LocalMaterial ?? Parent?.Material ?? fallbackMaterial;
        }

        /// <summary>
        /// Find set of spans at which the ray intersects this shape.
        /// This is an abstract method and must be implemented.
        /// </summary>
        /// <param name="ray">Tracing ray.</param>
        /// <returns>Set of spans.</returns>
        public override RangesShape Intersect(Ray ray)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find surface normal of the shape at specified position.
        /// </summary>
        /// <param name="pos">Position - the normal origin.</param>
        /// <returns>Surface normal vector.</returns>
        public abstract Vector3d Normal(Vector3d pos);       
    }
}
