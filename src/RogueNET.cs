using System;
using System.IO;

public class RogueNET
{
    public static Random RNG;
    public static RuntimeConfig config;
    public static Game game;

    const string configPath = "config.txt";

    public RogueNET(params string[] args)
    {
        Console.Clear();

        if (!File.Exists(configPath))
        {
            var dummy = new RuntimeConfig();
            File.WriteAllText(configPath, dummy.Serialize());
        }

        var configText = File.ReadAllText(configPath);
        config = Serializable<RuntimeConfig>.Deserialize(configText);
        if (args.Length == 0)
        {
            RNG = new Random(config.Seed);
            game = new Game(config);
            return;
        }

        if (!ParseRuntimeCommands(args))
        {
            RNG = new Random(config.Seed);
            game = new Game(config);
            game.RunFromCLI();
        }
    }

    bool ParseRuntimeCommands(params string[] args)
    {
        var command = new Command(args);
        var header = command.Args[0].String;

        if (header == "reset")  return ProcessDefaults(command);
        if (header == "config") return ProcessRuntimeConfig(command);
        if (header == "test")   return ProcessTests(command); 

        return false;
    }

    bool ProcessDefaults(Command command)
    {
        if (command.Args[1].String == "defaults")
        {
            var config = new RuntimeConfig();
            var serialized = config.Serialize();
            File.WriteAllText(configPath, serialized);

            return true;
        }

        return false;
    }

    bool ProcessRuntimeConfig(Command command)
    {
        if (command.Args[1].String == "--set")
        {
            var field = command.Args[2].String;
            var value = command.Args[3];

            var success = config.TrySetField(field, value);
            if (success)
            {
                File.WriteAllText("config.txt", config.Serialize());
                Console.WriteLine($"Successfully set '{field}'");
            }
            else
            {
                Console.WriteLine($"Failed to find setting '{field}'");
            }
            return true;
        }
        return false;
    }

    bool ProcessTests(Command command)
    {
        var testName = command.Args[1].String;
        var initialize = false;
        if (command.Args.Length > 2)
        {
            if (command.Args[2].String == "init")
            {
                initialize = true;
            }
        }

        var tests = new TestManager();
        var test = tests.Tests[command.Args[1].String];
        var testOutput = tests.RunTest(test, initialize);

        Console.Clear();
        Console.WriteLine(testOutput);

        return true;
    }
}