using System;

public struct Point : IEquatable<Point>
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static bool operator ==(Point a, Point b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Point a, Point b) => !(a == b);

    public static Point operator +(Point a, Point b) => new Point(a.X + b.X, a.Y + b.Y);
    public static Point operator -(Point a, Point b) => new Point(a.X - b.X, a.Y - b.Y);
    public static Point operator -(Point a) => new Point(-a.X, -a.Y);
    public static Point operator *(Point a, int scalar) => new Point(a.X * scalar, a.Y * scalar);
    public static Point operator *(int scalar, Point a) => new Point(a.X * scalar, a.Y * scalar);

    public static Point Zero => new Point(0, 0);
    public static Point Right => new Point(1, 0);
    public static Point Left => new Point(-1, 0);
    public static Point Up => new Point(0, 1);
    public static Point Down => new Point(0, -1);

    public bool Equals(Point other)
    {
        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
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