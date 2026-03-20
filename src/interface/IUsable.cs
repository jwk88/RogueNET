namespace RogueNET
{
    public interface IUsable : IOwnable
    {
        public void Use(Actor user, Entity target);
    }
}