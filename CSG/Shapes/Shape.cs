using CSG.Materials;
using OpenTK;
using OpenTK.Graphics;

namespace CSG.Shapes
{
    /// <summary>
    ///     Represents general traceable shape.
    /// </summary>
    public abstract class Shape : CSGNode
    {
        private readonly Material fallbackMaterial = new ConstMaterial(new PhongInfo(Color4.Red, Color4.Red));

        protected Shape(SceneObject sceneObject)
        {
            Parent = sceneObject;
        }

        public Material LocalMaterial { get; set; }

        public SceneObject Parent { get; set; }

        public virtual Material GetMaterial()
        {
            return LocalMaterial ?? Parent?.Material ?? fallbackMaterial;
        }

        /// <summary>
        ///     Find surface normal of the shape at specified position.
        /// </summary>
        /// <param name="pos">Position - the normal origin.</param>
        /// <returns>Surface normal vector.</returns>
        public abstract Vector3d Normal(Vector3d pos);
    }
}