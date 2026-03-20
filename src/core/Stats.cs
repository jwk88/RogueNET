namespace RogueNET
{
    using System;

    public class Stats
    {
        double weightKilograms;

        public double WeightKG => weightKilograms;

        public Stats()
        {
            weightKilograms = Definitions.entityDefaultWeightKG;
        }

        public void SetWeight(double kilograms)
        {
            weightKilograms = kilograms;
        }

        public void SetWeightByBellCurve(double min, double max)
        {
            weightKilograms = RandomGaussian((float)min, (float)max);
        }

        public float RandomGaussian(float minValue = 0.0f, float maxValue = 1.0f)
        {
            float u, v, S;
            do
            {
                u = 2.0f * (float)RogueNET.RNG.NextDouble() - 1.0f;
                v = 2.0f * (float)RogueNET.RNG.NextDouble() - 1.0f;
                S = u * u + v * v;
            }
            while (S >= 1.0f);
            float std = u * (float)Math.Sqrt(-2.0f * (float)Math.Log(S) / S);
            float mean = (minValue + maxValue) / 2.0f;
            float sigma = (maxValue - mean) / 3.0f;
            return Math.Clamp(std * sigma + mean, minValue, maxValue);
        }
    }
}
