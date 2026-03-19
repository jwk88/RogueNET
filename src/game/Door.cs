public class Door : Entity, IInterractable
{
    public Door()
    {
        SetSymbol('D');
        SetName("Door");

        stats = new Stats();
        stats.SetWeight(100);
    }

    protected IOpenable openable;

    public void Inject(IInterractable interractable)
    {
        openable = (IOpenable)interractable;
        openable.SetOwner(this);

        openable.OnOpen = () =>
        {
            if (grid.GetFirstEmptyNeighbour(Node.Point, out var neighbour))
            {
                Node.SetOwner(null);
                neighbour.SetOwner(this);
            }
            else
            {
                Log.Info($"{this} had nowhere to move, it was destroyed!");
                Node.SetOwner(null);
            }
        };
    }

    public void InteractedBy(Actor actor)
    {
        if (openable == null)
        {
            Log.Warn($"{this} has no interaction set - if this is by design, you can ignore this warning");
            return;
        }
        openable.InteractedBy(actor);
    }
}