using System;
using System.Collections;
using System.Collections.Generic;

public class Grid : IEnumerable<Node>
{
    Node[,] cells;
    int width;
    int depth;
    Point origin;

    public int Width => width;
    public int Depth => depth;
    public Point Origin => origin;

    public Grid(int width, int depth)
    {
        this.width = width;
        this.depth = depth;

        cells = new Node[width, depth];
        origin = new Point(width / 2, depth / 2);
        for (int y = 0; y < depth; y++)
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
            if (x < 0 || x >= width) throw new InvalidOperationException("Out of bounds, check walls");
            if (y < 0 || y >= depth) throw new InvalidOperationException("Out of bounds, check walls");

            return cells[x, y];
        }
    }

    public Node this[Point point]
    {
        get
        {
            var x = point.X;
            var y = point.Y;

            if (x < 0 || x >= width) throw new InvalidOperationException("Out of bounds, check walls");
            if (y < 0 || y >= depth) throw new InvalidOperationException("Out of bounds, check walls");

            return cells[x, y];
        }
    }

    public Point RightOf(Point point, int offsetX = 1)
    {
        return new Point(point.X + offsetX, point.Y);
    }

    public Point LeftOf(Point point, int offsetX = -1)
    {
        return new Point(point.X + offsetX, point.Y);
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