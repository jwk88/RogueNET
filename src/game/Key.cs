public class Key : Entity, IPickupable, IUsable
{
    Entity locked;
    public Entity Owner => locked;

    public Key()
    {
        SetSymbol('k');

        stats = new Stats();
        stats.SetWeight(0.2);
    }

    public void PickedUpBy(Actor actor)
    {
        actor.AddToInventory(this);
    }

    public void SetOwner(Entity entity)
    {
        locked = entity;
    }

    public void Use(Actor user, Entity target)
    {
        Log.Info($"{user} uses {this} on {target}");
        if (target is IOpenable)
        {
            var openable = target as IOpenable;
            if (openable == locked)
            {
                openable.Unlock();
            }
        }
        else if (locked != null)
        {
            Log.Info($"{target} was owned by {locked}");
            if (locked is IOpenable)
            {
                var openable = locked as IOpenable;
                openable.Unlock();
                Log.Info($"{user} used {this} to open {locked} on {target}");
            }
        }
        else
        {
            Log.Info($"Nothing happened.");
        }
    }
}