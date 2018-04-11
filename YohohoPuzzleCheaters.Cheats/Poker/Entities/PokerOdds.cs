namespace YohohoPuzzleCheaters.Cheats.Poker.Entities
{
    public class PokerOdds
    {
        public double HighCard { get; set; }

        public double OnePair { get; set; }

        public double TwoPair { get; set; }

        public double ThreeOfKind { get; set; }

        public double Straight { get; set; }

        public double Flush { get; set; }

        public double FullHouse { get; set; }

        public double FourOfKind { get; set; }

        public double StraightFlush { get; set; }

        public double WinningOdds
        {
            get
            {
                double odds = 0.0;

                odds += HighCard * 100.0;
                odds += OnePair * 100.0;
                odds += TwoPair * 100.0;
                odds += ThreeOfKind * 100.0;
                odds += Straight * 100.0;
                odds += Flush * 100.0;
                odds += FullHouse * 100.0;
                odds += FourOfKind * 100.0;
                odds += StraightFlush * 100.0;

                return odds;
            }
        }
    }
}
