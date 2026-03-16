using System;

public class RogueNET
{
    public RogueNET()
    {
        var tests = new TestManager();
        tests.RunTests(initialize: false);
    }

    public void Run()
    {
        Console.Clear();
    }
}
