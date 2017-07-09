namespace YohohoPuzzleCheaters.Cheats.TreasureDrop.Entities
{
    public class MoveList
    {
        public int[] Slot { get; set; }

        public float[] Value { get; set; }

        public MoveList()
        {
            Slot = new int[8];
            Value = new float[8];
        }
    }
}
