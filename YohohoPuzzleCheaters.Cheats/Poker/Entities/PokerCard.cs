namespace YohohoPuzzleCheaters.Cheats.Poker.Entities
{
    public class PokerCard
    {
        public PokerCardRank Rank { get; }

        public PokerCardSuit Suit { get; }

        public string Code => $"{Rank.Identifier}{Suit.Identifier}";

        public PokerCard(PokerCardRank rank, PokerCardSuit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public override string ToString() => $"{Rank.Name} of {Suit.Name}";
    }
}
