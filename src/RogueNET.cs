using System;
using System.IO;

public class RogueNET
{
    public static Random RNG;
    public static int seed = 1;
    public static TestManager tests;

    public RogueNET(params string[] args)
    {
        Console.Clear();

        RNG = new Random(seed);
        tests = new TestManager();

        var command = new Command(args);
        if (command.Args.Length > 0)
        {
            if (command.Args[0].String == "test")
            {
                var test = tests.Tests[command.Args[1].String];
                var testOutput = tests.RunTest(test);
                Console.Clear();
                Console.WriteLine(testOutput);
            }
            if (command.Args[0].String == "seed")
            {
                RNG = new Random(command.Args[1].Integer.Value);
            }
        }

        var grid = new Grid(128, 64);
        var dungeonBuilder = new DungeonBuilder(grid, minWidth: 24, minDepth: 8);
        var rooms = dungeonBuilder.GetRooms;
        for (int i = 0; i < rooms.Count; i++)
        {
            var rng = RNG.Next(0, 100);
            if (rng < 5)
            {
                rooms.RemoveAt(i);
            }
        }

        foreach (var room in rooms) new RoomBuilder(grid, room);

        var console = new ConsoleManager();
        var output = console.GetASCIIOnly(grid);
        File.WriteAllText("generated_dungeon_rooms.txt", output);
    }
}