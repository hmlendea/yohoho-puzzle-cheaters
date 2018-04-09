using System;

namespace YohohoPuzzleCheaters.Cheats.Bilging.Entities
{
    public class BilgingPiece : IEquatable<BilgingPiece>
    {
        public int Id { get; }

        public BilgingPieceType Type { get; }

        public BilgingPiece(int id, BilgingPieceType type)
        {
            Id = id;
            Type = type;
        }

        public bool Equals(BilgingPiece other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
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

            return Equals((BilgingPiece)obj);
        }

        public override int GetHashCode()
        {
            return 873 ^ Id;
        }

        public static bool operator ==(BilgingPiece p1, BilgingPiece p2) => p1.Equals(p2);

        public static bool operator !=(BilgingPiece p1, BilgingPiece p2) => !p1.Equals(p2);

        public static BilgingPiece Unknown => new BilgingPiece(-2, BilgingPieceType.Unknown);
        public static BilgingPiece Empty => new BilgingPiece(-1, BilgingPieceType.Empty);
        public static BilgingPiece SquareDark => new BilgingPiece(0, BilgingPieceType.Movable);
        public static BilgingPiece SquareLight => new BilgingPiece(1, BilgingPieceType.Movable);
        public static BilgingPiece CircleDark => new BilgingPiece(2, BilgingPieceType.Movable);
        public static BilgingPiece CircleLight => new BilgingPiece(3, BilgingPieceType.Movable);
        public static BilgingPiece OctogonDark => new BilgingPiece(4, BilgingPieceType.Movable);
        public static BilgingPiece OctogonLight => new BilgingPiece(5, BilgingPieceType.Movable);
        public static BilgingPiece PentagonDark => new BilgingPiece(6, BilgingPieceType.Movable);
        public static BilgingPiece Pufferfish => new BilgingPiece(7, BilgingPieceType.Pufferfish);
        public static BilgingPiece Jellyfish => new BilgingPiece(8, BilgingPieceType.Jellyfish);
        public static BilgingPiece Crab => new BilgingPiece(9, BilgingPieceType.Crab);
    }
}
