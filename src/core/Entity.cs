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

    public void StepRight(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (!SetPosition(point.X + 1, point.Y))
            {
                break;
            }
        }
    }

    public void StepLeft(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (!SetPosition(point.X - 1, point.Y))
            {
                break;
            }
        }
    }

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
        if (!isPlaced)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                Log.Info($"'{Name}' has been spawned at {this.point}");    
            }
        }
        
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