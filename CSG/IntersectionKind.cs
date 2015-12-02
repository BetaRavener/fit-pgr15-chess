namespace CSG
{
    public enum IntersectionKind
    {
        /// <summary>
        ///     There was no intersection.
        /// </summary>
        None,

        /// <summary>
        ///     The intersection after which the ray continues into shape.
        /// </summary>
        Into,

        /// <summary>
        ///     The intersection after which the ray leaves shape.
        /// </summary>
        Outfrom
    }
}