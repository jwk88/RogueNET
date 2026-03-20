namespace RogueNET
{
    public class Player : Actor
    {
        public Player()
        {
            SetSymbol('@');
            SetName("John Doe");

            stats = new Stats();
            stats.SetWeight(70);
        }
    }
}