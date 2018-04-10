using System;
using System.Collections.Generic;
using System.Threading;

using YohohoPuzzleCheaters.Cheats.Poker.Entities;
using YohohoPuzzleCheaters.Common.Windows;

namespace YohohoPuzzleCheaters.Cheats.Poker
{
    public class PokerCheat
    {
        readonly PokerBoardReader boardReader;

        PokerBoard gameBoard;

        public PokerCheat()
        {
            boardReader = new PokerBoardReader();
            gameBoard = new PokerBoard();
        }

        public void Start()
        {
            new Thread(() =>
            {
                while (WindowManager.Instance.CurrentScreen == ScreenType.PokerScreen)
                {
                    PokerBoard board = boardReader.ReadBoard();

                    if (board != null && gameBoard != board)
                    {
                        gameBoard = board;
                    }
                }
            }).Start();
        }

        public List<PokerCard> GetHand() => gameBoard.Hand;

        public List<PokerCard> GetDeck() => gameBoard.Deck;
    }
}
