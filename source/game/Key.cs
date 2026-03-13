public class Key : Entity, IPickupable
{
    public void PickedUpBy(Actor actor)
    {
        actor.AddToInventory(this);
    }
}