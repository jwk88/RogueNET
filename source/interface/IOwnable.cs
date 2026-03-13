public interface IOwnable
{
    public void SetOwner(Entity entity);
    public Entity Owner { get; }
}