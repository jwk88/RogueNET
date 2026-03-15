public abstract class Entity : EntityBase
{
    protected World world;
    protected Point point;
    protected int height = 1;
    protected int layer;

    public int Height => height;
    bool isPlaced;
    Node Node => world[layer][point];

    public void SetHeight(int height)
    {
        this.height = height;
    }

    public virtual void SetWorld(World world, int layer)
    {
        this.world = world;
        this.layer = layer;
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
        var next = world[layer][point];

        if (next.Occupied && !overwrite)
        {
            Log.Info($"{this} path was blocked by {next.Owner}");
            return false;
        }

        if (Node != null)
        {
            ReleaseOwnership();
        }

        if (isPlaced)
        {
            Log.Info($"'{Name}' is moving from {this.point} to {point}");    
        }

        this.point = point;
        Node.SetOwner(this);
        for (int i = 0; i < height; i++)
        {
            var above = world[layer + i][Node.X, Node.Y];
            above.SetOwner(this);
        }
        
        isPlaced = true;
        
        return true;
    }

    public void ReleaseOwnership()
    {
        if (Node == null) return;
        for (int i = 0; i < height; i++)
        {
            var above = world[layer + i][Node.Point];
            above.SetOwner(null);
        }
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