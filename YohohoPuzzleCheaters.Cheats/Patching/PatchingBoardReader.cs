using System.Drawing;

using YohohoPuzzleCheaters.Cheats.Patching.Entities;
using YohohoPuzzleCheaters.Common.Windows;

namespace YohohoPuzzleCheaters.Cheats.Patching
{
    public class PatchingBoardReader
    {
        public const int BoardWidth = 7;
        public const int BoardHeight = 6;

        public const int BoardX = 29;
        public const int BoardY = 69;
        public const int PieceSize = 56;

        public PatchingBoard ReadBoard()
        {
            PatchingBoard board = new PatchingBoard(BoardWidth, BoardHeight);

            for (int y = 0; y < BoardHeight; y++)
            {
                for (int x = 0; x < BoardWidth; x++)
                {
                    board[x, y] = ReadPiece(
                        BoardX + x * PieceSize,
                        BoardY + y * PieceSize);
                }
            }

            return board;
        }

        public PatchingPiece ReadPiece(int screenX, int screenY)
        {
            Color pixel29x11 = WindowManager.Instance.GetPixel(screenX + 29, screenY + 11);
            Color pixel12x27 = WindowManager.Instance.GetPixel(screenX + 12, screenY + 27);
            Color pixel39x27 = WindowManager.Instance.GetPixel(screenX + 39, screenY + 27);
            Color pixel27x42 = WindowManager.Instance.GetPixel(screenX + 27, screenY + 42);

            if (pixel29x11.R == 066 && pixel29x11.G == 142 && pixel29x11.B == 185 &&
                pixel12x27.R == 073 && pixel12x27.G == 146 && pixel12x27.B == 188 &&
                pixel39x27.R == 066 && pixel39x27.G == 142 && pixel39x27.B == 185 &&
                pixel27x42.R == 073 && pixel27x42.G == 146 && pixel27x42.B == 188)
            {
                return PatchingPiece.Blocker;
            }

            if ((pixel29x11.R == 069 && pixel29x11.G == 068 && pixel29x11.B == 061 &&
                 pixel12x27.R == 222 && pixel12x27.G == 213 && pixel12x27.B == 170 &&
                 pixel39x27.R == 058 && pixel39x27.G == 069 && pixel39x27.B == 082 &&
                 pixel27x42.R == 049 && pixel27x42.G == 074 && pixel27x42.B == 105) ||
                (pixel29x11.R == 249 && pixel29x11.G == 217 && pixel29x11.B == 060 &&
                 pixel12x27.R == 243 && pixel12x27.G == 195 && pixel12x27.B == 050 &&
                 pixel39x27.R == 254 && pixel39x27.G == 243 && pixel39x27.B == 169 &&
                 pixel27x42.R == 254 && pixel27x42.G == 248 && pixel27x42.B == 182))
            {
                return PatchingPiece.TeeRight;
            }

            if ((pixel29x11.R == 221 && pixel29x11.G == 210 && pixel29x11.B == 166 &&
                 pixel12x27.R == 049 && pixel12x27.G == 074 && pixel12x27.B == 105 &&
                 pixel39x27.R == 049 && pixel39x27.G == 074 && pixel39x27.B == 105 &&
                 pixel27x42.R == 218 && pixel27x42.G == 205 && pixel27x42.B == 160) ||
                (pixel29x11.R == 243 && pixel29x11.G == 188 && pixel29x11.B == 057 &&
                 pixel12x27.R == 251 && pixel12x27.G == 237 && pixel12x27.B == 150 &&
                 pixel39x27.R == 252 && pixel39x27.G == 231 && pixel39x27.B == 117 &&
                 pixel27x42.R == 253 && pixel27x42.G == 201 && pixel27x42.B == 069))
            {
                return PatchingPiece.StraightHorizontal;
            }

            if ((pixel29x11.R == 058 && pixel29x11.G == 069 && pixel29x11.B == 082 &&
                 pixel12x27.R == 221 && pixel12x27.G == 212 && pixel12x27.B == 162 &&
                 pixel39x27.R == 208 && pixel39x27.G == 199 && pixel39x27.B == 152 &&
                 pixel27x42.R == 049 && pixel27x42.G == 074 && pixel27x42.B == 105) ||
                (pixel29x11.R == 249 && pixel29x11.G == 217 && pixel29x11.B == 060 &&
                 pixel12x27.R == 243 && pixel12x27.G == 195 && pixel12x27.B == 050 &&
                 pixel39x27.R == 243 && pixel39x27.G == 204 && pixel39x27.B == 116 &&
                 pixel27x42.R == 254 && pixel27x42.G == 248 && pixel27x42.B == 182))
            {
                return PatchingPiece.StraightVertical;
            }

            if ((pixel29x11.R == 147 && pixel29x11.G == 147 && pixel29x11.B == 110 &&
                 pixel12x27.R == 069 && pixel12x27.G == 068 && pixel12x27.B == 061 &&
                 pixel39x27.R == 225 && pixel39x27.G == 215 && pixel39x27.B == 172 &&
                 pixel27x42.R == 220 && pixel27x42.G == 208 && pixel27x42.B == 161) ||
                (pixel29x11.R == 249 && pixel29x11.G == 217 && pixel29x11.B == 060 &&
                 pixel12x27.R == 254 && pixel12x27.G == 248 && pixel12x27.B == 182 &&
                 pixel39x27.R == 243 && pixel39x27.G == 204 && pixel39x27.B == 116 &&
                 pixel27x42.R == 243 && pixel27x42.G == 193 && pixel27x42.B == 063))
            {
                return PatchingPiece.ElbowLeftUp;
            }

            if ((pixel29x11.R == 147 && pixel29x11.G == 147 && pixel29x11.B == 110 &&
                 pixel12x27.R == 222 && pixel12x27.G == 213 && pixel12x27.B == 170 &&
                 pixel39x27.R == 069 && pixel39x27.G == 068 && pixel39x27.B == 061 &&
                 pixel27x42.R == 218 && pixel27x42.G == 205 && pixel27x42.B == 160) ||
                (pixel29x11.R == 249 && pixel29x11.G == 217 && pixel29x11.B == 060 &&
                 pixel12x27.R == 243 && pixel12x27.G == 195 && pixel12x27.B == 050 &&
                 pixel39x27.R == 254 && pixel39x27.G == 243 && pixel39x27.B == 169 &&
                 pixel27x42.R == 243 && pixel27x42.G == 193 && pixel27x42.B == 063))
            {
                return PatchingPiece.ElbowRightUp;
            }

            if ((pixel29x11.R == 225 && pixel29x11.G == 215 && pixel29x11.B == 172 &&
                 pixel12x27.R == 217 && pixel12x27.G == 206 && pixel12x27.B == 154 &&
                 pixel39x27.R == 049 && pixel39x27.G == 074 && pixel39x27.B == 105 &&
                 pixel27x42.R == 049 && pixel27x42.G == 074 && pixel27x42.B == 105) ||
                (pixel29x11.R == 243 && pixel29x11.G == 188 && pixel29x11.B == 057 &&
                 pixel12x27.R == 243 && pixel12x27.G == 195 && pixel12x27.B == 050 &&
                 pixel39x27.R == 254 && pixel39x27.G == 243 && pixel39x27.B == 169 &&
                 pixel27x42.R == 254 && pixel27x42.G == 248 && pixel27x42.B == 182))
            {
                return PatchingPiece.ElbowLeftDown;
            }

            if ((pixel29x11.R == 218 && pixel29x11.G == 205 && pixel29x11.B == 160 &&
                 pixel12x27.R == 058 && pixel12x27.G == 069 && pixel12x27.B == 082 &&
                 pixel39x27.R == 213 && pixel39x27.G == 207 && pixel39x27.B == 160 &&
                 pixel27x42.R == 049 && pixel27x42.G == 074 && pixel27x42.B == 105) ||
                (pixel29x11.R == 243 && pixel29x11.G == 188 && pixel29x11.B == 057 &&
                 pixel12x27.R == 254 && pixel12x27.G == 248 && pixel12x27.B == 182 &&
                 pixel39x27.R == 243 && pixel39x27.G == 204 && pixel39x27.B == 116 &&
                 pixel27x42.R == 254 && pixel27x42.G == 248 && pixel27x42.B == 182))
            {
                return PatchingPiece.ElbowRightDown;
            }

            if ((pixel29x11.R == 128 && pixel29x11.G == 114 && pixel29x11.B == 087 &&
                 pixel12x27.R == 058 && pixel12x27.G == 069 && pixel12x27.B == 082 &&
                 pixel39x27.R == 188 && pixel39x27.G == 187 && pixel39x27.B == 142 &&
                 pixel27x42.R == 049 && pixel27x42.G == 074 && pixel27x42.B == 105) ||
                (pixel29x11.R == 249 && pixel29x11.G == 217 && pixel29x11.B == 060 &&
                 pixel12x27.R == 254 && pixel12x27.G == 248 && pixel12x27.B == 182 &&
                 pixel39x27.R == 243 && pixel39x27.G == 204 && pixel39x27.B == 116 &&
                 pixel27x42.R == 254 && pixel27x42.G == 248 && pixel27x42.B == 182))
            {
                return PatchingPiece.TeeLeft;
            }

            if ((pixel29x11.R == 097 && pixel29x11.G == 097 && pixel29x11.B == 080 &&
                 pixel12x27.R == 049 && pixel12x27.G == 074 && pixel12x27.B == 105 &&
                 pixel39x27.R == 058 && pixel39x27.G == 069 && pixel39x27.B == 082 &&
                 pixel27x42.R == 202 && pixel27x42.G == 198 && pixel27x42.B == 152) ||
                (pixel29x11.R == 249 && pixel29x11.G == 217 && pixel29x11.B == 060 &&
                 pixel12x27.R == 254 && pixel12x27.G == 248 && pixel12x27.B == 182 &&
                 pixel39x27.R == 254 && pixel39x27.G == 243 && pixel39x27.B == 169 &&
                 pixel27x42.R == 249 && pixel27x42.G == 198 && pixel27x42.B == 072))
            {
                return PatchingPiece.TeeUp;
            }

            if ((pixel29x11.R == 069 && pixel29x11.G == 068 && pixel29x11.B == 061 &&
                 pixel12x27.R == 222 && pixel12x27.G == 213 && pixel12x27.B == 170 &&
                 pixel39x27.R == 058 && pixel39x27.G == 069 && pixel39x27.B == 082 &&
                 pixel27x42.R == 049 && pixel27x42.G == 074 && pixel27x42.B == 105) ||
                (pixel29x11.R == 249 && pixel29x11.G == 217 && pixel29x11.B == 060 &&
                 pixel12x27.R == 243 && pixel12x27.G == 195 && pixel12x27.B == 050 &&
                 pixel39x27.R == 254 && pixel39x27.G == 243 && pixel39x27.B == 169 &&
                 pixel27x42.R == 254 && pixel27x42.G == 248 && pixel27x42.B == 182))
            {
                return PatchingPiece.TeeRight;
            }

            if ((pixel29x11.R == 220 && pixel29x11.G == 208 && pixel29x11.B == 161 &&
                 pixel12x27.R == 049 && pixel12x27.G == 074 && pixel12x27.B == 105 &&
                 pixel39x27.R == 049 && pixel39x27.G == 074 && pixel39x27.B == 105 &&
                 pixel27x42.R == 049 && pixel27x42.G == 074 && pixel27x42.B == 105) ||
                (pixel29x11.R == 243 && pixel29x11.G == 188 && pixel29x11.B == 057 &&
                 pixel12x27.R == 254 && pixel12x27.G == 248 && pixel12x27.B == 182 &&
                 pixel39x27.R == 254 && pixel39x27.G == 243 && pixel39x27.B == 169 &&
                 pixel27x42.R == 254 && pixel27x42.G == 248 && pixel27x42.B == 182))
            {
                return PatchingPiece.TeeDown;
            }

            if ((pixel29x11.R == 120 && pixel29x11.G == 114 && pixel29x11.B == 098 &&
                 pixel12x27.R == 173 && pixel12x27.G == 082 && pixel12x27.B == 051 &&
                 pixel39x27.R == 075 && pixel39x27.G == 070 && pixel39x27.B == 062 &&
                 pixel27x42.R == 050 && pixel27x42.G == 047 && pixel27x42.B == 044) ||
                (pixel29x11.R == 112 && pixel29x11.G == 112 && pixel29x11.B == 109 &&
                 pixel12x27.R == 070 && pixel12x27.G == 060 && pixel12x27.B == 035 &&
                 pixel39x27.R == 172 && pixel39x27.G == 118 && pixel39x27.B == 017 &&
                 pixel27x42.R == 039 && pixel27x42.G == 039 && pixel27x42.B == 037))
            {
                return PatchingPiece.GrommetLeft;
            }

            if ((pixel29x11.R == 173 && pixel29x11.G == 082 && pixel29x11.B == 051 &&
                 pixel12x27.R == 106 && pixel12x27.G == 100 && pixel12x27.B == 087 &&
                 pixel39x27.R == 075 && pixel39x27.G == 070 && pixel39x27.B == 062 &&
                 pixel27x42.R == 050 && pixel27x42.G == 047 && pixel27x42.B == 044) ||
                (pixel29x11.R == 112 && pixel29x11.G == 112 && pixel29x11.B == 109 &&
                 pixel12x27.R == 070 && pixel12x27.G == 060 && pixel12x27.B == 035 &&
                 pixel39x27.R == 172 && pixel39x27.G == 118 && pixel39x27.B == 017 &&
                 pixel27x42.R == 039 && pixel27x42.G == 039 && pixel27x42.B == 037))
            {
                return PatchingPiece.GrommetUp;
            }

            if ((pixel29x11.R == 120 && pixel29x11.G == 114 && pixel29x11.B == 098 &&
                 pixel12x27.R == 106 && pixel12x27.G == 100 && pixel12x27.B == 087 &&
                 pixel39x27.R == 119 && pixel39x27.G == 038 && pixel39x27.B == 019 &&
                 pixel27x42.R == 050 && pixel27x42.G == 047 && pixel27x42.B == 044) ||
                (pixel29x11.R == 112 && pixel29x11.G == 112 && pixel29x11.B == 109 &&
                 pixel12x27.R == 067 && pixel12x27.G == 063 && pixel12x27.B == 054 &&
                 pixel39x27.R == 118 && pixel39x27.G == 103 && pixel39x27.B == 054 &&
                 pixel27x42.R == 049 && pixel27x42.G == 046 && pixel27x42.B == 038))
            {
                return PatchingPiece.GrommetRight;
            }

            if ((pixel29x11.R == 120 && pixel29x11.G == 114 && pixel29x11.B == 098 &&
                 pixel12x27.R == 106 && pixel12x27.G == 100 && pixel12x27.B == 087 &&
                 pixel39x27.R == 075 && pixel39x27.G == 070 && pixel39x27.B == 062 &&
                 pixel27x42.R == 097 && pixel27x42.G == 027 && pixel27x42.B == 013) ||
                (pixel29x11.R == 112 && pixel29x11.G == 112 && pixel29x11.B == 109 &&
                 pixel12x27.R == 093 && pixel12x27.G == 091 && pixel12x27.B == 083 &&
                 pixel39x27.R == 164 && pixel39x27.G == 101 && pixel39x27.B == 012 &&
                 pixel27x42.R == 059 && pixel27x42.G == 052 && pixel27x42.B == 036))
            {
                return PatchingPiece.GrommetDown;
            }

            if (pixel12x27.R == 175 && pixel12x27.G == 064 && pixel12x27.B == 029)
            {
                return PatchingPiece.TieOffLeft;
            }

            if (pixel29x11.R == 181 && pixel29x11.G == 071 && pixel29x11.B == 041)
            {
                return PatchingPiece.TieOffUp;
            }

            if (pixel39x27.R == 118 && pixel39x27.G == 044 && pixel39x27.B == 026)
            {
                return PatchingPiece.TieOffRight;
            }

            if (pixel27x42.R == 132 && pixel27x42.G == 049 && pixel27x42.B == 029)
            {
                return PatchingPiece.TieOffDown;
            }

            if (pixel29x11.R == 040 && pixel29x11.G == 035 && pixel29x11.B == 025 &&
                pixel12x27.R == 223 && pixel12x27.G == 169 && pixel12x27.B == 022 &&
                pixel39x27.R == 097 && pixel39x27.G == 107 && pixel39x27.B == 083 &&
                pixel27x42.R == 194 && pixel27x42.G == 136 && pixel27x42.B == 088)
            {
                return PatchingPiece.SpoolLeft;
            }

            if (pixel29x11.R == 230 && pixel29x11.G == 188 && pixel29x11.B == 056 &&
                pixel12x27.R == 206 && pixel12x27.G == 137 && pixel12x27.B == 081 &&
                pixel39x27.R == 250 && pixel39x27.G == 231 && pixel39x27.B == 102 &&
                pixel27x42.R == 119 && pixel27x42.G == 107 && pixel27x42.B == 064)
            {
                return PatchingPiece.SpoolUp;
            }

            if (pixel29x11.R == 196 && pixel29x11.G == 128 && pixel29x11.B == 079 &&
                pixel12x27.R == 230 && pixel12x27.G == 175 && pixel12x27.B == 048 &&
                pixel39x27.R == 200 && pixel39x27.G == 137 && pixel39x27.B == 005 &&
                pixel27x42.R == 089 && pixel27x42.G == 057 && pixel27x42.B == 042)
            {
                return PatchingPiece.SpoolRight;
            }

            if (pixel29x11.R == 144 && pixel29x11.G == 109 && pixel29x11.B == 020 &&
                pixel12x27.R == 040 && pixel12x27.G == 035 && pixel12x27.B == 025 &&
                pixel39x27.R == 001 && pixel39x27.G == 001 && pixel39x27.B == 007 &&
                pixel27x42.R == 105 && pixel27x42.G == 093 && pixel27x42.B == 058)
            {
                return PatchingPiece.SpoolDown;
            }

            return PatchingPiece.Unknown;
        }
    }
}
