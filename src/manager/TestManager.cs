using System;
using System.Collections.Generic;
using System.IO;

public class TestManager
{
    List<Test> tests;

    public TestManager()
    {
        tests = new List<Test>();
        tests.Add(new Test1());
        tests.Add(new Test1b());
    }

    public void RunTests(bool initialize)
    {
        var testOutput = "";
        foreach (var test in tests)
        {
            if (!test.Run(saveOutput: initialize))
            {
                throw new Exception($"{test} FAILED!");
            }
            else
            {
                testOutput += test.ToString() + " passed, OK" + "\n";
            }
        }

        Console.Clear();
        Console.WriteLine(testOutput);
        Console.WriteLine("Tests passed, press any key to continue");
        Console.ReadKey();
    }
}