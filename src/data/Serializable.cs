namespace RogueNET
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public abstract class Serializable<T> where T : new()
    {
        const string delimiter = " = ";

        public string Serialize()
        {
            var flags = BindingFlags.Public | BindingFlags.Public | BindingFlags.Instance;
            var fields = GetType().GetFields(flags);
            var output = new StringBuilder();
            foreach (var field in fields)
            {
                var value = field.GetValue(this);
                output.Append(field.Name);
                output.Append(delimiter);
                output.Append(value);
                output.AppendLine();
            }
            return output.ToString();
        }

        public bool TrySetField(string fieldName, CommandArg value)
        {
            var flags = BindingFlags.Public | BindingFlags.Public | BindingFlags.Instance;
            var fields = GetType().GetFields(flags);
            var found = false;
            foreach (var field in fields)
            {
                var toLower = field.Name.ToLower();
                if (toLower == fieldName.ToLower())
                {
                    found = true;
                    try
                    {
                        if (value.IsInteger)
                            field.SetValue(this, value.Integer.Value);
                        if (value.IsBool)
                            field.SetValue(this, value.Boolean.Value);
                        if (value.IsDouble)
                            field.SetValue(this, value.Double.Value);
                    }
                    catch
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        
            return found;
        }

        public static T Deserialize(string content)
        {
            var config = new T();
            var flags = BindingFlags.Public | BindingFlags.Public | BindingFlags.Instance;
            var fields = typeof(T).GetFields(flags);
            var split = content.Split(new [] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in split)
            {
                var inner = line.Split(delimiter);
            
                var field = inner[0];
                var value = inner[1];

                var match = fields.FirstOrDefault(x => x.Name == field);
                var command = new Command(field, value);

                if (command.Args[1].IsInteger) match.SetValue(config, command.Args[1].Integer.Value);
                if (command.Args[1].IsBool) match.SetValue(config, command.Args[1].Boolean.Value);
                if (command.Args[1].IsDouble) match.SetValue(config, command.Args[1].Double.Value);
            }

            return config;
        }
    }
}
