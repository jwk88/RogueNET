public class Roof : Entity
{
    public Roof()
    {
        SetName("Roof");

        stats = new Stats();
        stats.SetWeight(double.MaxValue);
    }
}