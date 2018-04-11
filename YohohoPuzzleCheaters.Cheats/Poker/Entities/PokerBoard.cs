using System;
using System.Collections.Generic;
using System.Linq;

namespace YohohoPuzzleCheaters.Cheats.Poker.Entities
{
    public class PokerBoard : IEquatable<PokerBoard>
    {
        public List<PokerCard> Pocket { get; set; }

        public List<PokerCard> Deck { get; set; }

        public PokerBoard()
        {
            Pocket = new List<PokerCard>();
            Deck = new List<PokerCard>();
        }

        public bool Equals(PokerBoard other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (Pocket.Count != other.Pocket.Count ||
                Deck.Count != other.Deck.Count)
            {
                return false;
            }

            // TODO: The following checks might not be ok

            if (!Pocket.All(other.Pocket.Contains))
            {
                return false;
            }

            if (!Deck.All(other.Deck.Contains))
            {
                return false;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (this == null || obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((PokerBoard)obj);
        }

        public override int GetHashCode()
        {
            int hash = 613;

            foreach (PokerCard card in Pocket)
            {
                hash ^= card.Number.GetHashCode() ^ card.Colour.GetHashCode();
            }

            foreach (PokerCard card in Deck)
            {
                hash ^= card.Number.GetHashCode() ^ card.Colour.GetHashCode();
            }

            return hash;
        }

        public static bool operator ==(PokerBoard b1, PokerBoard b2) => b1.Equals(b2);

        public static bool operator !=(PokerBoard b1, PokerBoard b2) => !b1.Equals(b2);
    }
}
