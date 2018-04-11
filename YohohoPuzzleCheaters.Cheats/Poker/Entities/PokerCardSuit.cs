using System;

namespace YohohoPuzzleCheaters.Cheats.Poker.Entities
{
    public sealed class PokerCardSuit : IEquatable<PokerCardSuit>
    {
        public static PokerCardSuit Unknown => new PokerCardSuit(-1, "Unknown", 'u');
        public static PokerCardSuit Spades => new PokerCardSuit(1, "Spades", 's');
        public static PokerCardSuit Hearts => new PokerCardSuit(2, "Hearts", 'h');
        public static PokerCardSuit Clubs => new PokerCardSuit(3, "Clubs", 'c');
        public static PokerCardSuit Diamonds => new PokerCardSuit(4, "Diamonds", 'd');

        public int Value { get; }

        public char Identifier { get; }

        public string Name { get; }

        PokerCardSuit(int value, string name, char identifier)
        {
            Value = value;
            Name = name;
            Identifier = identifier;
        }

        public bool Equals(PokerCardSuit other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (Value != other.Value)
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

            return Equals((PokerCardSuit)obj);
        }

        public override int GetHashCode()
        {
            return 123 ^ Value;
        }

        public override string ToString() => Name;

        public static bool operator ==(PokerCardSuit cc1, PokerCardSuit cc2) => cc1.Equals(cc2);

        public static bool operator !=(PokerCardSuit cc1, PokerCardSuit cc2) => !cc1.Equals(cc2);

        public static explicit operator int(PokerCardSuit cc) => cc.Value;
    }
}
