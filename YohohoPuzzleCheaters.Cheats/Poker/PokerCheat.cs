using System.Collections.Generic;
using System.Linq;
using System.Threading;

using HoldemHand;

using YohohoPuzzleCheaters.Cheats.Poker.Entities;
using YohohoPuzzleCheaters.Common.Windows;

namespace YohohoPuzzleCheaters.Cheats.Poker
{
    public class PokerCheat
    {
        readonly PokerBoardReader boardReader;

        PokerBoard gameBoard;
        PokerOdds playerOdds;
        PokerOdds opponentOdds;

        public PokerCheat()
        {
            boardReader = new PokerBoardReader();

            gameBoard = new PokerBoard();
            playerOdds = new PokerOdds();
            opponentOdds = new PokerOdds();
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

                        //ClearOdds();
                        CalculateOdds();
                    }
                }
            }).Start();
        }

        public List<PokerCard> GetPocket() => gameBoard.Pocket;

        public List<PokerCard> GetDeck() => gameBoard.Deck;

        public PokerOdds GetPlayerOdds() => playerOdds;

        public PokerOdds GetOpponentOdds() => opponentOdds;

        void ClearOdds()
        {
            playerOdds = new PokerOdds();
            opponentOdds = new PokerOdds();
        }

        void CalculateOdds()
        {
            int count = 0;
            double[] player = new double[9];
            double[] opponent = new double[9];

            string pocket = string.Join(" ", GetPocket().Select(x => x.Code));
            string deck = string.Join(" ", GetDeck().Select(x => x.Code));
            string hand = $"{pocket} {deck}";

            if (string.IsNullOrWhiteSpace(pocket))
            {
                return;
            }

            if (!Hand.ValidateHand(hand))
            {
                return;
            }

            Hand.ParseHand(hand, ref count);

            // Don't allow these configurations because of calculation time.
            if (count == 0 || count == 1 || count == 3 || count == 4 || count > 7)
            {
                return;
            }

            Hand.HandPlayerOpponentOdds(pocket, deck, ref player, ref opponent);

            for (int i = 0; i < 9; i++)
            {
                switch ((Hand.HandTypes)i)
                {
                    case Hand.HandTypes.HighCard:
                        playerOdds.HighCard = player[i];
                        opponentOdds.HighCard = opponent[i];
                        break;

                    case Hand.HandTypes.Pair:
                        playerOdds.OnePair = player[i];
                        opponentOdds.OnePair = opponent[i];
                        break;

                    case Hand.HandTypes.TwoPair:
                        playerOdds.TwoPair = player[i];
                        opponentOdds.TwoPair = opponent[i];
                        break;

                    case Hand.HandTypes.Trips:
                        playerOdds.ThreeOfKind = player[i];
                        opponentOdds.ThreeOfKind = opponent[i];
                        break;

                    case Hand.HandTypes.Straight:
                        playerOdds.Straight = player[i];
                        opponentOdds.Straight = opponent[i];
                        break;

                    case Hand.HandTypes.Flush:
                        playerOdds.Flush = player[i];
                        opponentOdds.Flush = opponent[i];
                        break;

                    case Hand.HandTypes.FullHouse:
                        playerOdds.FullHouse = player[i];
                        opponentOdds.FullHouse = opponent[i];
                        break;

                    case Hand.HandTypes.FourOfAKind:
                        playerOdds.FourOfKind = player[i];
                        opponentOdds.FourOfKind = opponent[i];
                        break;

                    case Hand.HandTypes.StraightFlush:
                        playerOdds.StraightFlush = player[i];
                        opponentOdds.StraightFlush = opponent[i];
                        break;
                }
            }
        }
    }
}
