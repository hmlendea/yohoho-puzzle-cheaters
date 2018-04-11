using System.Collections.Generic;
using System.Drawing;

using YohohoPuzzleCheaters.Cheats.Poker.Entities;
using YohohoPuzzleCheaters.Common.Windows;

namespace YohohoPuzzleCheaters.Cheats.Poker
{
    public class PokerBoardReader
    {
        const int PocketX = 125;
        const int PocketY = 515;

        const int DeckX = 155;
        const int DeckY = 250;

        const int CardsDistance = 20;

        public PokerBoard ReadBoard()
        {
            if (WindowManager.Instance.CurrentScreen != ScreenType.PokerScreen)
            {
                return null;
            }

            PokerBoard board = new PokerBoard();
            board.Pocket = ReadPlayerCards();
            board.Deck = ReadDeckCards();

            return board;
        }

        public List<PokerCard> ReadPlayerCards()
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

        public PokerCard ReadCard(int cardX, int cardY)
        {
            PokerCardNumber cardNumber = ReadCardNumber(cardX, cardY);
            PokerCardColour cardColour = ReadCardColour(cardX, cardY);

            return new PokerCard(cardNumber, cardColour);
        }

        public PokerCardNumber ReadCardNumber(int cardX, int cardY)
        {
            Color pixel05x04 = WindowManager.Instance.GetPixel(cardX + 05, cardY + 04);
            Color pixel08x06 = WindowManager.Instance.GetPixel(cardX + 08, cardY + 06);

            if ((pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 025 && pixel08x06.G == 025 && pixel08x06.B == 025) ||
                (pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 204 && pixel08x06.G == 060 && pixel08x06.B == 020))
            {
                return PokerCardNumber.Ace;
            }

            if ((pixel05x04.R == 088 && pixel05x04.G == 087 && pixel05x04.B == 087 &&
                 pixel08x06.R == 040 && pixel08x06.G == 040 && pixel08x06.B == 040) ||
                (pixel05x04.R == 220 && pixel05x04.G == 119 && pixel05x04.B == 087 &&
                 pixel08x06.R == 212 && pixel08x06.G == 076 && pixel08x06.B == 036))
            {
                return PokerCardNumber.King;
            }

            if ((pixel05x04.R == 060 && pixel05x04.G == 060 && pixel05x04.B == 060 &&
                 pixel08x06.R == 252 && pixel08x06.G == 252 && pixel08x06.B == 252) ||
                (pixel05x04.R == 214 && pixel05x04.G == 091 && pixel05x04.B == 053 &&
                 pixel08x06.R == 252 && pixel08x06.G == 252 && pixel08x06.B == 252))
            {
                return PokerCardNumber.Queen;
            }

            if ((pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 004 && pixel08x06.G == 004 && pixel08x06.B == 004) ||
                (pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 204 && pixel08x06.G == 052 && pixel08x06.B == 004))
            {
                return PokerCardNumber.Jester;
            }

            if ((pixel05x04.R == 004 && pixel05x04.G == 004 && pixel05x04.B == 004 &&
                 pixel08x06.R == 201 && pixel08x06.G == 201 && pixel08x06.B == 201) ||
                (pixel05x04.R == 204 && pixel08x06.G == 052 && pixel08x06.B == 004 &&
                 pixel08x06.R == 244 && pixel08x06.G == 213 && pixel08x06.B == 204))
            {
                return PokerCardNumber.Ten;
            }

            if ((pixel05x04.R == 120 && pixel05x04.G == 120 && pixel05x04.B == 119 &&
                 pixel08x06.R == 252 && pixel08x06.G == 252 && pixel08x06.B == 252) ||
                (pixel05x04.R == 230 && pixel08x06.G == 150 && pixel08x06.B == 121 &&
                 pixel08x06.R == 252 && pixel08x06.G == 252 && pixel08x06.B == 252))
            {
                return PokerCardNumber.Nine;
            }

            if ((pixel05x04.R == 088 && pixel05x04.G == 087 && pixel05x04.B == 087 &&
                 pixel08x06.R == 252 && pixel08x06.G == 252 && pixel08x06.B == 252) ||
                (pixel05x04.R == 220 && pixel08x06.G == 119 && pixel08x06.B == 087 &&
                 pixel08x06.R == 252 && pixel08x06.G == 252 && pixel08x06.B == 252))
            {
                return PokerCardNumber.Eight;
            }

            if ((pixel05x04.R == 004 && pixel05x04.G == 004 && pixel05x04.B == 004 &&
                 pixel08x06.R == 188 && pixel08x06.G == 188 && pixel08x06.B == 188) ||
                (pixel05x04.R == 204 && pixel08x06.G == 052 && pixel08x06.B == 004 &&
                 pixel08x06.R == 242 && pixel08x06.G == 201 && pixel08x06.B == 186))
            {
                return PokerCardNumber.Seven;
            }

            if ((pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 236 && pixel08x06.G == 236 && pixel08x06.B == 236) ||
                (pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 252 && pixel08x06.G == 244 && pixel08x06.B == 236))
            {
                return PokerCardNumber.Six;
            }

            if ((pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 188 && pixel08x06.G == 188 && pixel08x06.B == 188) ||
                (pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 242 && pixel08x06.G == 201 && pixel08x06.B == 186))
            {
                return PokerCardNumber.Five;
            }

            if ((pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 052 && pixel08x06.G == 052 && pixel08x06.B == 052) ||
                (pixel05x04.R == 252 && pixel05x04.G == 252 && pixel05x04.B == 252 &&
                 pixel08x06.R == 214 && pixel08x06.G == 091 && pixel08x06.B == 053))
            {
                return PokerCardNumber.Four;
            }

            if ((pixel05x04.R == 104 && pixel05x04.G == 104 && pixel05x04.B == 104 &&
                 pixel08x06.R == 140 && pixel08x06.G == 140 && pixel08x06.B == 140) ||
                (pixel05x04.R == 228 && pixel05x04.G == 134 && pixel05x04.B == 104 &&
                 pixel08x06.R == 231 && pixel08x06.G == 156 && pixel08x06.B == 132))
            {
                return PokerCardNumber.Three;
            }

            if ((pixel05x04.R == 153 && pixel05x04.G == 153 && pixel05x04.B == 153 &&
                 pixel08x06.R == 071 && pixel08x06.G == 071 && pixel08x06.B == 071) ||
                (pixel05x04.R == 236 && pixel05x04.G == 170 && pixel05x04.B == 153 &&
                 pixel08x06.R == 223 && pixel08x06.G == 108 && pixel08x06.B == 069))
            {
                return PokerCardNumber.Two;
            }

            return PokerCardNumber.Unknown;
        }

        public PokerCardColour ReadCardColour(int cardX, int cardY)
        {
            Color pixel06x25 = WindowManager.Instance.GetPixel(cardX + 06, cardY + 25);

            if (pixel06x25.R == 012 && pixel06x25.G == 004 && pixel06x25.B == 012)
            {
                return PokerCardColour.Spades;
            }

            if (pixel06x25.R == 204 && pixel06x25.G == 044 && pixel06x25.B == 004)
            {
                return PokerCardColour.Hearts;
            }

            if (pixel06x25.R == 003 && pixel06x25.G == 003 && pixel06x25.B == 003)
            {
                return PokerCardColour.Clubs;
            }

            if (pixel06x25.R == 204 && pixel06x25.G == 052 && pixel06x25.B == 004)
            {
                return PokerCardColour.Diamonds;
            }

            return PokerCardColour.Unknown;
        }
    }
}
