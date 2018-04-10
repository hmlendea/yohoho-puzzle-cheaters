using System.Collections.Generic;

namespace YohohoPuzzleCheaters.Cheats.Poker.Entities
{
    public class PokerBoard
    {
        public IList<PokerCard> PlayerCards { get; set; }

        public IList<PokerCard> TableCards { get; set; }
    }
}
