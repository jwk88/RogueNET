using System;

public class Builder
{
    Grid grid;
    Cell target;
    Type type;

    public Builder(Type type, Grid grid, Cell target)
    {
        this.grid = grid;
        this.target = target;
        this.type = type;
    }

    public void Build(Action<Entity, Type> onBuild)
    {
        var entity = (Entity)Activator.CreateInstance(type);
        var node = grid[target.X, target.Y];
        if (node.Owner != null)
        {
            Log.Warn("Tried to build entity on top of an existing entity. Cancelled.");
            return;
        }

        entity.SetGrid(grid);
        entity.SetNode(node);

        onBuild?.Invoke(entity, type);
    }
}
