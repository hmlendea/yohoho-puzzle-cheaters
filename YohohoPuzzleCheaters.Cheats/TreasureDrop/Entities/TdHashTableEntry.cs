namespace YohohoPuzzleCheaters.Cheats.TreasureDrop.Entities
{
    public class TdHashTableEntry
    {
        public GameIdentifier Id { get; set; }

        public TdHashTableEntry Next { get; set; }

        public TdHashTableValue Value { get; set; }
    }
}
