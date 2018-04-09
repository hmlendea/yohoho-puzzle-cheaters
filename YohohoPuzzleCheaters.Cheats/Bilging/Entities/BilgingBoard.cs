using System.Linq;

namespace YohohoPuzzleCheaters.Cheats.Bilging.Entities
{
    public class BilgingBoard
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

        public int UnknownPieces => pieces.Count(x => x.Type == BilgingPieceType.Unknown);

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
    }
}
