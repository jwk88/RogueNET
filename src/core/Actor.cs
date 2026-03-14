using System.Collections.Generic;

public class Actor : Entity
{
    List<Entity> inventory;
    Node Target(int xDir, int yDir) => grid[point + new Point(xDir, yDir)];

    public Actor()
    {
        inventory = new List<Entity>();
    }

    public virtual void Interract(int xDir, int yDir)
    {
        var target = Target(xDir, yDir);
        if (target.Owner != null)
        {
            if (target.Owner is IInterractable)
            {
                var openable = target.Owner as IInterractable;
                openable.InteractedBy(this);
            }
            else
            {
                Log.Info($"{this} tried to interract with {target.Owner}. Nothing happened.");
            }
        }
        else
        {
            Log.Info($"{this} tried to interract with something, but there's nothing there!");
        }
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

    public virtual void UseItem(int index, int xDir, int yDir)
    {
        if (inventory.Count == 0)
        {
            Log.Info($"{this} searched his inventory for item {index}, but inventory is empty!");
            return;
        }

        var item = inventory[index];
        var target = Target(xDir, yDir);
        if (target.Owner == null)
        {
            Log.Info($"{this} tried to use {item} at {target}, but there's nothing there!");
            return;
        }
        if (item is IUsable)
        {
            var usable = item as IUsable;
            usable.Use(this, target.Owner);
        }
    }

    public virtual void AddToInventory(Entity entity)
    {
        inventory.Add(entity);
        Log.Info($"{this} placed {entity} in their inventory");
    }
}
