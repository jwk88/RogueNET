namespace RogueNET
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    public class Grid : IEnumerable<Node>
    {
        Node[,] nodes;
        char[,] ascii;
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

            nodes = new Node[width, depth];
            ascii = new char[width, depth];
            origin = new Point(width / 2, depth / 2);
            for (int y = 0; y < depth; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    nodes[x, y] = new Node(x, y);
                    ascii[x, y] = ' ';
                }
            }
        }

        public string GetCharsAround(Entity entity, int xSize, int ySize)
        {
            var sb = new StringBuilder();

            int centerX = entity.Point.X;
            int centerY = entity.Point.Y;

            int halfX = xSize / 2;
            int halfY = ySize / 2;

            int startX = centerX - halfX;
            int startY = centerY - halfY;

            for (int y = 0; y < ySize; y++)
            {
                int gy = startY + y;

                for (int x = 0; x < xSize; x++)
                {
                    int gx = startX + x;

                    char c = ' ';

                    if (gx >= 0 && gx < Width && gy >= 0 && gy < Depth)
                    {
                        var node = nodes[gx, gy];

                        if (node.Owner != null)
                            c = node.Owner.Symbol;
                    }

                    sb.Append(c);
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        public bool IsInside(Point p)
        {
            return p.X >= 0 && p.X < Width &&
                   p.Y >= 0 && p.Y < Depth;
        }

        public Node this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= width) throw new InvalidOperationException("Out of bounds, check walls");
                if (y < 0 || y >= depth) throw new InvalidOperationException("Out of bounds, check walls");

                return nodes[x, y];
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

                return nodes[x, y];
            }
        }

        public bool HasNeighbour<T>(Point center)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i == 4) continue;

                var xi = (i % 3) - 1;
                var yi = (i / 3) - 1;

                var x = xi + center.X;
                var y = yi + center.Y;

                var nPoint = new Point(x, y);
                if (!IsInside(nPoint)) continue;
            
                var node = nodes[nPoint.X, nPoint.Y];
                if (node.Owner is T)
                {
                    return true;
                }
            }
            return false;
        }

        public bool GetFirstEmptyNeighbour(Point center, out Node node)
        {
            for (int i = 0; i < 9; i++)
            {
                if (i == 4) continue;

                var xi = (i % 3) - 1;
                var yi = (i / 3) - 1;

                var x = xi + center.X;
                var y = yi + center.Y;

                var nPoint = new Point(x, y);
                if (!IsInside(nPoint)) continue;
            
                node = nodes[nPoint.X, nPoint.Y];
                if (node.Owner == null)
                {
                    return true;
                }
            }
            node = null;
            return false;
        }

        public IEnumerator<Node> GetEnumerator()
        {
            foreach (var cell in nodes)
            {
                yield return cell;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}