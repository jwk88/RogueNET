using System;
using System.Collections.Generic;

public class TestManager
{
    Dictionary<string, Test> tests;

    public Dictionary<string,  Test> Tests => tests;

    public TestManager()
    {
        tests = new Dictionary<string, Test>
        {
            { "Test1", new Test1() },
            { "Test1b", new Test1b() }
        };
    }

    public void RunAllTests(bool initialize)
    {
        var testOutput = "";
        foreach (var test in tests)
        {
            testOutput += RunTest(test.Value, initialize);
        }

        Console.Clear();
        Console.WriteLine(testOutput);
        Console.WriteLine("Tests passed, press any key to continue");
        Console.ReadKey();
    }

    public string RunTest(Test test, bool initialize = false)
    {
        if (!test.Run(saveOutput: initialize))
        {
            throw new Exception($"{test} FAILED!");
        }
        else
        {
            return test.ToString() + " passed, OK" + "\n";
        }
    }
}