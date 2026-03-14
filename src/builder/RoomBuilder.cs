using System;

public class RoomBuilder
{
    World world;
    Random rng;
    int width;
    int depth;

    public RoomBuilder(World world, Point origin, Random rng)
    {
        this.world = world;
        this.rng = rng;

        width = rng.Next(Config.roomMinWidth, Config.roomMaxWidth);
        depth = rng.Next(Config.roomMinDepth, Config.roomMaxDepth);

        FormatRoom(origin, width, depth);
    }

    void FormatRoom(Point center, int width, int height)
    {
        var xMin = center.X - (width / 2) + 1;
        var yMin = center.Y - (height / 2) + 1;
        var xMax = center.X + (width / 2) - 1;
        var yMax = center.Y + (height / 2) - 1;

        for (int i = 0; i < Config.worldHeight; i++)
        {
            for (int y = 0; y < Config.depth; y++)
            {
                for (int x = 0; x < Config.width; x++)
                {
                    var grid = world[i];
                    var node = grid[x, y];
                    var p = node.Point;

                    if (p.X < xMin || p.Y < yMin || p.X > xMax || p.Y > yMax)
                    {
                        continue;
                    }
                    
                    if ((p.X == xMin || p.X == xMax) && (p.Y == yMin || p.Y == yMax))
                    {
                        new EntityBuilder<Corner>(grid, p).Build();
                    }
                    else if (p.X == xMin || p.X == xMax)
                    {
                        var vertical = new EntityBuilder<Wall>(grid, p).Build();
                        vertical.SetSymbol('|');
                    }
                    else if (p.Y == yMin || p.Y == yMax)
                    {
                        var horizontal = new EntityBuilder<Wall>(grid, p).Build();
                        horizontal.SetSymbol('-');
                    }
                    else
                    {
                        if (i == 0)
                        {
                            new EntityBuilder<Ground>(grid, p).Build();
                        }
                        if (i == Config.worldHeight - 1)
                        {
                            new EntityBuilder<Roof>(grid, p).Build();
                        }
                    }
                }
            }
        }
    }
}