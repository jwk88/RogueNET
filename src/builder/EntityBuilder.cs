using System;

public class EntityBuilder<T> where T : Entity
{
    protected World world;
    protected Point point;
    protected int layer;
    protected int height = 1;

    public EntityBuilder(World world, int layer, Point point)
    {
        this.world = world;
        this.point = point;
        this.layer = layer;
    }

    public void SetCustomHeight(int height)
    {
        this.height = height;
    }

    public virtual T Build(bool overwrite = false)
    {
        var entity = (Entity)Activator.CreateInstance(typeof(T));
        var node = world[layer][point.X, point.Y];
        if (node.Owner != null && !overwrite)
        {
            var msg = "Tried to build entity on top of an existing entity";
            msg += '\n' + "Current owner: " + node.Owner;
            msg += '\n' + "Builder: " + typeof(T);
            throw new InvalidOperationException(msg);
        }

        entity.SetWorld(world, layer);
        entity.SetHeight(height);
        entity.SetPosition(point, overwrite);

        return (T)entity;
    }
}