namespace RogueNET
{
    public class Wall : Entity
    {
        public Wall()
        {
            SetName("Wall");

            stats = new Stats();
            stats.SetWeight(double.MaxValue);
        }
    }
}