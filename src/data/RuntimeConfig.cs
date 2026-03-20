namespace RogueNET
{
    public class RuntimeConfig : Serializable<RuntimeConfig>
    {
        public int Seed = 2;
        public int GridWidth = 128;
        public int GridDepth = 64;
        public int RoomMinWidth = 24;
        public int RoomMinDepth = 8;
        public int RoomDiscardChance = 5;
    }
}
