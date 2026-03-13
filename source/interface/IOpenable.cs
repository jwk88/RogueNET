public interface IOpenable : IInterractable
{
    public void SetOwner(Entity entity);
    public Entity Owner { get; }
    public bool IsOpen { get; }
    public bool IsLocked { get; }
}