using System;

public abstract class EntityBase
{
    public Guid Guid { get => guid; private set => guid = value; }
    Guid guid;

    public EntityBase()
    {
        guid = Guid.NewGuid();
    }

    public override string ToString()
    {
        return GetType().Name;
    }
}
