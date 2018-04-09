namespace YohohoPuzzleCheaters.Cheats.Bilging.Entities
{
    public class BilgingMove
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Score { get; set; }

        public static BilgingMove InvalidMove => new BilgingMove { X = -1, Y = -1, Score = -1 };
    }
}
