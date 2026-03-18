using System;
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
    HashSet<RoomData> roomData = new HashSet<RoomData>();

    public List<RoomData> RoomsData => roomData.ToList();

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
            Depth = roomDepth
        });
    }

    public void DiscardRooms(int chance)
    {
        for (int i = 0; i < RoomsData.Count; i++)
        {
            var rng = RogueNET.RNG.Next(0, 100);
            if (rng < chance)
            {
                RoomsData.RemoveAt(i);
            }
        }
    }

    void CreatePath(List<Point> path, bool vertical)
    {
        var i = 0;
        foreach (var entry in path)
        {
            if (i == path.Count - 1)
            {
                break;
            }

            var wall1 = new EntityBuilder<Wall>(grid, entry + (vertical ? Point.Right : Point.Up)).Build(true);
            wall1.SetSymbol(vertical ? Definitions.VerticalWall : Definitions.HorizontWall);

            var wall2 = new EntityBuilder<Wall>(grid, entry + (vertical ? Point.Left : Point.Down)).Build(true);
            wall2.SetSymbol(vertical ? Definitions.VerticalWall : Definitions.HorizontWall);
            i++;
        }
    }

    public void ConnectRooms()
    {
        var raycaster = new Raycast(grid);

        foreach (var room in RoomsData)
        {
            var tNode = grid[room.Origin + (Point.Up    * ((room.Depth / 2) - 1))];
            var bNode = grid[room.Origin + (Point.Down  * ((room.Depth / 2) - 1))];
            var lNode = grid[room.Origin + (Point.Left  * ((room.Width / 2) - 1))];
            var rNode = grid[room.Origin + (Point.Right * ((room.Width / 2) - 1))];

            var path = new List<Point>();
            var up = raycaster.CastUp(tNode.Point, ref path);
            if (up != null && up is Wall)
            {
                CreatePath(path, vertical: true);   
            }
            path.Clear();
            
            var left = raycaster.CastLeft(lNode.Point, ref path);
            if (left != null && left is Wall)
            {
                CreatePath(path, vertical: false);
            }

            path.Clear();
            // raycaster.CastDown(bNode.Point, ref path);
            // raycaster.CastRight(rNode.Point, ref path);
        }
    }

    // public void SaveForLater()
    // {
    //     // var cornerLeft = start + Point.Left;
    //     // var cornerRight = start + Point.Right;

    //     // var leftBuild = new EntityBuilder<Corner>(grid, cornerLeft).Build(overwrite: true);
    //     // var rightBuild = new EntityBuilder<Corner>(grid, cornerRight).Build(overwrite: true);

    //     // leftBuild.SetSymbol(Definitions.BotRightWall);
    //     // rightBuild.SetSymbol(Definitions.BotLeftCWall);

    //     // new EntityBuilder<Door>(grid, start).Build(overwrite: true);
    //     var i = 0;
    //     foreach (var entry in path)
    //     {
    //         if (i == path.Count - 1)
    //         {
    //             // cornerLeft = entry + Point.Left;
    //             // cornerRight = entry + Point.Right;

    //             // leftBuild = new EntityBuilder<Corner>(grid, cornerLeft).Build(overwrite: true);
    //             // rightBuild = new EntityBuilder<Corner>(grid, cornerRight).Build(overwrite: true);

    //             // leftBuild.SetSymbol(Definitions.TopRightWall);
    //             // rightBuild.SetSymbol(Definitions.TopLeftCWall);

    //             // new EntityBuilder<Door>(grid, entry).Build(overwrite: true);
    //             break;
    //         }
    //     }
    // }
}