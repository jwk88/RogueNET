using System;

public struct Point : IEquatable<Point>
{
    public int X;
    public int Y;
    public bool Valid;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
        Valid = true;
    }

    public bool Equals(Point other)
    {
        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static bool operator==(Point a, Point b)
    {
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator!=(Point a, Point b)
    {
        return !(a == b);
    }

    public override string ToString()
    {
        return "{" + X + ":" + Y + "}";
    }

    public override bool Equals(object obj)
    {
        return obj is Point && Equals((Point)obj);
    }
}