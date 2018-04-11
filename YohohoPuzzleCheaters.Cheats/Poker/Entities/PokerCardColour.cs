using System;

namespace YohohoPuzzleCheaters.Cheats.Poker.Entities
{
    public sealed class PokerCardColour : IEquatable<PokerCardColour>
    {
        public static PokerCardColour Unknown => new PokerCardColour(-1, "Unknown", 'u');
        public static PokerCardColour Spades => new PokerCardColour(1, "Spades", 's');
        public static PokerCardColour Hearts => new PokerCardColour(2, "Hearts", 'h');
        public static PokerCardColour Clubs => new PokerCardColour(3, "Clubs", 'c');
        public static PokerCardColour Diamonds => new PokerCardColour(4, "Diamonds", 'd');

        public int Value { get; }

        public char CharCode { get; }

        public string Description { get; }

        PokerCardColour(int value, string name, char charCode)
        {
            Value = value;
            Description = name;
            CharCode = charCode;
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

        public override string ToString() => Description;

        public static bool operator ==(PokerCardColour cc1, PokerCardColour cc2) => cc1.Equals(cc2);

        public static bool operator !=(PokerCardColour cc1, PokerCardColour cc2) => !cc1.Equals(cc2);

        public static explicit operator int(PokerCardColour cc) => cc.Value;
    }
}
