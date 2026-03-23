using System;

public struct RoomData : IEquatable<RoomData>
{
    public Point Origin;
    public int Width;
    public int Depth;
    public bool Offline;

    public bool Equals(RoomData other)
    {
        return Origin == other.Origin &&
               Width == other.Width &&
               Depth == other.Depth;
    }

    public override bool Equals(object obj)
    {
        return obj is RoomData other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Origin, Width, Depth);
    }
}
