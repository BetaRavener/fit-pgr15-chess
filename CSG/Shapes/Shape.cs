using OpenTK;
using RayMath;
using System;
using OpenTK.Graphics;

namespace CSG.Shapes
{
    /// <summary>
    /// Represents general traceable shape.
    /// </summary>
    public abstract class Shape : CsgNode
    {
        public ISceneObject Parent { get; set; }

        public Color4 Color { get; set; }
        protected Shape(ISceneObject sceneObject)
        {
            Parent = sceneObject;
        }

        protected Shape(Color4 color)
        {
            Color = color;
        }


        public virtual Color4 GetColor(Vector3d position, Vector3d normal)
        {  
            return Parent?.ComputeColor(position, normal) ?? Color;
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
