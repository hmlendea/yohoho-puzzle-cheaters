using System;

namespace YohohoPuzzleCheaters.Cheats.Patching.Entities
{
    public class PatchingPiece : IEquatable<PatchingPiece>
    {
        public static PatchingPiece Unknown => new PatchingPiece(-1, PatchingPieceType.Unknown, PatchingPieceDirection.None);

        public static PatchingPiece SpoolLeft => new PatchingPiece(0, PatchingPieceType.Spool, PatchingPieceDirection.Left);
        public static PatchingPiece SpoolUp => new PatchingPiece(1, PatchingPieceType.Spool, PatchingPieceDirection.Up);
        public static PatchingPiece SpoolRight => new PatchingPiece(2, PatchingPieceType.Spool, PatchingPieceDirection.Right);
        public static PatchingPiece SpoolDown => new PatchingPiece(3, PatchingPieceType.Spool, PatchingPieceDirection.Down);
        public static PatchingPiece TieOffLeft => new PatchingPiece(4, PatchingPieceType.TieOff, PatchingPieceDirection.Left);
        public static PatchingPiece TieOffUp => new PatchingPiece(5, PatchingPieceType.TieOff, PatchingPieceDirection.Up);
        public static PatchingPiece TieOffRight => new PatchingPiece(6, PatchingPieceType.TieOff, PatchingPieceDirection.Right);
        public static PatchingPiece TieOffDown => new PatchingPiece(7, PatchingPieceType.TieOff, PatchingPieceDirection.Down);
        public static PatchingPiece Blocker => new PatchingPiece(8, PatchingPieceType.Blocker, PatchingPieceDirection.None);
        public static PatchingPiece ElbowLeftUp => new PatchingPiece(12, PatchingPieceType.Elbow, PatchingPieceDirection.Up);
        public static PatchingPiece ElbowRightUp => new PatchingPiece(13, PatchingPieceType.Elbow, PatchingPieceDirection.Up);
        public static PatchingPiece ElbowRightDown => new PatchingPiece(14, PatchingPieceType.Elbow, PatchingPieceDirection.Down);
        public static PatchingPiece ElbowLeftDown => new PatchingPiece(15, PatchingPieceType.Elbow, PatchingPieceDirection.Down);
        public static PatchingPiece TeeLeft => new PatchingPiece(16, PatchingPieceType.Tee, PatchingPieceDirection.Left);
        public static PatchingPiece TeeUp => new PatchingPiece(17, PatchingPieceType.Tee, PatchingPieceDirection.Up);
        public static PatchingPiece TeeRight => new PatchingPiece(18, PatchingPieceType.Tee, PatchingPieceDirection.Right);
        public static PatchingPiece TeeDown => new PatchingPiece(19, PatchingPieceType.Tee, PatchingPieceDirection.Down);
        public static PatchingPiece GrommetLeft => new PatchingPiece(20, PatchingPieceType.Grommet, PatchingPieceDirection.Left);
        public static PatchingPiece GrommetUp => new PatchingPiece(21, PatchingPieceType.Grommet, PatchingPieceDirection.Up);
        public static PatchingPiece GrommetRight => new PatchingPiece(22, PatchingPieceType.Grommet, PatchingPieceDirection.Right);
        public static PatchingPiece GrommetDown => new PatchingPiece(23, PatchingPieceType.Grommet, PatchingPieceDirection.Down);
        public static PatchingPiece StraightHorizontal => new PatchingPiece(24, PatchingPieceType.Straight, PatchingPieceDirection.Left);
        public static PatchingPiece StraightVertical => new PatchingPiece(25, PatchingPieceType.Straight, PatchingPieceDirection.Up);
        public static PatchingPiece Cross => new PatchingPiece(26, PatchingPieceType.Cross, PatchingPieceDirection.None);

        public int Value { get; }

        public PatchingPieceType Type { get; }

        public PatchingPieceDirection Direction { get; }

        public PatchingPiece(int value, PatchingPieceType type, PatchingPieceDirection direction)
        {
            Value = value;
            Type = type;
            Direction = direction;
        }

        public bool Equals(PatchingPiece other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (Value != other.Value || Type != other.Type || Direction != other.Direction)
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

            return Equals((PatchingPiece)obj);
        }

        public override int GetHashCode()
        {
            return 345 ^ Value;
        }

        public static bool operator ==(PatchingPiece obj1, PatchingPiece obj2) => obj1.Equals(obj2);

        public static bool operator !=(PatchingPiece obj1, PatchingPiece obj2) => !obj1.Equals(obj2);

        public static explicit operator int(PatchingPiece obj) => obj.Value;
    }
}
