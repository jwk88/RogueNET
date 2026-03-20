namespace RogueNET
{
    using System;

    public class Game
    {
        ConsoleManager console;
        RuntimeConfig config;

        public Grid Grid                { get; private set; }
        public DungeonBuilder Dungeon   { get; private set; }
        public Player Player            { get; private set; }

        public Game(RuntimeConfig config)
        {
            this.config = config;

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            console = new ConsoleManager();
            GenerateDungeon();
        
            Player = new EntityBuilder<Player>(Grid, Dungeon.GetActiveRooms[0].Origin).Build();
        }

        public void RunFromCLI()
        {
            Console.Clear();
            Console.Write(console.GetASCIIOnly(Grid));

            while (true)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                    break;

                if (key.Key == ConsoleKey.RightArrow)
                    Player.StepRight(1);

                if (key.Key == ConsoleKey.LeftArrow)
                    Player.StepLeft(1);

                if (key.Key == ConsoleKey.DownArrow)
                    Player.StepDown(1);

                if (key.Key == ConsoleKey.UpArrow)
                    Player.StepUp(1);
            
                if (key.Key == ConsoleKey.I)
                    Player.Interract(Player.Direction.X, Player.Direction.Y);

                Console.Clear();
                Console.Write(console.GetASCIIOnly(Grid));
                while (Log.logs.Count > 0)
                {
                    Console.WriteLine(Log.logs.Dequeue());
                }
            }
        }

        public void GenerateDungeon()
        {
            var width = config.GridWidth;
            var depth = config.GridDepth;

            Grid = new Grid(width, depth);
            Dungeon = new DungeonBuilder(Grid, minWidth: config.RoomMinWidth, minDepth: config.RoomMinDepth);
            Dungeon.DiscardRooms(config.RoomDiscardChance);
            foreach (var room in Dungeon.GetActiveRooms)
            {
                new RoomBuilder(Grid, room);
            }

            Dungeon.ConnectRooms();
        }
    }
}
