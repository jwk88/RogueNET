using System.Collections.Generic;

public class Actor : Entity
{
    public string Name { get => name; private set => name = value; }
    string name;
    List<Entity> inventory;

    public Actor()
    {
        inventory = new List<Entity>();
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    Node Target(int xDir, int yDir)
    {
        return grid[node.X + xDir, node.Y + yDir];
    }

    public virtual Entity Interract(int xDir, int yDir)
    {
        var target = Target(xDir, yDir);
        if (target.Owner != null)
        {
            if (target.Owner is IInterractable)
            {
                var openable = target.Owner as IInterractable;
                openable.InteractedBy(this);
            }
            return target.Owner;
        }
        return null;
    }

    public virtual void Loot(int xDir, int yDir)
    {
        var node = Target(xDir, yDir);
        if (node.Owner == null)
        {
            Log.Info($"{this} tried to loot the contents of air!");
            return;
        }
        var entity = node.Owner;
        if (entity != null && entity is ILootable)
        {
            var container = entity as ILootable;
            container.LootFor(this);
        }
    }

    public virtual void AddToInventory(Entity entity)
    {
        inventory.Add(entity);
        Log.Info($"{this} placed {entity} in their inventory");
    }

    public virtual bool HasItem(Entity entity)
    {
        return inventory.Contains(entity);
    }

    public override string ToString()
    {
        return $"{base.ToString()} ({Name})";
    }
}
