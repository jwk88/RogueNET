namespace RogueNET
{
    public class Rat : Actor
    {
        string gender;

        public Rat()
        {
            SetSymbol('r');
            SetName("Rat");

            gender = RogueNET.RNG.Next(0, 100) < 50 ? "Male" : "Female";

            stats = new Stats();
            if (gender == "Male") stats.SetWeightByBellCurve(0.45, 0.65);
            if (gender == "Female") stats.SetWeightByBellCurve(0.35, 0.45);
        }

        public override string ToString()
        {
            return $"{base.ToString()} ({gender})";
        }
    }
}