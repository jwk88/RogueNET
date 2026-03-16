using System;
using System.Text;

public class ConsoleManager
{
    StringBuilder sb;

    public ConsoleManager()
    {
        sb = new StringBuilder();
    }

    public string Draw(Grid grid)
    {
        sb.AppendLine();
        sb.AppendLine("-----------------------------------------");

        while (Log.logs.Count > 0)
        {
            sb.AppendLine(Log.logs.Dequeue());
        }

        int depth = Config.depth;
        int width = Config.width;

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

            sb.AppendLine(new string(line));
        }

        string output = sb.ToString();

        Console.Write(output);
        return output;
    }

    public string GetOutput()
    {
        return sb.ToString();
    }
}
