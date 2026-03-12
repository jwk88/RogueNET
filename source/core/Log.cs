using System;

public static class Log
{
    public static void Info(string msg)
    {
        Console.WriteLine($"[Info] {msg}");
    }

    public static void Warn(string msg)
    {
        Console.WriteLine($"[Warning] {msg}");
    }

    public static void Debug(string msg)
    {
        Console.WriteLine($"[DEBUG] {msg}");
    }
}
