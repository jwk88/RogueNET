public abstract class Entity : EntityBase
{
    protected Grid grid;
    protected Node node;

    public virtual void SetGrid(Grid grid)
    {
        this.grid = grid;
    }

    public virtual bool SetNode(Node next)
    {
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

        if (node != null)
        {
            node.Owner = null;
        }

        node = next;
        node.Owner = this;
        Log.Info($"{this} moved to {node}");
        return true;
    }

    public virtual void Move(int xDir, int yDir)
    {
        var next = grid[node.X + xDir, node.Y + yDir];
        SetNode(next);
    }

    public override string ToString()
    {
        if (node != null)
        {
            return $"{base.ToString()} {node}";    
        }
        else
        {
            return base.ToString() + " {-,-}";
        }
    }
}