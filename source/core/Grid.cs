using System.Collections;
using System.Collections.Generic;

public class Grid : IEnumerable<Node>
{
    Node[,] cells;
    int width;
    int height;

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

    public Cell RightOf(Cell cell, int offsetX = 1)
    {
        return cells[cell.X + offsetX, cell.Y];
    }

    public Cell LeftOf(Cell cell, int offsetX = -1)
    {
        return cells[cell.X + offsetX, cell.Y];
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