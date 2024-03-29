﻿using System.Collections.Generic;
using CSG;
using CSG.Shapes;
using OpenTK;

namespace Chess.Scene.Figures
{
    public abstract class FigureObject
    {
        public double MaxX = 60;
        public double MaxY = 100;
        public double MaxZ = 60;

        public abstract CSGNode Build(Vector3d center, SceneObject sceneObject);

        public virtual Box BuildMasterBoundingBox(Vector3d pos)
        {
            var minX = pos.X - MaxX/2;
            var minZ = pos.Z - MaxZ/2;
            var minY = pos.Y;

            var maxX = pos.X + MaxX/2;
            var maxZ = pos.Z + MaxZ/2;
            var maxY = pos.Y + MaxY;

            return new Box(new Vector3d(minX, minY, minZ), new Vector3d(maxX, maxY, maxZ), null);
        }

        public virtual List<Box> BuildMinorBoundingBoxes(Vector3d pos)
        {
            return null;
        }
    }
}