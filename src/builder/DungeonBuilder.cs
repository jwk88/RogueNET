using System;

public class DungeonBuilder : Builder
{
    public DungeonBuilder() : base() { }
    public DungeonBuilder(Type type, Grid grid, Point point) : base(type, grid, point)
    {
        Fill(new Builder(typeof(Wall), grid, new Point(0,0)));
    }

    void Fill(Builder builder)
    {
        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                
            }
        }
    }
}