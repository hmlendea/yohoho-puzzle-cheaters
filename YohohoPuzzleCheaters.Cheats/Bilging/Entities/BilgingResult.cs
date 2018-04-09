using NuciXNA.Primitives;

namespace YohohoPuzzleCheaters.Cheats.Bilging.Entities
{
    public class BilgingResult
    {
        public Point2D Selection1 { get; set; }

        public Point2D Selection2 { get; set; }

        public bool IsSuccessful =>
            Selection1.X >= 0 && Selection1.Y >= 0 &&
            Selection2.X >= 0 && Selection2.Y >= 0;

        public static BilgingResult InvalidResult
        {
            get
            {
                return new BilgingResult
                {
                    Selection1 = new Point2D(-1, -1),
                    Selection2 = new Point2D(-1, -1)
                };
            }
        }
    }
}
