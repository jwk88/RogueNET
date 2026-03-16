using System.IO;

public abstract class Test
{
    protected Grid grid;
    protected ConsoleManager console = new ConsoleManager();

    public abstract bool Run(bool saveOutput = false);

    public bool Validate(string output, bool saveOutput = false)
    {
        if (saveOutput)
        {
            File.WriteAllText($"{this}.txt", output);
            return true;
        }
        else
        {
            var test = File.ReadAllText($"{this}.txt");
            if (test == output)
            {
                return true;
            }
        }
        return false;
    }

    public override string ToString()
    {
        return GetType().Name;
    }
}
