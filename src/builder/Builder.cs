using System;

public class Builder
{
    protected Grid grid;
    protected Point point;
    protected Type type;

    public Builder() { }
    public Builder(Type type, Grid grid, Point point)
    {
        this.grid = grid;
        this.point = point;
        this.type = type;
    }

    public void Build(Action<Entity, Type> onBuild)
    {
        var entity = (Entity)Activator.CreateInstance(type);
        if (grid[point.X, point.Y].Owner != null)
        {
            Log.Warn("Tried to build entity on top of an existing entity. Cancelled.");
            return;
        }

        entity.SetGrid(grid);
        entity.SetPosition(point);

        onBuild?.Invoke(entity, type);
    }

    public void Retarget(Point target)
    {
        this.point = target;
    }
}