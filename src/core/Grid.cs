using System.Collections;
using System.Collections.Generic;

public class Grid : IEnumerable<Node>
{
    Node[,] cells;
    int width;
    int height;

    public int Width => width;
    public int Height => height;

    public Grid(int width, int height)
    {
        this.width = width;
        this.height = height;

        cells = new Node[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                cells[x, y] = new Node(x, y);
            }
        }
    }

    public Node this[int x, int y]
    {
        get
        {
            if (x < 0 || x >= width)  return null;
            if (y < 0 || y >= height) return null;

            return cells[x, y];
        }
    }

    public Node this[Point point]
    {
        get
        {
            var x = point.X;
            var y = point.Y;

            if (x < 0 || x >= width)  return null;
            if (y < 0 || y >= height) return null;

            return cells[x, y];
        }
    }

    public Point RightOf(Point point, int offsetX = 1)
    {
        return cells[point.X + offsetX, point.Y].Point;
    }

    public Point LeftOf(Point point, int offsetX = -1)
    {
        return cells[point.X + offsetX, point.Y].Point;
    }

    public IEnumerator<Node> GetEnumerator()
    {
        foreach (var cell in cells)
        {
            yield return cell;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}