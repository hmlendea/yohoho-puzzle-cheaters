using System;

namespace YohohoPuzzleCheaters.Cheats.Poker.Entities
{
    public sealed class PokerCardNumber : IEquatable<PokerCardNumber>
    {
        public static PokerCardNumber Unknown => new PokerCardNumber(-1, "Unknown", 'U');
        public static PokerCardNumber Two => new PokerCardNumber(2, "Two", '2');
        public static PokerCardNumber Three => new PokerCardNumber(3, "Three", '3');
        public static PokerCardNumber Four => new PokerCardNumber(4, "Four", '4');
        public static PokerCardNumber Five => new PokerCardNumber(5, "Five", '5');
        public static PokerCardNumber Six => new PokerCardNumber(6, "Six", '6');
        public static PokerCardNumber Seven => new PokerCardNumber(7, "Seven", '7');
        public static PokerCardNumber Eight => new PokerCardNumber(8, "Eight", '8');
        public static PokerCardNumber Nine => new PokerCardNumber(9, "Nine", '9');
        public static PokerCardNumber Ten => new PokerCardNumber(10, "Ten", 'T');
        public static PokerCardNumber Jester => new PokerCardNumber(11, "Jester", 'J');
        public static PokerCardNumber Queen => new PokerCardNumber(12, "Queen", 'Q');
        public static PokerCardNumber King => new PokerCardNumber(13, "King", 'K');
        public static PokerCardNumber Ace => new PokerCardNumber(14, "Ace", 'A');

        public int Value { get; }

        public char CharCode { get; }

        public string Description { get; }

        PokerCardNumber(int value, string name, char charCode)
        {
            Value = value;
            Description = name;
            CharCode = charCode;
        }

        public bool Equals(PokerCardNumber other)
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

            return Equals((PokerCardNumber)obj);
        }

        public override int GetHashCode()
        {
            return 234 ^ Value;
        }

        public override string ToString() => Description;

        public static bool operator ==(PokerCardNumber cn1, PokerCardNumber cn2) => cn1.Equals(cn2);

        public static bool operator !=(PokerCardNumber cn1, PokerCardNumber cn2) => !cn1.Equals(cn2);

        public static explicit operator int(PokerCardNumber b) => b.Value;
    }
}
