public abstract class Entity : EntityBase
{
    protected Grid grid;
    protected Point point;

    public Node Node => grid[point];
    bool isPlaced;

    public virtual void SetGrid(Grid grid)
    {
        this.grid = grid;
    }

    public virtual void Move(int xDir, int yDir) => SetPosition(point.X + xDir, point.Y + yDir);
    public virtual bool SetPosition(int x, int y) => SetPosition(new Point(x, y));
    public virtual bool SetPosition(Point point)
    {
        var next = grid[point];

        if (next == null)
        {
            Log.Info($"{this} path was blocked, edge of the world");
            return false;
        }

        if (next.Occupied)
        {
            Log.Info($"{this} path was blocked by {next.Owner}");
            return false;
        }

        if (Node != null)
        {
            Node.SetOwner(null);
        }

        if (isPlaced)
        {
            Log.Info($"'{Name}' is moving from {this.point} to {point}");    
        }

        this.point = point;
        Node.SetOwner(this);
        
        isPlaced = true;
        
        return true;
    }

    public override string ToString()
    {
        if (!isPlaced)
        {
            return base.ToString() + " {-,-}";
        }
        else
        {
            return $"{base.ToString()} {point}";
        }
    }
}