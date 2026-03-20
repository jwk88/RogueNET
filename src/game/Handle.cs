namespace RogueNET
{
    using System;

    public class Handle : Entity, IOpenable
    {
        bool open;
        bool locked;
        Entity owner;
        Action onOpen;

        public bool IsOpen => open;
        public bool IsLocked => locked;

        public Entity Owner => owner;

        public Action OnOpen { get => onOpen; set => onOpen = value; }

        public Handle()
        {
            SetSymbol('h');

            stats = new Stats();
            stats.SetWeight(1);
        }

        public void Inject(IInterractable interractable)
        {
            throw new NotImplementedException("Tried to inject interractable to a Handle. This is not defined behaviour yet");
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
            OnOpen?.Invoke();
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
}