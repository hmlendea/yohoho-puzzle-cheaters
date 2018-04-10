using System.Threading;

using YohohoPuzzleCheaters.Cheats.Poker.Entities;
using YohohoPuzzleCheaters.Common.Windows;

namespace YohohoPuzzleCheaters.Cheats.Poker
{
    public class PokerCheat
    {
        PokerBoard gameBoard;

        public PokerCheat()
        {
            gameBoard = new PokerBoard();

        }

        public void Start()
        {
            new Thread(() =>
            {
                while (WindowManager.Instance.CurrentScreen == ScreenType.BilgingScreen)
                {
                    PokerBoard board = RetrieveBoard();

                    if (gameBoard != board)
                    {
                        gameBoard = board;
                    }
                }
            }).Start();
        }

        PokerBoard RetrieveBoard()
        {
            PokerBoard board = new PokerBoard();


            return board;
        }
    }
}
