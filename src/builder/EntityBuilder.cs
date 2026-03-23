using System;

public class EntityBuilder<T> where T : Entity
{
    protected Grid grid;
    protected Point point;

    public EntityBuilder(Grid grid, Point point)
    {
        this.grid = grid;
        this.point = point;
    }

    public virtual T Build(bool overwrite = false)
    {
        var entity = (Entity)Activator.CreateInstance(typeof(T));
        var node = grid[point.X, point.Y];
        if (node.Owner != null && !overwrite)
        {
            var msg = "Tried to build entity on top of an existing entity";
            msg += '\n' + "Current owner: " + node.Owner;
            msg += '\n' + "Builder: " + typeof(T);
            throw new InvalidOperationException(msg);
        }

        entity.SetGrid(grid);
        entity.SetPosition(point, overwrite);

        return (T)entity;
    }
}