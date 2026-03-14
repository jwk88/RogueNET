using System;

public class Builder<T> where T : Entity
{
    protected Grid grid;
    protected Point point;

    public Builder(Grid grid, Point point)
    {
        this.grid = grid;
        this.point = point;
    }

    public T Build()
    {
        var entity = (Entity)Activator.CreateInstance(typeof(T));
        if (grid[point.X, point.Y].Owner != null)
        {
            Log.Warn("Tried to build entity on top of an existing entity. Cancelled.");
            return null;
        }

        entity.SetGrid(grid);
        entity.SetPosition(point);

        return (T)entity;
    }

    public void Retarget(int x, int y) => Retarget(new Point(x, y));
    public void Retarget(Point point)
    {
        this.point = point;
    }
}