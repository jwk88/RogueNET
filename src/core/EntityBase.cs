using System;

public abstract class EntityBase
{
    public Guid Guid { get => guid; private set => guid = value; }
    Guid guid;

    public string Name { get => name; private set => name = value; }
    string name;

    public EntityBase()
    {
        guid = Guid.NewGuid();
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public override string ToString()
    {
        return $"'{Name}'";
    }
}
