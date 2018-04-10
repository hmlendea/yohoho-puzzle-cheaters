namespace YohohoPuzzleCheaters.Cheats.Poker.Entities
{
    public class PokerCard
    {
        public PokerCardNumber Number { get; }

        public PokerCardColour Colour { get; }

        public PokerCard(PokerCardNumber number, PokerCardColour colour)
        {
            Number = number;
            Colour = colour;
        }
    }
}
