public class Corner : Entity
{
    public Corner()
    {
        SetSymbol('*');
        SetName("Corner");

        stats = new Stats();
        stats.SetWeight(double.MaxValue);
    }
}