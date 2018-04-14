using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using YohohoPuzzleCheaters.Cheats.Poker.Entities;
using YohohoPuzzleCheaters.Common.Windows;
using YohohoPuzzleCheaters.Infrastructure.Extensions;

namespace YohohoPuzzleCheaters.Cheats.Poker
{
    public class PokerBoardReader
    {
        const int PocketX = 125;
        const int PocketY = 515;

        const int DeckX = 155;
        const int DeckY = 250;

        const int CardsDistance = 20;
        const int NamePixelsToCheck = 10;
        const int NamePixelTolerance = 10;

        public PokerBoard ReadBoard()
        {
            if (WindowManager.Instance.CurrentScreen != ScreenType.PokerScreen)
            {
                return null;
            }

            PokerBoard board = new PokerBoard();
            board.Pocket = ReadPocketCards();
            board.Deck = ReadDeckCards();
            board.PlayersCount = ReadPlayersCount();

            return board;
        }

        public List<PokerCard> ReadPocketCards()
        {
            List<PokerCard> playerCards = new List<PokerCard>();

            for (int i = 0; i < 2; i++)
            {
                int cardX = PocketX + CardsDistance * i;

                Color pixel00x02 = WindowManager.Instance.GetPixel(cardX, PocketY + 02);

                if (pixel00x02.R != 3 || pixel00x02.G != 3 || pixel00x02.B != 3)
                {
                    break;
                }

                playerCards.Add(ReadCard(cardX, PocketY));
            }

            return playerCards;
        }

        public List<PokerCard> ReadDeckCards()
        {
            List<PokerCard> deck = new List<PokerCard>();

            for (int i = 0; i < 5; i++)
            {
                int cardX = DeckX + CardsDistance * i;

                Color pixel00x02 = WindowManager.Instance.GetPixel(cardX, DeckY + 02);

                if (pixel00x02.R != 3 || pixel00x02.G != 3 || pixel00x02.B != 3)
                {
                    break;
                }

                deck.Add(ReadCard(cardX, DeckY));
            }

            return deck;
        }

        public int ReadPlayersCount()
        {
            // Starting from bottom and going clockwise
            int playersCount = 0;

            foreach (Point point in nameTagLocations)
            {
                for (int i = 0; i < NamePixelsToCheck; i++)
                {
                    Color pixel = WindowManager.Instance.GetPixel(point.X + i, point.Y);

                    if (activeTagColours.Any(x => pixel.EqualsWithTolerance(x, NamePixelTolerance)) ||
                        inactiveTagColours.Any(x => pixel.EqualsWithTolerance(x, NamePixelTolerance)))
                    {
                        playersCount += 1;
                        break;
                    }
                }
            }

            return playersCount;
        }

        public PokerCard ReadCard(int cardX, int cardY)
        {
            PokerCardRank rank = ReadCardRank(cardX, cardY);
            PokerCardSuit suit = ReadCardSuit(cardX, cardY);

            return new PokerCard(rank, suit);
        }

