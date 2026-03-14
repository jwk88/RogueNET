using System;

public abstract class EntityBase
{
    Guid guid;
    string name;
    char symbol;

    public Guid Guid => guid;
    public string Name => name;
    public char Symbol => symbol;

    public EntityBase()
    {
        guid = Guid.NewGuid();
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public void SetSymbol(char symbol)
    {
        this.symbol = symbol;
    }

    public override string ToString()
    {
        return $"'{Name}'";
    }
}
