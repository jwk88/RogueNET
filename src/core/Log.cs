namespace RogueNET
{
    using System;
    using System.Collections.Generic;

    public static class Log
    {
        public static Queue<string> logs = new Queue<string>();

        public static void Info(string msg)
        {
            logs.Enqueue($"[Info] {msg}");
        }

        public static void Warn(string msg)
        {
            logs.Enqueue($"[WARNING] {msg}");
        }

        public static void Debug(string msg)
        {
            logs.Enqueue($"[DEBUG] {msg}");
        }

        public static void Action(string msg)
        {
            logs.Enqueue($"[ACTION] {msg}");
        }
    }
}
