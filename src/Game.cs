using System;
using System.IO;

public class Game
{
    public Game(RuntimeConfig config)
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

        var console = new ConsoleManager();
        var output = console.GetASCIIOnly(grid);
        File.WriteAllText("generated_dungeon_rooms.txt", output);

        Console.Clear();
        Console.Write(output);
        while (true)
        {
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
            {
                break;
            }
        }
    }
}
