using NuciXNA.Primitives;

namespace YohohoPuzzleCheaters.Models
{
    public class BilgingResult
    {
        public Point2D Selection1 { get; set; }

        public Point2D Selection2 { get; set; }

        public bool IsSuccessful =>
            Selection1.X >= 0 && Selection1.Y >= 0 &&
            Selection2.X >= 0 && Selection2.Y >= 0;
    }
}
