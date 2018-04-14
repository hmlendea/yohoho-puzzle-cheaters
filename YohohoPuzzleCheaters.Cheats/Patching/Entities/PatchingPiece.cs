using System;
using System.Collections.Generic;

namespace YohohoPuzzleCheaters.Cheats.Patching.Entities
{
    public class PatchingPiece : IEquatable<PatchingPiece>
    {
        static readonly Dictionary<int, PatchingPiece> PatchingPieces = new Dictionary<int, PatchingPiece>
        {
            { -1, new PatchingPiece(-1, PatchingPieceType.Unknown, PatchingPieceDirection.Left) },
            { 0, new PatchingPiece(0, PatchingPieceType.Spool, PatchingPieceDirection.Left) },
            { 1, new PatchingPiece(1, PatchingPieceType.Spool, PatchingPieceDirection.Up) },
            { 2, new PatchingPiece(2, PatchingPieceType.Spool, PatchingPieceDirection.Right) },
            { 3, new PatchingPiece(3, PatchingPieceType.Spool, PatchingPieceDirection.Down) },
            { 4, new PatchingPiece(4, PatchingPieceType.TieOff, PatchingPieceDirection.Left) },
            { 5, new PatchingPiece(5, PatchingPieceType.TieOff, PatchingPieceDirection.Up) },
            { 6, new PatchingPiece(6, PatchingPieceType.TieOff, PatchingPieceDirection.Right) },
            { 7, new PatchingPiece(7, PatchingPieceType.TieOff, PatchingPieceDirection.Down) },
            { 8, new PatchingPiece(8, PatchingPieceType.Blocker, PatchingPieceDirection.Left) },
            { 12, new PatchingPiece(12, PatchingPieceType.Elbow, PatchingPieceDirection.Left) },
            { 13, new PatchingPiece(13, PatchingPieceType.Elbow, PatchingPieceDirection.Up) },
            { 14, new PatchingPiece(14, PatchingPieceType.Elbow, PatchingPieceDirection.Right) },
            { 15, new PatchingPiece(15, PatchingPieceType.Elbow, PatchingPieceDirection.Down) },
            { 16, new PatchingPiece(16, PatchingPieceType.Tee, PatchingPieceDirection.Left) },
            { 17, new PatchingPiece(17, PatchingPieceType.Tee, PatchingPieceDirection.Up) },
            { 18, new PatchingPiece(18, PatchingPieceType.Tee, PatchingPieceDirection.Right) },
            { 19, new PatchingPiece(19, PatchingPieceType.Tee, PatchingPieceDirection.Down) },
            { 20, new PatchingPiece(20, PatchingPieceType.Grommet, PatchingPieceDirection.Left) },
            { 21, new PatchingPiece(21, PatchingPieceType.Grommet, PatchingPieceDirection.Up) },
            { 22, new PatchingPiece(22, PatchingPieceType.Grommet, PatchingPieceDirection.Right) },
            { 23, new PatchingPiece(23, PatchingPieceType.Grommet, PatchingPieceDirection.Down) },
            { 24, new PatchingPiece(24, PatchingPieceType.Straight, PatchingPieceDirection.Left) },
            { 25, new PatchingPiece(25, PatchingPieceType.Straight, PatchingPieceDirection.Up) },
            { 28, new PatchingPiece(28, PatchingPieceType.Cross, PatchingPieceDirection.Left) }
        };

        public static PatchingPiece Unknown => PatchingPieces[-1];
        public static PatchingPiece SpoolLeft => PatchingPieces[0];
        public static PatchingPiece SpoolUp => PatchingPieces[1];
        public static PatchingPiece SpoolRight => PatchingPieces[2];
        public static PatchingPiece SpoolDown => PatchingPieces[3];
        public static PatchingPiece TieOffLeft => PatchingPieces[4];
        public static PatchingPiece TieOffUp => PatchingPieces[5];
        public static PatchingPiece TieOffRight => PatchingPieces[6];
        public static PatchingPiece TieOffDown => PatchingPieces[7];
        public static PatchingPiece Blocker => PatchingPieces[8];
        public static PatchingPiece ElbowLeftUp => PatchingPieces[12];
        public static PatchingPiece ElbowRightUp => PatchingPieces[13];
        public static PatchingPiece ElbowRightDown => PatchingPieces[14];
        public static PatchingPiece ElbowLeftDown => PatchingPieces[15];
        public static PatchingPiece TeeLeft => PatchingPieces[16];
        public static PatchingPiece TeeUp => PatchingPieces[17];
        public static PatchingPiece TeeRight => PatchingPieces[18];
        public static PatchingPiece TeeDown => PatchingPieces[19];
        public static PatchingPiece GrommetLeft => PatchingPieces[20];
        public static PatchingPiece GrommetUp => PatchingPieces[21];
        public static PatchingPiece GrommetRight => PatchingPieces[22];
        public static PatchingPiece GrommetDown => PatchingPieces[23];
        public static PatchingPiece StraightHorizontal => PatchingPieces[24];
        public static PatchingPiece StraightVertical => PatchingPieces[25];
        public static PatchingPiece Cross => PatchingPieces[28];

        public int Value { get; }

        public PatchingPieceType Type { get; }

        public PatchingPieceDirection Direction { get; }

        public bool IsMoveable
        {
            get
            {
                if (Type == PatchingPieceType.Elbow ||
                    Type == PatchingPieceType.Tee ||
                    Type == PatchingPieceType.Grommet ||
                    Type == PatchingPieceType.Straight ||
                    Type == PatchingPieceType.Cross)
                {
                    return true;
                }

                return false;
            }
        }

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
            return 345 ^ Value ^ (int)Type ^ (int)Direction;
        }

        public static bool operator ==(PatchingPiece obj1, PatchingPiece obj2) => obj1.Equals(obj2);

        public static bool operator !=(PatchingPiece obj1, PatchingPiece obj2) => !obj1.Equals(obj2);

        public static explicit operator int(PatchingPiece obj) => obj.Value;

        public static explicit operator PatchingPiece(int value) => PatchingPieces[value];
    }
}
