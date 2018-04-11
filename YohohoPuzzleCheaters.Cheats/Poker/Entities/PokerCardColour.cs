using System;

namespace YohohoPuzzleCheaters.Cheats.Poker.Entities
{
    public sealed class PokerCardColour : IEquatable<PokerCardColour>
    {
        public static PokerCardColour Unknown => new PokerCardColour(-1, "u");
        public static PokerCardColour Spades => new PokerCardColour(1, "s");
        public static PokerCardColour Hearts => new PokerCardColour(2, "h");
        public static PokerCardColour Clubs => new PokerCardColour(3, "c");
        public static PokerCardColour Diamonds => new PokerCardColour(4, "d");

        public int Value { get; }

        string stringRepresentation;

        PokerCardColour(int value, string stringRepresentation)
        {
            Value = value;

            this.stringRepresentation = stringRepresentation;
        }

        public bool Equals(PokerCardColour other)
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

            return Equals((PokerCardColour)obj);
        }

        public override int GetHashCode()
        {
            return 123 ^ Value;
        }

        public override string ToString() => stringRepresentation;

        public static bool operator ==(PokerCardColour cc1, PokerCardColour cc2) => cc1.Equals(cc2);

        public static bool operator !=(PokerCardColour cc1, PokerCardColour cc2) => !cc1.Equals(cc2);

        public static explicit operator int(PokerCardColour cc) => cc.Value;
    }
}