        public PokerCardRank ReadCardRank(int cardX, int cardY)
        {
            Color pixel05x04 = WindowManager.Instance.GetPixel(cardX + 05, cardY + 04);
            Color pixel08x06 = WindowManager.Instance.GetPixel(cardX + 08, cardY + 06);

            if ((pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 025 && pixel08x06.G == 025 && pixel08x06.B == 025) ||
                (pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 204 && pixel08x06.G == 060 && pixel08x06.B == 020))
            {
                return PokerCardRank.Ace;
            }

            if ((pixel05x04.R == 088 && pixel05x04.G == 087 && pixel05x04.B == 087 &&
                 pixel08x06.R == 040 && pixel08x06.G == 040 && pixel08x06.B == 040) ||
                (pixel05x04.R == 220 && pixel05x04.G == 119 && pixel05x04.B == 087 &&
                 pixel08x06.R == 212 && pixel08x06.G == 076 && pixel08x06.B == 036))
            {
                return PokerCardRank.King;
            }

            if ((pixel05x04.R == 060 && pixel05x04.G == 060 && pixel05x04.B == 060 &&
                 pixel08x06.R == 252 && pixel08x06.G == 252 && pixel08x06.B == 252) ||
                (pixel05x04.R == 214 && pixel05x04.G == 091 && pixel05x04.B == 053 &&
                 pixel08x06.R == 252 && pixel08x06.G == 252 && pixel08x06.B == 252))
            {
                return PokerCardRank.Queen;
            }

            if ((pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 004 && pixel08x06.G == 004 && pixel08x06.B == 004) ||
                (pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 204 && pixel08x06.G == 052 && pixel08x06.B == 004))
            {
                return PokerCardRank.Jester;
            }

            if ((pixel05x04.R == 004 && pixel05x04.G == 004 && pixel05x04.B == 004 &&
                 pixel08x06.R == 201 && pixel08x06.G == 201 && pixel08x06.B == 201) ||
                (pixel05x04.R == 204 && pixel05x04.G == 052 && pixel05x04.B == 004 &&
                 pixel08x06.R == 244 && pixel08x06.G == 213 && pixel08x06.B == 204))
            {
                return PokerCardRank.Ten;
            }

            if ((pixel05x04.R == 120 && pixel05x04.G == 120 && pixel05x04.B == 119 &&
                 pixel08x06.R == 252 && pixel08x06.G == 252 && pixel08x06.B == 252) ||
                (pixel05x04.R == 230 && pixel05x04.G == 150 && pixel05x04.B == 121 &&
                 pixel08x06.R == 252 && pixel08x06.G == 252 && pixel08x06.B == 252))
            {
                return PokerCardRank.Nine;
            }

            if ((pixel05x04.R == 088 && pixel05x04.G == 087 && pixel05x04.B == 087 &&
                 pixel08x06.R == 252 && pixel08x06.G == 252 && pixel08x06.B == 252) ||
                (pixel05x04.R == 220 && pixel05x04.G == 119 && pixel05x04.B == 087 &&
                 pixel08x06.R == 252 && pixel08x06.G == 252 && pixel08x06.B == 252))
            {
                return PokerCardRank.Eight;
            }

            if ((pixel05x04.R == 004 && pixel05x04.G == 004 && pixel05x04.B == 004 &&
                 pixel08x06.R == 188 && pixel08x06.G == 188 && pixel08x06.B == 188) ||
                (pixel05x04.R == 204 && pixel05x04.G == 052 && pixel05x04.B == 004 &&
                 pixel08x06.R == 242 && pixel08x06.G == 201 && pixel08x06.B == 186))
            {
                return PokerCardRank.Seven;
            }

            if ((pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 236 && pixel08x06.G == 236 && pixel08x06.B == 236) ||
                (pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 252 && pixel08x06.G == 244 && pixel08x06.B == 236))
            {
                return PokerCardRank.Six;
            }

            if ((pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 188 && pixel08x06.G == 188 && pixel08x06.B == 188) ||
                (pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 242 && pixel08x06.G == 201 && pixel08x06.B == 186))
            {
                return PokerCardRank.Five;
            }

            if ((pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 052 && pixel08x06.G == 052 && pixel08x06.B == 052) ||
                (pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 214 && pixel08x06.G == 091 && pixel08x06.B == 053))
            {
                return PokerCardRank.Four;
            }

            if ((pixel05x04.R == 104 && pixel05x04.G == 104 && pixel05x04.B == 104 &&
                 pixel08x06.R == 140 && pixel08x06.G == 140 && pixel08x06.B == 140) ||
                (pixel05x04.R == 228 && pixel05x04.G == 134 && pixel05x04.B == 104 &&
                 pixel08x06.R == 231 && pixel08x06.G == 156 && pixel08x06.B == 132))
            {
                return PokerCardRank.Three;
            }

            if ((pixel05x04.R == 153 && pixel05x04.G == 153 && pixel05x04.B == 153 &&
                 pixel08x06.R == 071 && pixel08x06.G == 071 && pixel08x06.B == 071) ||
                (pixel05x04.R == 236 && pixel05x04.G == 170 && pixel05x04.B == 153 &&
                 pixel08x06.R == 223 && pixel08x06.G == 108 && pixel08x06.B == 069))
            {
                return PokerCardRank.Two;
            }

            return PokerCardRank.Unknown;
        }

        public PokerCardSuit ReadCardSuit(int cardX, int cardY)
        {
            Color pixel06x25 = WindowManager.Instance.GetPixel(cardX + 06, cardY + 25);

            if (pixel06x25.R == 012 && pixel06x25.G == 004 && pixel06x25.B == 012)
            {
                return PokerCardSuit.Spades;
            }

            if (pixel06x25.R == 204 && pixel06x25.G == 044 && pixel06x25.B == 004)
            {
                return PokerCardSuit.Hearts;
            }

            if (pixel06x25.R == 003 && pixel06x25.G == 003 && pixel06x25.B == 003)
            {
                return PokerCardSuit.Clubs;
            }

            if (pixel06x25.R == 204 && pixel06x25.G == 052 && pixel06x25.B == 004)
            {
                return PokerCardSuit.Diamonds;
            }

            return PokerCardSuit.Unknown;
        }

        List<Point> nameTagLocations = new List<Point>
        {
            new Point(202, 417),
            new Point(090, 377),
            new Point(085, 356),
            new Point(041, 277),
            new Point(038, 216),
            new Point(083, 076),
            new Point(102, 055),
            new Point(199, 017),
            new Point(295, 055),
            new Point(354, 155),
            new Point(356, 278),
            new Point(298, 378)
        };
        List<Color> activeTagColours = new List<Color>
        {
            Color.FromArgb(255, 255, 000), // Yellow
            Color.FromArgb(163, 167, 002), // Yellow
            Color.FromArgb(084, 255, 000), // Green
            Color.FromArgb(063, 191, 000)  // Green
        };
        List<Color> inactiveTagColours = new List<Color>
        {
            Color.FromArgb(121, 121, 000),
            Color.FromArgb(192, 196, 001),
            Color.FromArgb(223, 223, 000),
            Color.FromArgb(067, 069, 001)
        };
    }
}
