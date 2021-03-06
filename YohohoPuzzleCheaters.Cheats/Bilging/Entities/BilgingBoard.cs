﻿using System;
using System.Linq;

namespace YohohoPuzzleCheaters.Cheats.Bilging.Entities
{
    public class BilgingBoard : IEquatable<BilgingBoard>
    {
        readonly BilgingPiece[] pieces;

        public int Width { get; }

        public int Height { get; }

        public int WaterLevel { get; set; }

        public int EmptyPiecesCount => emptyPiecesCount;

        public int CrabsCount => crabsCount;

        int emptyPiecesCount;
        int crabsCount;

        public BilgingBoard()
        {
            Width = 6;
            Height = 12;

            pieces = new BilgingPiece[Width * Height];

            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i] = BilgingPiece.Unknown;
            }
        }

        public BilgingPiece this[int x, int y]
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

        public BilgingPiece this[int index]
        {
            get
            {
                return pieces[index];
            }
            set
            {
                if (value.Type == BilgingPieceType.Empty)
                {
                    emptyPiecesCount += 1;
                }
                else if (pieces[index].Type == BilgingPieceType.Empty && value.Type != BilgingPieceType.Empty)
                {
                    emptyPiecesCount -= 1;
                }

                if (value.Type == BilgingPieceType.Crab)
                {
                    crabsCount += 1;
                }
                else if (pieces[index].Type == BilgingPieceType.Crab && value.Type != BilgingPieceType.Crab)
                {
                    crabsCount -= 1;
                }

                pieces[index] = value;
            }
        }

        public bool ContainsUnknownPieces => pieces.Any(x => x.Type == BilgingPieceType.Unknown);

        public BilgingBoard CreateCopy()
        {
            BilgingBoard board = new BilgingBoard();
            board.WaterLevel = WaterLevel;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
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

            return Equals((BilgingBoard)obj);
        }

        public override int GetHashCode()
        {
            int hash = 613 ^ WaterLevel;

            for (int i = 0; i < Width * Height; i++)
            {
                hash ^= this[i].GetHashCode();
            }

            return hash;
        }

        public static bool operator ==(BilgingBoard me, BilgingBoard other)
        {
            if (ReferenceEquals(other, null))
            {
                return ReferenceEquals(me, null);
            }

            return me.Equals(other);
        }

        public static bool operator !=(BilgingBoard me, BilgingBoard other) => !(me == other);
    }
}
