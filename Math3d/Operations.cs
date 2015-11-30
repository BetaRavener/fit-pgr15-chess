﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RayMath
{
    public static class Operations
    {
        private const double Epsilon = 0.000001f;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="bb"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        /// <link>https://en.m.wikipedia.org/wiki/M%C3%B6ller%E2%80%93Trumbore_intersection_algorithm</link>
        public static bool Intersects(this Ray ray, Triangle tri, out double distance)
        {
            // Initialize distance to max in case there's no intersection
            distance = double.MaxValue;

            //Find vectors for two edges sharing V1
            var e1 = Vector3d.Subtract(tri.V2, tri.V1);
            var e2 = Vector3d.Subtract(tri.V3, tri.V1);

            //Begin calculating determinant - also used to calculate u parameter
            var P = Vector3d.Cross(ray.Direction, e2);
            //if determinant is near zero, ray lies in plane of triangle
            var det = Vector3d.Dot(e1, P);
            //NOT CULLING
            if (det > -Epsilon && det < Epsilon)
                return false;

            var invDet = 1.0f / det;

            //calculate distance from V1 to ray origin
            var T = Vector3d.Subtract(ray.Origin, tri.V1);

            //Calculate u parameter and test bound
            var u = Vector3d.Dot(T, P) * invDet;
            //The intersection lies outside of the triangle
            if (u < 0.0f || u > 1.0f)
                return false;

            //Prepare to test v parameter
            var Q = Vector3d.Cross(T, e1);

            //Calculate V parameter and test bound
            var v = Vector3d.Dot(ray.Direction, Q) * invDet;
            //The intersection lies outside of the triangle
            if (v < 0.0f || u + v > 1.0f) return false;

            var t = Vector3d.Dot(e2, Q) * invDet;

            if (t > Epsilon)
            { //ray intersection
                distance = t;
                return true;
            }

            // No hit, no win
            return false;
        }

        public static Vector3d Reflect(this Vector3d v, Vector3d normal)
        {
            return -2 * Vector3d.Dot(v, normal) * normal + v;
        }
    }
}
