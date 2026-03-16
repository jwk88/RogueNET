using System;

public class RogueNET
{
    public static Random RNG;
    public static int seed = 1;

    public RogueNET()
    {
        RNG = new Random(seed);

        var tests = new TestManager();
        tests.RunTests(initialize: false);
    }

    public void Run()
    {
        Console.Clear();
    }
}
