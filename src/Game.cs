using System;
using System.IO;

public class Game
{
    ConsoleManager console;
    RuntimeConfig config;

    public Game(RuntimeConfig config)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        console = new ConsoleManager();
        this.config = config;
        Console.Clear();
        Console.Write(GenerateDungeon());

        while (true)
        {
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
            {
                break;
            }
            if (key.Key == ConsoleKey.G)
            {
                config.Seed = RogueNET.RNG.Next(int.MinValue, int.MaxValue);
                Console.Clear();
                Console.Write(GenerateDungeon());
            }
        }
    }

    string GenerateDungeon()
    {
        var width = config.GridWidth;
        var depth = config.GridDepth;

        var grid = new Grid(width, depth);
        var builder = new DungeonBuilder(grid, minWidth: config.RoomMinWidth, minDepth: config.RoomMinDepth);
        builder.DiscardRooms(config.RoomDiscardChance);
        foreach (var room in builder.RoomsData)
        {
            new RoomBuilder(grid, room);
        }
        builder.ConnectRooms();

        var output = console.GetASCIIOnly(grid);
        File.WriteAllText("generated_dungeon_rooms.txt", output);
        return output;
    }
}
