public class Key : Entity, IPickup
{
    public void Pickup(Actor actor)
    {
        actor.Pickup(this);
    }
}