public class Cell
{
    public int X;
    public int Y;

    public Cell(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return "{" + X + ":" + Y + "}";
    }
}