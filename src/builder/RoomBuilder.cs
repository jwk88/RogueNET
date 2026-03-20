namespace RogueNET
{
    public class RoomBuilder
    {
        Grid grid;
        int width;
        int depth;

        public RoomBuilder(Grid grid, RoomData data)
        {
            this.grid = grid;
        
            width = data.Width;
            depth = data.Depth;

            FormatRoom(data.Origin);
        }

        public RoomBuilder(Grid grid, Point origin, int width, int depth)
        {
            this.grid = grid;
            this.width = width;
            this.depth = depth;

            FormatRoom(origin);
        }

        void FormatRoom(Point center)
        {
            var xMin = center.X - (width / 2) + 1;
            var yMin = center.Y - (depth / 2) + 1;
            var xMax = center.X + (width / 2) - 1;
            var yMax = center.Y + (depth / 2) - 1;

            for (int y = 0; y < grid.Depth; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    var node = grid[x, y];
                    var p = node.Point;

                    if (p.X < xMin || p.Y < yMin || p.X > xMax || p.Y > yMax)
                    {
                        continue;
                    }

                    if ((p.X == xMin || p.X == xMax) && (p.Y == yMin || p.Y == yMax))
                    {
                        var corner = new EntityBuilder<Corner>(grid, p).Build();
                        if (p.X == xMin && p.Y == yMin) corner.SetSymbol(Definitions.TopLeftCWall);
                        if (p.X == xMin && p.Y == yMax) corner.SetSymbol(Definitions.BotLeftCWall);
                        if (p.X == xMax && p.Y == yMin) corner.SetSymbol(Definitions.TopRightWall);
                        if (p.X == xMax && p.Y == yMax) corner.SetSymbol(Definitions.BotRightWall);
                    }
                    else if (p.X == xMin || p.X == xMax)
                    {
                        var vertical = new EntityBuilder<Wall>(grid, p).Build();
                        vertical.SetSymbol(Definitions.VerticalWall);
                    }
                    else if (p.Y == yMin || p.Y == yMax)
                    {
                        var horizontal = new EntityBuilder<Wall>(grid, p).Build();
                        horizontal.SetSymbol(Definitions.HorizontWall);
                    }
                }
            }
        }
    }
}