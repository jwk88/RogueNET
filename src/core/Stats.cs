public class Stats
{
    double weightKilograms;

    public double WeightKG => weightKilograms;

    public Stats()
    {
        weightKilograms = Config.entityDefaultWeightKG;
    }

    public void SetWeight(double kilograms)
    {
        weightKilograms = kilograms;
    }
}
