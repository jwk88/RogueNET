using System.Collections.Generic;

public class Actor : Entity
{
    List<Entity> inventory;
    Node Target(int xDir, int yDir) => grid[point + new Point(xDir, yDir)];

    public Actor()
    {
        inventory = new List<Entity>();
    }

    public virtual void AddToInventory(Entity entity)
    {
        inventory.Add(entity);
        Log.Info($"{this} placed {entity} in their inventory");
    }

    public virtual void Interract(int xDir, int yDir)
    {
        Log.Action($"INTERRACT");
        if (xDir == 0 && yDir == 0) return;
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
        Log.Action($"LOOT");
        if (xDir == 0 && yDir == 0) return;
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
        Log.Action($"USE ITEM");
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

    public virtual void Pickup(int xDir, int yDir)
    {
        Log.Action("PICK UP");
        if (xDir == 0 && yDir == 0) return;
        var target = Target(xDir, yDir);
        if (target.Owner == null)
        {
            Log.Info($"{this} tried to pick up air!");
            return;
        }

        // TODO: strength vs weight check here later, so can't pickup walls and stuff
        SetCarry(target.Owner);
        target.SetOwner(null);
        Log.Info($"{this} picked up {Carry}");
    }

    public virtual void Putdown(int xDir, int yDir)
    {
        Log.Action("PUT DOWN");
        if (xDir == 0 && yDir == 0) return;
        if (Carry == null)
        {
            Log.Info($"{this} is not carrying anything to put down!");
            return;
        }

        var node = Target(xDir, yDir);
        var nodeOwner = node.Owner;
        Log.Info($"{this} puts down the {Carry} he was carrying");
        
        if (nodeOwner != null)
        {
            var prevWeight = nodeOwner.Stats.WeightKG;
            var carryWeight = Carry.Stats.WeightKG;
            if (prevWeight >= carryWeight)
            {
                nodeOwner.SetCarry(Carry);
            }
            else
            {
                if (nodeOwner is Actor)
                {
                    Log.Info($"{nodeOwner} died");
                }
                node.SetOwner(null);
                node.SetOwner(Carry);
            }
        }

        SetCarry(null);
    }
}