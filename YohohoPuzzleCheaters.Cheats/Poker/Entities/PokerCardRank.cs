using System;

namespace YohohoPuzzleCheaters.Cheats.Poker.Entities
{
    public sealed class PokerCardRank : IEquatable<PokerCardRank>
    {
        public static PokerCardRank Unknown => new PokerCardRank(-1, "Unknown", 'U');
        public static PokerCardRank Two => new PokerCardRank(2, "Two", '2');
        public static PokerCardRank Three => new PokerCardRank(3, "Three", '3');
        public static PokerCardRank Four => new PokerCardRank(4, "Four", '4');
        public static PokerCardRank Five => new PokerCardRank(5, "Five", '5');
        public static PokerCardRank Six => new PokerCardRank(6, "Six", '6');
        public static PokerCardRank Seven => new PokerCardRank(7, "Seven", '7');
        public static PokerCardRank Eight => new PokerCardRank(8, "Eight", '8');
        public static PokerCardRank Nine => new PokerCardRank(9, "Nine", '9');
        public static PokerCardRank Ten => new PokerCardRank(10, "Ten", 'T');
        public static PokerCardRank Jester => new PokerCardRank(11, "Jester", 'J');
        public static PokerCardRank Queen => new PokerCardRank(12, "Queen", 'Q');
        public static PokerCardRank King => new PokerCardRank(13, "King", 'K');
        public static PokerCardRank Ace => new PokerCardRank(14, "Ace", 'A');

        public int Value { get; }

        public char Identifier { get; }

        public string Name { get; }

        PokerCardRank(int value, string name, char identifier)
        {
            Value = value;
            Name = name;
            Identifier = identifier;
        }

        public bool Equals(PokerCardRank other)
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

            return Equals((PokerCardRank)obj);
        }

        public override int GetHashCode()
        {
            return 234 ^ Value;
        }

        public override string ToString() => Name;

        public static bool operator ==(PokerCardRank cn1, PokerCardRank cn2) => cn1.Equals(cn2);

        public static bool operator !=(PokerCardRank cn1, PokerCardRank cn2) => !cn1.Equals(cn2);

        public static explicit operator int(PokerCardRank b) => b.Value;
    }
}
