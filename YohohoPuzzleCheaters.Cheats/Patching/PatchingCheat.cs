using System.Threading;

using YohohoPuzzleCheaters.Cheats.Patching.Entities;
using YohohoPuzzleCheaters.Common.Windows;

namespace YohohoPuzzleCheaters.Cheats.Patching
{
    public class PatchingCheat
    {
        readonly PatchingBoardReader boardReader;
        readonly PatchingSolver solver;

        PatchingBoard board;
        PatchingBoard solution;

        public PatchingCheat()
        {
            boardReader = new PatchingBoardReader();
            solver = new PatchingSolver();

            board = new PatchingBoard(0, 0);
        }

        public PatchingBoard Solution => solution;

        public void Start()
        {
            new Thread(() =>
            {
                while (WindowManager.Instance.CurrentScreen == ScreenType.PatchingScreen)
                {
                    PatchingBoard newBoard = boardReader.ReadBoard();

                    if (newBoard != board && !newBoard.ContainsUnknownPieces)
                    {
                        board = newBoard;

                        System.Console.WriteLine(System.DateTime.Now + " Calc");
                        solution = solver.CalculateSolution(board);
                    }
                }
            }).Start();
        }
    }
}
