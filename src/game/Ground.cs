namespace RogueNET
{
    public class Ground : Entity
    {
        public Ground()
        {
            SetSymbol('.');
            SetName("Ground");

            stats = new Stats();
            stats.SetWeight(double.MaxValue);
        }
    }
}
