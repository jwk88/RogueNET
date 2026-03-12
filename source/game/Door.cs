public class Door : Entity, IInterractable
{
    protected Handle handle;

    public virtual void SetHandle(Handle handle)
    {
        this.handle = handle;
    }

    public void Interract(Actor actor)
    {
        Log.Info($"{actor} interracts with {this}");
        handle.Interract(actor);
        
        if (handle.IsOpen)
        {
            Log.Info($"{this} is now open");    
        }
        else
        {
            if (handle.IsLocked)
            {
                Log.Info($"{this} is locked, {actor} did not have the key.");
            }
            else
            {
                Log.Info($"{this} is now closed");    
            }
        }
    }
}