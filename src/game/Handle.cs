public class Handle : Entity, IOpenable
{
    bool open;
    bool locked;
    Entity owner;

    public bool IsOpen => open;
    public bool IsLocked => locked;

    public Entity Owner => owner;

    public Handle()
    {
        SetSymbol('h');

        stats = new Stats();
        stats.SetWeight(1);
    }

    public void Inject(IInterractable interractable)
    {
        throw new System.NotImplementedException();
    }

    public void InteractedBy(Actor actor)
    {
        Log.Info($"{actor} interracts with {Owner}");

        if (IsOpen)
        {
            Log.Info($"{Owner} is now closed");
            open = false;
            return;
        }

        if (locked)
        {
            Log.Info($"{Owner} is locked");
            return;
        }

        Log.Info($"{Owner} is now open");
        open = true;
    }

    public void Lock()
    {
        locked = true;
    }

    public void Unlock()
    {
        locked = false;
    }

    public void LockWithKey(IOwnable key)
    {
        locked = true;
        key.SetOwner(this);
    }

    public void SetOwner(Entity entity)
    {
        this.owner = entity;
    }
}