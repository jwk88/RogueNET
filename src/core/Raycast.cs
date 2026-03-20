namespace RogueNET
{
    using System.Collections.Generic;

    public class Raycast
    {
        Grid grid;

        public Raycast(Grid grid)
        {
            this.grid = grid;
        }

        public Entity CastUp(Point point, ref List<Point> path)
        {
            for (int y = point.Y - 1; y >= 0; y--)
            {
                if (y < 0) break;
                var probe = grid[point.X, y];
                path.Add(probe.Point);
                if (probe.Owner != null)
                {
                    return probe.Owner;
                }
            }

            return null;
        }

        public Entity CastDown(Point point, ref List<Point> path)
        {
            for (int y = point.Y + 1; y < grid.Depth; y++)
            {
                if (y >= grid.Depth) break;
                var probe = grid[point.X, y];
                path.Add(probe.Point);
                if (probe.Owner != null)
                {
                    return probe.Owner;
                }
            }

            return null;
        }

        public Entity CastRight(Point point, ref List<Point> path)
        {
            for (int x = point.X + 1; x < grid.Width; x++)
            {
                if (x >= grid.Width) break;
                var probe = grid[x, point.Y];
                path.Add(probe.Point);
                if (probe.Owner != null)
                {
                    return probe.Owner;
                }
            }

            return null;
        }

        public Entity CastLeft(Point point, ref List<Point> path)
        {
            for (int x = point.X - 1; x >= 0; x--)
            {
                if (x < 0) break;
                var probe = grid[x, point.Y];
                path.Add(probe.Point);
                if (probe.Owner != null)
                {
                    return probe.Owner;
                }
            }

            return null;
        }
    }
}