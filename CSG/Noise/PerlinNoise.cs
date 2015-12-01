namespace CSG.Noise
{
    // Source: Aurelius
    public class PerlinNoise<TNoise> : Noise 
        where TNoise : Noise, new()
    {
        TNoise noise;
        int count;
        double alpha, beta;
        public PerlinNoise()
        {
            alpha = 2.0; beta = 2.0; count = 5;
            noise = new TNoise();
        }
        public override double Eval(double x, double y, double z)
        {
            var sum = 0.0;
            var scale = 1.0;

            for (var i = 0; i < count; i++)
            {
                var val = noise.Eval(x, y, z);
                sum += val / scale;

                scale *= alpha;
                x *= beta;
                y *= beta;
                z *= beta;
            }
            return sum;
        }
    }
}