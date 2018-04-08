using System;
using System.Drawing;

using YohohoPuzzleCheaters.Common.Windows;
using YohohoPuzzleCheaters.Settings;

namespace YohohoPuzzleCheaters.Cheats.Bilging
{
    public class BilgingCheat
    {
        public const int TablePosX = 89;
        public const int TablePosY = 46;
        public const int TableColumns = 6;
        public const int TableRows = 12;
        public const int PieceSize = 45;

        int[,] table;

        public void LoadContent()
        {
            table = new int[TableColumns, TableRows];
        }

        public void UnloadContent()
        {
            table = null;
        }

        public void Update(double elapsedTime)
        {
            for (int y = 0; y < TableRows; y++)
            {
                for (int x = 0; x < TableColumns; x++)
                {
                    int pieceX = TablePosX + x * PieceSize;
                    int pieceY = TablePosY + y * PieceSize;

                    Color pixel22x22 = WindowManager.Instance.GetPixel(pieceX + 22, pieceY + 22);

                    if (pixel22x22.R == 025 && pixel22x22.G == 136 && pixel22x22.B == 202 ||
                        pixel22x22.R == 010 && pixel22x22.G == 099 && pixel22x22.B == 179)
                    {
                        table[x, y] = (int)BilgingPiece.SquareDark;
                    }
                    else if (pixel22x22.R == 004 && pixel22x22.G == 220 && pixel22x22.B == 204 ||
                             pixel22x22.R == 002 && pixel22x22.G == 133 && pixel22x22.B == 180)
                    {
                        table[x, y] = (int)BilgingPiece.SquareLight;
                    }
                    else if (pixel22x22.R == 025 && pixel22x22.G == 200 && pixel22x22.B == 243 ||
                             pixel22x22.R == 010 && pixel22x22.G == 125 && pixel22x22.B == 195)
                    {
                        table[x, y] = (int)BilgingPiece.CircleDark;
                    }
                    else if (pixel22x22.R == 136 && pixel22x22.G == 226 && pixel22x22.B == 197 ||
                             pixel22x22.R == 054 && pixel22x22.G == 135 && pixel22x22.B == 177)
                    {
                        table[x, y] = (int)BilgingPiece.CircleLight;
                    }
                    else if (pixel22x22.R == 059 && pixel22x22.G == 135 && pixel22x22.B == 150 ||
                             pixel22x22.R == 024 && pixel22x22.G == 099 && pixel22x22.B == 158)
                    {
                        table[x, y] = (int)BilgingPiece.OctogonDark;
                    }
                    else if (pixel22x22.R == 087 && pixel22x22.G == 189 && pixel22x22.B == 245 ||
                             pixel22x22.R == 035 && pixel22x22.G == 121 && pixel22x22.B == 196)
                    {
                        table[x, y] = (int)BilgingPiece.OctogonLight;
                    }
                    else
                    {
                        table[x, y] = (int)BilgingPiece.Unknown;
                    }
                }
            }
        }

        public void Draw()
        {
            if (SettingsManager.Instance.DebugMode)
            {
                DrawTable();
            }
        }

        public int GetPiece(int x, int y) => table[x, y];

        public int GetBestTarget() => 10;

        void DrawTable()
        {
            Console.WriteLine("Table: ");

            for (int y = 0; y < TableRows; y++)
            {
                for (int x = 0; x < TableColumns; x++)
                {
                    Console.Write($"{table[x, y]} ");
                }

                Console.WriteLine();
            }
        }
    }
}
