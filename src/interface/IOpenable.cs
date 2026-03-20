namespace RogueNET
{
    using System;

    public interface IOpenable : IInterractable, IOwnable
    {
        public bool IsOpen { get; }
        public bool IsLocked { get; }
        public void Unlock();
        public void Lock();
        public Action OnOpen { get; set; }
    }
}