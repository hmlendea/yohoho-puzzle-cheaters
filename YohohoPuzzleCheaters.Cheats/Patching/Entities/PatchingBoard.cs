using System;
using System.Linq;

namespace YohohoPuzzleCheaters.Cheats.Patching.Entities
{
    public class PatchingBoard : IEquatable<PatchingBoard>
    {
        readonly PatchingPiece[] pieces;

        public int Width { get; }

        public int Height { get; }

        public int Size { get; }

        public PatchingBoard(int width, int height)
        {
            Width = width;
            Height = height;
            Size = width * height;

            pieces = new PatchingPiece[Size];

            for (int i = 0; i < Size; i++)
            {
                pieces[i] = PatchingPiece.Unknown;
            }
        }

        public PatchingPiece this[int x, int y]
        {
            get
            {
                return pieces[y * Width + x];
            }
            set
            {
                pieces[y * Width + x] = value;
            }
        }

        public PatchingPiece this[int index]
        {
            get
            {
                return pieces[index];
            }
            set
            {
                pieces[index] = value;
            }
        }

        public bool ContainsUnknownPieces => pieces.Any(x => x.Type == PatchingPieceType.Unknown);

        public int BlockerCount => pieces.Count(x => x.Type == PatchingPieceType.Blocker);

        public PatchingBoard CreateCopy()
        {
            PatchingBoard board = new PatchingBoard(Width, Height);

            for (int i = 0; i < Size; i++)
            {
                board[i] = this[i];
            }

            return board;
        }

        public bool Equals(PatchingBoard other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (Width != other.Width || Height != other.Height)
            {
                return false;
            }

            for (int i = 0; i < Width * Height; i++)
            {
                if (this[i] != other[i])
                {
                    return false;
                }
            }

            return true;
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

            return Equals((PatchingBoard)obj);
        }

        public override int GetHashCode()
        {
            int hash = 911 ^ Width ^ Height;

            for (int i = 0; i < Width * Height; i++)
            {
                hash ^= this[i].GetHashCode();
            }

            return hash;
        }

        public static bool operator ==(PatchingBoard me, PatchingBoard other)
        {
            if (ReferenceEquals(null, me) && ReferenceEquals(null, other))
            {
                return true;
            }

            return me.Equals(other);
        }

        public static bool operator !=(PatchingBoard me, PatchingBoard other) => !(me == other);
    }
}
