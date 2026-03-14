public class Node
{
    Point point;
    Entity owner;

    public Entity Owner => owner;
    public bool Occupied => Owner != null;
    public Point Point => point;
    public int X => point.X;
    public int Y => point.Y;

    public Node(int x, int y)
    {
        owner = null;
        point = new Point(x, y);
    }

    public void SetOwner(Entity owner)
    {
        this.owner = owner;
    }
}
