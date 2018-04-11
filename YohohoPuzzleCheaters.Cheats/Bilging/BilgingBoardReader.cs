using System.Drawing;

using YohohoPuzzleCheaters.Cheats.Bilging.Entities;
using YohohoPuzzleCheaters.Common.Windows;

namespace YohohoPuzzleCheaters.Cheats.Bilging
{
    public class BilgingBoardReader
    {
        public const int BoardX = 89;
        public const int BoardY = 46;
        public const int PieceSize = 45;

        public BilgingBoard ReadBoard()
        {
            BilgingBoard board = new BilgingBoard();

            for (int y = 0; y < board.Height; y++)
            {
                for (int x = 0; x < board.Width; x++)
                {
                    board[x, y] = ReadPiece(
                        BoardX + x * PieceSize,
                        BoardY + y * PieceSize);
                }
            }

            return board;
        }

        BilgingPiece ReadPiece(int x, int y)
        {
            Color pixel22x22 = WindowManager.Instance.GetPixel(x + 22, y + 22);

            if (pixel22x22.R == 025 && pixel22x22.G == 136 && pixel22x22.B == 202 ||
                pixel22x22.R == 010 && pixel22x22.G == 099 && pixel22x22.B == 179 ||
                pixel22x22.R == 010 && pixel22x22.G == 099 && pixel22x22.B == 178)
            {
                return BilgingPiece.SquareDark;
            }

            if (pixel22x22.R == 004 && pixel22x22.G == 220 && pixel22x22.B == 204 ||
                pixel22x22.R == 002 && pixel22x22.G == 133 && pixel22x22.B == 180 ||
                pixel22x22.R == 002 && pixel22x22.G == 133 && pixel22x22.B == 179) // YPP:DS through Wine
            {
                return BilgingPiece.SquareLight;
            }

            if (pixel22x22.R == 025 && pixel22x22.G == 200 && pixel22x22.B == 243 ||
                pixel22x22.R == 010 && pixel22x22.G == 125 && pixel22x22.B == 195 ||
                pixel22x22.R == 010 && pixel22x22.G == 125 && pixel22x22.B == 194) // YPP:DS through Wine
            {
                return BilgingPiece.CircleDark;
            }

            if (pixel22x22.R == 136 && pixel22x22.G == 226 && pixel22x22.B == 197 ||
                pixel22x22.R == 054 && pixel22x22.G == 135 && pixel22x22.B == 177 ||
                pixel22x22.R == 054 && pixel22x22.G == 135 && pixel22x22.B == 176) // YPP:DS through Wine
            {
                return BilgingPiece.CircleLight;
            }

            if (pixel22x22.R == 059 && pixel22x22.G == 135 && pixel22x22.B == 150 ||
                pixel22x22.R == 024 && pixel22x22.G == 099 && pixel22x22.B == 158 ||
                pixel22x22.R == 024 && pixel22x22.G == 099 && pixel22x22.B == 157) // YPP:DS through Wine
            {
                return BilgingPiece.OctogonDark;
            }

            if (pixel22x22.R == 087 && pixel22x22.G == 189 && pixel22x22.B == 245 ||
                pixel22x22.R == 035 && pixel22x22.G == 121 && pixel22x22.B == 196 ||
                pixel22x22.R == 035 && pixel22x22.G == 121 && pixel22x22.B == 195) // YPP:DS through Wine
            {
                return BilgingPiece.OctogonLight;
            }

            if (pixel22x22.R == 007 && pixel22x22.G == 122 && pixel22x22.B == 235 ||
                pixel22x22.R == 003 && pixel22x22.G == 094 && pixel22x22.B == 192 ||
                pixel22x22.R == 003 && pixel22x22.G == 094 && pixel22x22.B == 191) // YPP:DS through Wine
            {
                return BilgingPiece.PentagonDark;
            }

            if (pixel22x22.R == 250 && pixel22x22.G == 242 && pixel22x22.B == 068 ||
                pixel22x22.R == 100 && pixel22x22.G == 142 && pixel22x22.B == 125 ||
                pixel22x22.R == 100 && pixel22x22.G == 142 && pixel22x22.B == 124) // YPP:DS through Wine
            {
                return BilgingPiece.Pufferfish;
            }

            if (pixel22x22.R == 000 && pixel22x22.G == 255 && pixel22x22.B == 232 ||
                pixel22x22.R == 000 && pixel22x22.G == 147 && pixel22x22.B == 191 ||
                pixel22x22.R == 000 && pixel22x22.G == 147 && pixel22x22.B == 190) // YPP:DS through Wine
            {
                return BilgingPiece.Jellyfish;
            }

            if (pixel22x22.R == 026 && pixel22x22.G == 071 && pixel22x22.B == 124 ||
                pixel22x22.R == 026 && pixel22x22.G == 071 && pixel22x22.B == 123) // YPP:DS through Wine
            {
                return BilgingPiece.Crab;
            }

            return BilgingPiece.Unknown;
        }
    }
}
