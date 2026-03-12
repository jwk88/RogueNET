public class Handle : IInterractable
{
    bool open;
    bool locked;
    Key key;

    public bool IsOpen => open;
    public bool IsLocked => locked;

    public void Interract(Actor actor)
    {
        if (IsOpen)
        {
            open = false;
            return;
        }

        if (locked)
        {
            if (!actor.HasItem(key))
            {
                return;    
            }

            locked = false;
        }

        open = true;
    }

    public void LockWithKey(Key key)
    {
        this.key = key;
        locked = true;
    }
}