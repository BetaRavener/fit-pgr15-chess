using OpenTK;

namespace RayMath
{
    public static class Operations
    {
        /// <summary>
        /// </summary>
        /// <param name="v"></param>
        /// <param name="normal"></param>
        /// <returns></returns>
        /// <link>http://www.3dkingdoms.com/weekly/weekly.php?a=2</link>
        public static Vector3d Reflect(this Vector3d v, Vector3d normal)
        {
            return -2*Vector3d.Dot(v, normal)*normal + v;
        }
    }
}