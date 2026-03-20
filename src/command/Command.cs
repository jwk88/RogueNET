namespace RogueNET
{
    public class Command
    {
        public CommandArg[] Args;
        public int Length;

        public Command(params string[] args)
        {
            Length = args.Length;
            Args = new CommandArg[Length];

            for (int i = 0; i < Length; i++)
            {
                Args[i] = new CommandArg(args[i]);
            }
        }

        public override string ToString()
        {
            var command = "";
            foreach (var arg in Args)
            {
                if (arg.IsBool)
                    command += "Boolean: " + arg.Boolean.Value;
                else if (arg.IsInteger)
                    command += "Integer: " + arg.Integer.Value;
                else if (arg.IsDouble)
                    command += "Double: " + arg.Double.Value;
                else
                    command += "String: " + arg.String;

                command += " ";
            }
            return command;
        }
    }
}