namespace RogueNET
{
    public class Program
    {
        #if CONSOLE
        private static void Main(string[] args)
        {
            RogueNET rogueNET = new RogueNET(args);
        }
        #endif
    }
}
