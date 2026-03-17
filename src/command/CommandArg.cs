public class CommandArg
{
    public string String;
    public int? Integer;
    public bool? Boolean;
    public double? Double;

    public bool IsInteger => Integer.HasValue;
    public bool IsBool => Boolean.HasValue;
    public bool IsDouble => Double.HasValue;

    public CommandArg(string value)
    {
        String = value;
        Integer = null;
        Boolean = null;
        Double = null;

        if (int.TryParse(value, out var i)) 
            Integer = i;
        else if (double.TryParse(value, out var d)) 
            Double = d;
        else if (bool.TryParse(value, out var b)) 
            Boolean = b;
    }
}
