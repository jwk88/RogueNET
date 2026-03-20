namespace RogueNET
{
    using System.Collections.Generic;
    using System.Linq;

    public class DungeonBuilder
    {
        class BSPNode
        {
            public int X;
            public int Y;
            public int Width;
            public int Depth;
            public BSPNode Left;
            public BSPNode Right;

            public BSPNode(int x, int y, int width, int depth)
            {
                X = x;
                Y = y;
                Width = width;
                Depth = depth;
                Left = null;
                Right = null;
            }

            public bool IsLeaf => Left == null && Right == null;
        }

        Grid grid;
        int minWidth;
        int minDepth;
        int margin;
        List<RoomData> roomData = new List<RoomData>();

        public List<RoomData> GetActiveRooms => roomData.Where(x => !x.Offline).ToList();

        public DungeonBuilder(Grid grid, int minWidth = 10, int minDepth = 10, int margin = 2)
        {
            this.grid = grid;
            this.minWidth = minWidth;
            this.minDepth = minDepth;
            this.margin = margin;

            var root = new BSPNode(0, 0, grid.Width, grid.Depth);

            Split(root);
            BuildRooms(root);
        }

        void Split(BSPNode node)
        {
            if (node.Width < (minWidth + margin) * 2 && node.Depth < (minDepth + margin) * 2)
                return;

            bool splitVertical = node.Width > node.Depth;

            if (node.Width >= node.Depth && node.Width >= (minWidth + margin) * 2)
                splitVertical = true;
            else if (node.Depth >= (minDepth + margin) * 2)
                splitVertical = false;
            else
                return;

            if (splitVertical)
            {
                int split = RogueNET.RNG.Next(
                    minWidth + margin,
                    node.Width - (minWidth + margin));

                node.Left = new BSPNode(node.X, node.Y, split, node.Depth);
                node.Right = new BSPNode(node.X + split, node.Y, node.Width - split, node.Depth);
            }
            else
            {
                int split = RogueNET.RNG.Next(
                    minDepth + margin,
                    node.Depth - (minDepth + margin));

                node.Left = new BSPNode(node.X, node.Y, node.Width, split);
                node.Right = new BSPNode(node.X, node.Y + split, node.Width, node.Depth - split);
            }

            Split(node.Left);
            Split(node.Right);
        }

        void BuildRooms(BSPNode node)
        {
            if (!node.IsLeaf)
            {
                if (node.Left != null) BuildRooms(node.Left);
                if (node.Right != null) BuildRooms(node.Right);
                return;
            }

            int maxWidth = node.Width - margin * 2;
            int maxDepth = node.Depth - margin * 2;

            if (maxWidth <= minWidth || maxDepth <= minDepth)
                return;

            int roomWidth = RogueNET.RNG.Next(minWidth, maxWidth);
            int roomDepth = RogueNET.RNG.Next(minDepth, maxDepth);

            int roomX = node.X + margin + RogueNET.RNG.Next(0, maxWidth - roomWidth);
            int roomY = node.Y + margin + RogueNET.RNG.Next(0, maxDepth - roomDepth);

            int centerX = roomX + roomWidth / 2;
            int centerY = roomY + roomDepth / 2;

            roomData.Add(new RoomData()
            {
                Origin = new Point(centerX, centerY),
                Width = roomWidth,
                Depth = roomDepth,
                Offline = false,
            });
        }

        public void DiscardRooms(int chance)
        {
            for (int i = 0; i < roomData.Count; i++)
            {
                var rng = RogueNET.RNG.Next(0, 100);
                if (rng < chance)
                {
                    var data = roomData[i];
                    data.Offline = true;
                    roomData[i] = data;
                }
            }
        }

        void CreatePathway(List<Point> path, bool vertical)
        {
            foreach (var entry in path)
            {
                var wall1 = new EntityBuilder<Wall>(grid, entry + (vertical ? Point.Right : Point.Up)).Build(true);
                wall1.SetSymbol(vertical ? Definitions.VerticalWall : Definitions.HorizontWall);

                var wall2 = new EntityBuilder<Wall>(grid, entry + (vertical ? Point.Left : Point.Down)).Build(true);
                wall2.SetSymbol(vertical ? Definitions.VerticalWall : Definitions.HorizontWall);
            }

            var door1 = new EntityBuilder<Door>(grid, path[0]).Build(true);
            var door2 = new EntityBuilder<Door>(grid, path[path.Count - 1]).Build(true);

            var handle1 = new Handle();
            handle1.SetName("Door Handle");
            door1.Inject(handle1);

            var handle2 = new Handle();
            handle2.SetName("Door Handle");
            door2.Inject(handle2);

            var offset1 = vertical ? Point.Left : Point.Up;
            var offset2 = vertical ? Point.Right : Point.Down;

            new EntityBuilder<Corner>(grid, path[0] + offset1).Build(true).SetSymbol(Definitions.BotRightWall);
            new EntityBuilder<Corner>(grid, path[0] + offset2).Build(true).SetSymbol(vertical ? Definitions.BotLeftCWall : Definitions.TopRightWall);

            offset1 = vertical ? Point.Left : Point.Up;
            offset2 = vertical ? Point.Right : Point.Down;

            new EntityBuilder<Corner>(grid, path[path.Count - 1] + offset1).Build(true).SetSymbol(vertical ? Definitions.TopRightWall : Definitions.BotLeftCWall);
            new EntityBuilder<Corner>(grid, path[path.Count - 1] + offset2).Build(true).SetSymbol(Definitions.TopLeftCWall);
        }

        public void ConnectRooms()
        {
            var raycaster = new Raycast(grid);

            foreach (var room in GetActiveRooms)
            {
                var tNode = grid[room.Origin + (Point.Up    * ((room.Depth / 2) - 1))];
                var bNode = grid[room.Origin + (Point.Down  * ((room.Depth / 2) - 1))];
                var lNode = grid[room.Origin + (Point.Left  * ((room.Width / 2) - 1))];
                var rNode = grid[room.Origin + (Point.Right * ((room.Width / 2) - 1))];

                var path = new List<Point>();
                var up = raycaster.CastUp(tNode.Point, ref path);
                if (up != null && up is Wall)
                {
                    var last = path[path.Count - 1];
                    if (!grid.HasNeighbour<Corner>(last))
                    {
                        path.Insert(0, tNode.Point);
                        CreatePathway(path, vertical: true);
                    }
                }

                path.Clear();
            
                var left = raycaster.CastLeft(lNode.Point, ref path);
                if (left != null && left is Wall)
                {
                    var last = path[path.Count - 1];
                    if (!grid.HasNeighbour<Corner>(last))
                    {
                        path.Insert(0, lNode.Point);
                        CreatePathway(path, vertical: false);
                    }
                }
                path.Clear();
            }
        }
    }
}