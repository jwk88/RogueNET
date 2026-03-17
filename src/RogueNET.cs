using System;

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
        if (command.Args[0].String == "test")
        {
            var test = tests.Tests[command.Args[1].String];
            var output = tests.RunTest(test);
            Console.Clear();
            Console.WriteLine(output);
        }
    }
}