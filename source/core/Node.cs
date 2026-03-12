public class Node : Cell
{
    public Entity Owner { get => owner; set => owner = value; }
    Entity owner;

    public bool Occupied { get => Owner != null; }

    public Node(int x, int y) : base(x, y)
    {
        owner = null;
    }
}
