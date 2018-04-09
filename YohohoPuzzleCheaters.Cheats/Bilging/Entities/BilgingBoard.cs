using System;
using System.Linq;

namespace YohohoPuzzleCheaters.Cheats.Bilging.Entities
{
    public class BilgingBoard : IEquatable<BilgingBoard>
    {
        public const short BoardWidth = 6;
        public const short BoardHeight = 12;

        readonly BilgingPiece[] pieces;

        public int WaterLevel { get; set; }

        public BilgingBoard()
        {
            pieces = new BilgingPiece[BoardWidth * BoardHeight];

            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i] = BilgingPiece.Unknown;
            }
        }

        public BilgingPiece this[int x, int y]
        {
            get
            {
                return pieces[y * BoardWidth + x];
            }
            set
            {
                pieces[y * BoardWidth + x] = value;
            }
        }

        public BilgingPiece this[int index]
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

        public bool ContainsUnknownPieces => pieces.Any(x => x.Type == BilgingPieceType.Unknown);

        public int EmptyPiecesCount => pieces.Count(x => x.Type == BilgingPieceType.Empty);

        public int CrabsCount => pieces.Count(x => x.Type == BilgingPieceType.Crab);

        public BilgingBoard CreateCopy()
        {
            BilgingBoard board = new BilgingBoard();
            board.WaterLevel = WaterLevel;

            for (int y = 0; y < BoardHeight; y++)
            {
                for (int x = 0; x < BoardWidth; x++)
                {
                    board[x, y] = new BilgingPiece(this[x, y].Id, this[x, y].Type);
                }
            }

            return board;
        }

        public bool Equals(BilgingBoard other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (WaterLevel != other.WaterLevel)
            {
                return false;
            }

            for (int i = 0; i < BoardWidth * BoardHeight; i++)
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

            return Equals((BilgingBoard)obj);
        }

        public override int GetHashCode()
        {
            int hash = 613 ^ WaterLevel;

            for (int i = 0; i < BoardWidth * BoardHeight; i++)
            {
                hash ^= this[i].GetHashCode();
            }

            return hash;
        }

        public static bool operator ==(BilgingBoard b1, BilgingBoard b2) => b1.Equals(b2);

        public static bool operator !=(BilgingBoard b1, BilgingBoard b2) => !b1.Equals(b2);
    }
}
