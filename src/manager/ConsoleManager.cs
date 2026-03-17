using System;
using System.Text;

public class ConsoleManager
{
    StringBuilder full;

    public string Title { get; set; }

    public ConsoleManager()
    {
        full = new StringBuilder();
    }

    public string Draw(Grid grid)
    {
        Console.Clear();
        var frame = new StringBuilder();
        frame.AppendLine($"-------{Title}-------");

        int depth = grid.Depth;
        int width = grid.Width;

        for (int y = 0; y < depth; y++)
        {
            char[] line = new char[width];

            for (int x = 0; x < width; x++)
            {
                char symbol = ' ';
                var owner = grid[x, y].Owner;
                if (owner != null)
                {
                    symbol = owner.Symbol;
                }

                line[x] = symbol;
            }

            frame.AppendLine(new string(line));
        }

        frame.AppendLine();

        while (Log.logs.Count > 0)
        {
            frame.AppendLine(Log.logs.Dequeue());
        }

        full.Append(frame.ToString());
        Console.Write(frame.ToString());
        Console.ReadKey();
        return full.ToString();
    }

    public string GetASCIIOnly(Grid grid)
    {
        Console.Clear();
        var frame = new StringBuilder();

        int depth = grid.Depth;
        int width = grid.Width;

        for (int y = 0; y < depth; y++)
        {
            char[] line = new char[width];

            for (int x = 0; x < width; x++)
            {
                char symbol = ' ';
                var owner = grid[x, y].Owner;
                if (owner != null)
                {
                    symbol = owner.Symbol;
                }

                line[x] = symbol;
            }

            frame.AppendLine(new string(line));
        }

        return frame.ToString();
    }

    public string GetFullOutput()
    {
        return full.ToString();
    }
}
