using System.Threading;

using YohohoPuzzleCheaters.Cheats.Bilging.Entities;
using YohohoPuzzleCheaters.Common.Windows;

namespace YohohoPuzzleCheaters.Cheats.Bilging
{
    public class BilgingCheat
    {
        readonly BilgingBoardReader boardReader;
        readonly BilgingSolver bilgingSolver;

        BilgingBoard board;
        BilgingMove bestMove;

        public BilgingCheat()
        {
            boardReader = new BilgingBoardReader();
            bilgingSolver = new BilgingSolver();

            board = new BilgingBoard();
            board.WaterLevel = 3;
        }

        public void Start()
        {
            new Thread(() =>
            {
                while (WindowManager.Instance.CurrentScreen == ScreenType.BilgingScreen)
                {
                    BilgingBoard newBoard = boardReader.ReadBoard();

                    if (board != newBoard)
                    {
                        board = newBoard;
                        bestMove = bilgingSolver.CalculateMove(board);
                    }
                }
            }).Start();
        }

        public BilgingPiece GetPiece(int x, int y) => board[x, y];

        public bool IsBoardComplete() => !board.ContainsUnknownPieces && board.EmptyPiecesCount == 0;

        public BilgingMove GetBestTarget() => bestMove;
    }
}
