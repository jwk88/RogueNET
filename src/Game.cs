using System;
using System.IO;

public class Game
{
    ConsoleManager console;
    RuntimeConfig config;
    Grid grid;
    DungeonBuilder dungeon;
    Player player;

    public Game(RuntimeConfig config)
    {
        this.config = config;

        Console.OutputEncoding = System.Text.Encoding.UTF8;
        console = new ConsoleManager();
        GenerateDungeon();
        
        player = new EntityBuilder<Player>(grid, dungeon.GetActiveRooms[0].Origin).Build();

        Console.Clear();
        Console.Write(console.GetASCIIOnly(grid));

        while (true)
        {
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
                break;

            if (key.Key == ConsoleKey.RightArrow)
                player.StepRight(1);

            if (key.Key == ConsoleKey.LeftArrow)
                player.StepLeft(1);

            if (key.Key == ConsoleKey.DownArrow)
                player.StepDown(1);

            if (key.Key == ConsoleKey.UpArrow)
                player.StepUp(1);
            
            if (key.Key == ConsoleKey.I)
                player.Interract(player.Direction.X, player.Direction.Y);

            Console.Clear();
            Console.Write(console.GetASCIIOnly(grid));
            while (Log.logs.Count > 0)
            {
                Console.WriteLine(Log.logs.Dequeue());
            }
        }
    }

    void GenerateDungeon()
    {
        var width = config.GridWidth;
        var depth = config.GridDepth;

        grid = new Grid(width, depth);
        dungeon = new DungeonBuilder(grid, minWidth: config.RoomMinWidth, minDepth: config.RoomMinDepth);
        dungeon.DiscardRooms(config.RoomDiscardChance);
        foreach (var room in dungeon.GetActiveRooms)
        {
            new RoomBuilder(grid, room);
        }
        dungeon.ConnectRooms();
    }
}
