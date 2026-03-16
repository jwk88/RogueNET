using System;

public class RogueNET
{
    public static Random RNG;
    public static int seed = 1;

    public RogueNET()
    {
        RNG = new Random(seed);

        // TODO: make it CLI adjustable which test is run
        var tests = new TestManager();
        tests.RunTests(initialize: true);
    }

    public void Run()
    {
        Console.Clear();
    }
}