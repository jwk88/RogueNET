public class Handle : IOpenable
{
    bool open;
    bool locked;
    Key key;
    Entity owner;

    public bool IsOpen => open;
    public bool IsLocked => locked;

    public Entity Owner => owner;

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
            if (!actor.HasItem(key))
            {
                Log.Info($"{Owner} is locked, {actor} did not have the key.");
                return;    
            }

            locked = false;
        }

        Log.Info($"{Owner} is now open");
        open = true;
    }

    public void LockWithKey(Key key)
    {
        this.key = key;
        locked = true;
    }

    public void SetOwner(Entity entity)
    {
        this.owner = entity;
    }
}