public abstract class Entity : EntityBase
{
    protected Grid grid;
    protected Point point;
    protected Stats stats;

    bool isPlaced;
    Node Node => grid[point];

    public void SetStats(Stats stats)
    {
        this.stats = stats;
    }

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

    public virtual bool SetPosition(int x, int y, bool overwrite = false) => SetPosition(new Point(x, y), overwrite);
    public virtual bool SetPosition(Point point, bool overwrite = false)
    {
        var next = grid[point];

        if (next.Occupied && !overwrite)
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

    public Entity GetBelow()
    {
        return grid[Node.Point].Owner;
    }

    public Entity GetAbove()
    {
        return grid[Node.Point].Owner;
    }
}