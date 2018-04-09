﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using NuciXNA.Primitives;

using YohohoPuzzleCheaters.Common.Windows;
using YohohoPuzzleCheaters.Models;

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
                    else if (pixel22x22.R == 250 && pixel22x22.G == 242 && pixel22x22.B == 068 ||
                             pixel22x22.R == 100 && pixel22x22.G == 142 && pixel22x22.B == 125)
                    {
                        table[x, y] = (int)BilgingPiece.BlowFish;
                    }
                    else
                    {
                        table[x, y] = (int)BilgingPiece.Unknown;
                    }
                }
            }
        }

        public int GetPiece(int x, int y) => table[x, y];

        public bool ContainsUnknownPieces()
        {
            for (int y = 0; y < TableRows; y++)
            {
                for (int x = 0; x < TableColumns; x++)
                {
                    if (table[x, y] == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public BilgingResult GetBestTarget()
        {
            foreach (int[][] pattern in patterns)
            {
                BilgingResult result = GetTargetForPattern(pattern);

                if (result.IsSuccessful)
                {
                    return result;
                }
            }

            return CreateNonSuccessResult();
        }

        BilgingResult GetTargetForPattern(int[][] pattern)
        {
            int patW = pattern[0].Length;
            int patH = pattern.Length;

            for (int y = 0; y < TableRows - patH; y++)
            {
                for (int x = 0; x < TableColumns - patW; x++)
                {
                    int target1X = -1;
                    int target1Y = -1;
                    int target2X = -1;
                    int target2Y = -1;

                    List<int> pieces = new List<int>();

                    for (int patY = 0; patY < patH; patY++)
                    {
                        for (int patX = 0; patX < patW; patX++)
                        {
                            if (pattern[patY][patX] == 1 || pattern[patY][patX] == 3)
                            {
                                pieces.Add(table[x + patX, y + patY]);
                            }

                            if (pattern[patY][patX] == 2)
                            {
                                target1X = x + patX;
                                target1Y = y + patY;
                            }
                            else if (pattern[patY][patX] == 3)
                            {
                                target2X = x + patX;
                                target2Y = y + patY;
                            }
                        }
                    }

                    if (pieces.Contains(0) || pieces.Distinct().Count() == 1)
                    {
                        BilgingResult result = new BilgingResult();

                        Point2D target1 = new Point2D(target1X, target1Y);
                        Point2D target2 = new Point2D(target2X, target2Y);

                        if (target1X < target2X)
                        {
                            result.Selection1 = target1;
                            result.Selection2 = target2;
                        }
                        else
                        {
                            result.Selection1 = target2;
                            result.Selection2 = target1;
                        }

                        return result;
                    }
                }
            }

            return CreateNonSuccessResult();
        }

        BilgingResult CreateNonSuccessResult()
        {
            return new BilgingResult
            {
                Selection1 = new Point2D(-1, -1),
                Selection2 = new Point2D(-1, -1)
            };
        }

        // TODO: Refactor this. It literally gives me nightmares
        // 0 can be anything
        // 1 must be the same as other 1s
        // selection has value + 2
        List<int[][]> patterns = new List<int[][]>
        {
            new int[][] {
                new int[] { 0, 1, 0, 0 },
                new int[] { 0, 1, 0, 0 },
                new int[] { 3, 2, 1, 1 },
                new int[] { 0, 1, 0, 0 },
                new int[] { 0, 1, 0, 0 } },
            new int[][] {
                new int[] { 0, 0, 1, 0 },
                new int[] { 0, 0, 1, 0 },
                new int[] { 1, 1, 2, 3 },
                new int[] { 0, 0, 1, 0 },
                new int[] { 0, 0, 1, 0 } },
            new int[][] {
                new int[] { 0, 1, 0, 0 },
                new int[] { 3, 2, 1, 1 },
                new int[] { 0, 1, 0, 0 },
                new int[] { 0, 1, 0, 0 } },
            new int[][] {
                new int[] { 0, 1, 0, 0 },
                new int[] { 0, 1, 0, 0 },
                new int[] { 3, 2, 1, 1 },
                new int[] { 0, 1, 0, 0 } },
            new int[][] {
                new int[] { 0, 0, 1, 0 },
                new int[] { 1, 1, 2, 3 },
                new int[] { 0, 0, 1, 0 },
                new int[] { 0, 0, 1, 0 } },
            new int[][] {
                new int[] { 0, 0, 1, 0 },
                new int[] { 0, 0, 1, 0 },
                new int[] { 1, 1, 2, 3 },
                new int[] { 0, 0, 1, 0 } },
            new int[][] {
                new int[] { 0, 1 },
                new int[] { 3, 2 },
                new int[] { 0, 1 },
                new int[] { 0, 1 } },
            new int[][] {
                new int[] { 0, 1 },
                new int[] { 0, 1 },
                new int[] { 3, 2 },
                new int[] { 0, 1 } },
            new int[][] {
                new int[] { 1, 0 },
                new int[] { 2, 3 },
                new int[] { 1, 0 },
                new int[] { 1, 0 } },
            new int[][] {
                new int[] { 1, 0 },
                new int[] { 1, 0 },
                new int[] { 2, 3 },
                new int[] { 1, 0 } },
            new int[][] {
                new int[] { 3, 2 },
                new int[] { 0, 1 },
                new int[] { 0, 1 } },
            new int[][] {
                new int[] { 0, 1 },
                new int[] { 3, 2 },
                new int[] { 0, 1 } },
            new int[][] {
                new int[] { 0, 1 },
                new int[] { 0, 1 },
                new int[] { 3, 2 } },
            new int[][] {
                new int[] { 2, 3 },
                new int[] { 1, 0 },
                new int[] { 1, 0 } },
            new int[][] {
                new int[] { 1, 0 },
                new int[] { 2, 3 },
                new int[] { 1, 0 } },
            new int[][] {
                new int[] { 1, 0 },
                new int[] { 1, 0 },
                new int[] { 2, 3 } },
            new int[][] {
                new int[] { 1, 1, 2, 3 } },
            new int[][] {
                new int[] { 3, 2, 1, 1 } }
        };
    }
}
