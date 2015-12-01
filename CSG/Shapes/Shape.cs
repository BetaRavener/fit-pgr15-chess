using OpenTK;
using RayMath;
using System;
using Chess.Scene.Color;
using CSG.Color;
using CSG.Material;
using CSG.Noise;
using OpenTK.Graphics;

namespace CSG.Shapes
{
    /// <summary>
    /// Represents general traceable shape.
    /// </summary>
    public abstract class Shape : CsgNode
    {
        public static Color4 FallbackColor = Color4.Red;
        public static double FallbackShininess = 0;
        public static double FallbackReflectance = 0;

        private Material.Material material;

        public SceneObject Parent { get; set; }

        protected Shape(SceneObject sceneObject)
        {
            Parent = sceneObject;

/*
            var colorMap = new ColorMap(); // diffuse
            colorMap.AddSegment(new ColorMap.Segment(0, 0.6, new Color.Color(0.2, 0.25, 0.7), new Color.Color(0.2, 0.25, 0.7)));
            colorMap.AddSegment(new ColorMap.Segment(0.6, 0.7, new Color.Color(0.2, 0.25, 0.7), new Color.Color(0.3, 0.3, 0.36)));



            ColorMap cm2 = new ColorMap();
            cm2.AddSegment(new ColorMap.Segment(0, 0.6, new Color.Color(0.2, 0.25, 0.7), new Color.Color(0.1, 0.1, 0.2)));

            var cm = new PhongMap(colorMap, cm2, 0.3);

            test = new NoiseDisplace(
                new Wood(cm, new Vector3d(0, -1.5, 0), new Vector3d(0.1, 0, 1), 5),
                new PerlinNoise<ImprovedNoise>(), 
                0.1);*/
            //test = new Checker(new PhongInfo(new Color.Color(Color4.White), new Color.Color(Color4.White)),
             //   new PhongInfo(new Color.Color(Color4.Black), new Color.Color(Color4.Black));

        }

        public virtual Color4 Color(Vector3d position, Vector3d normal)
        {  
            return Parent?.ComputeColor(position, normal) ?? FallbackColor;
        }

        public Material.Material Material => Parent?.Material ?? Material;
        

        static Material.Material GetMatClouds()
        {

            var colorMap = new ColorMap(); // diffuse
            colorMap.AddSegment(new ColorMap.Segment(0, 0.6, new Color.Color(0.2, 0.25, 0.7), new Color.Color(0.2, 0.25, 0.7)));
            colorMap.AddSegment(new ColorMap.Segment(0.6, 0.7, new Color.Color(0.2, 0.25, 0.7), new Color.Color(0.3, 0.3, 0.36)));
            colorMap.AddSegment(new ColorMap.Segment(0.7, 1.0, new Color.Color(0.3, 0.3, 0.36), new Color.Color(0.8, 0.8, 0.38)));


            return new NoiseDisplace(new Clouds(new PhongMap(colorMap, new ColorMap(new CSG.Color.Color(0.15, 0.15, 0.15))), 0.3),
                                     new PerlinNoise<ImprovedNoise>(), 0.0);

         //   var colorMAp = new ColorMap();
         //   colorMAp.AddSegment(new CSG.Color.ColorMap.Segment(0, 1, new CSG.Color.Color(0, 0, 0), new CSG.Color.Color(1, 1, 1)));
           
         //return new Clouds(new PhongMap(colorMAp , // diffuse
         //                              new ColorMap(new CSG.Color.Color(0.1, 0.1, 0.1))), 0.1);// ambient
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
