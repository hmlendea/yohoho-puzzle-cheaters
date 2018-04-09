namespace YohohoPuzzleCheaters.Cheats.Bilging.Entities
{
    public class BilgingBoard
    {
        public const short BoardWidth = 6;
        public const short BoardHeight = 12;

        readonly BilgingPieceType[,] pieces;

        public int WaterDepth { get; set; }

        public BilgingBoard()
        {
            pieces = new BilgingPieceType[BoardWidth, BoardHeight];
        }

        public BilgingPieceType this[int x, int y]
        {
            get
            {
                return pieces[x, y];
            }
            set
            {
                pieces[x, y] = value;
            }
        }

        public BilgingPieceType this[int index]
        {
            get
            {
                int x = index % BoardWidth;
                int y = index / BoardHeight;

                return pieces[x, y];
            }
            set
            {
                int x = index % BoardWidth;
                int y = index / BoardHeight;

                pieces[x, y] = value;
            }
        }

        public bool ContainsUnknownPieces()
        {
            for (int y = 0; y < BoardHeight; y++)
            {
                for (int x = 0; x < BoardWidth; x++)
                {
                    if (pieces[x, y] == BilgingPieceType.Unknown)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
