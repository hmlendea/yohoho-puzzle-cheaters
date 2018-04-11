namespace YohohoPuzzleCheaters.Cheats.Poker.Entities
{
    public class PokerCard
    {
        public PokerCardNumber Number { get; }

        public PokerCardColour Colour { get; }

        public string Code => $"{Number.CharCode}{Colour.CharCode}";

        public PokerCard(PokerCardNumber number, PokerCardColour colour)
        {
            Number = number;
            Colour = colour;
        }

        public override string ToString() => $"{Number.Description} of {Colour.Description}";
    }
}
