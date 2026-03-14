using System.Collections;
using System.Collections.Generic;

public class Layers : IEnumerable<Grid>
{
    Grid[] grids;
    int height;

    public Grid[] Grids => grids;
    public int Height => height;

    public Layers(int width, int depth, int height)
    {
        this.height = height;
        grids = new Grid[height];

        for (int i = 0; i < height; i++)
        {
            grids[i] = new Grid(width, depth);
        }
    }

    public Grid this[int index]
    {
        get
        {
            return grids[index];
        }
    }

    public IEnumerator<Grid> GetEnumerator()
    {
        foreach (var grid in grids)
        {
            yield return null;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
