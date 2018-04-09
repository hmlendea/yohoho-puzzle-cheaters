using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

using YohohoPuzzleCheaters.Cheats.Bilging.Entities;
using YohohoPuzzleCheaters.Common.Windows;

namespace YohohoPuzzleCheaters.Cheats.Bilging
{
    public class BilgingCheat
    {
        public const int TablePosX = 89;
        public const int TablePosY = 46;
        public const int TableColumns = 6;
        public const int TableRows = 12;
        public const int PieceSize = 45;

        BilgingBoard gameBoard;
        BilgingMove bestMove;

        public BilgingCheat()
        {
            gameBoard = new BilgingBoard();
            gameBoard.WaterLevel = 3;
        }

        public void Start()
        {
            new Thread(() =>
            {
                while (WindowManager.Instance.CurrentScreen == ScreenType.BilgingScreen)
                {
                    BilgingBoard board = RetrieveBoard();

                    if (gameBoard != board)
                    {
                        gameBoard = board;
                        CalculateMove();
                    }
                }
            }).Start();
        }

        public BilgingPiece GetPiece(int x, int y) => gameBoard[x, y];

        public bool IsBoardComplete() => !gameBoard.ContainsUnknownPieces && gameBoard.EmptyPiecesCount == 0;

        public BilgingMove GetBestTarget()
        {
            if (bestMove == null)
            {
                return BilgingMove.InvalidMove;
            }

            return bestMove;
        }

        /// <summary>
        /// Shifts all empty cells to the bottom of the board
        /// </summary>
        bool Shift(BilgingBoard board)
        {
            if (board.EmptyPiecesCount == 0)
            {
                return false;
            }

            bool hasShifted = false;

            for (int j = 0; j < BilgingBoard.BoardWidth; j++)
            {
                // find the nearest (vertically) empty cell
                int nearestEmpty = 0;
                while (nearestEmpty < BilgingBoard.BoardHeight &&
                       board[nearestEmpty * BilgingBoard.BoardWidth + j].Type != BilgingPieceType.Empty)
                {
                    nearestEmpty += 1;
                }

                // find the nearest (vertically) non-empty cell after the empty cell
                int nearestNonEmpty = nearestEmpty + 1;
                while (nearestNonEmpty < BilgingBoard.BoardHeight &&
                       board[nearestNonEmpty * BilgingBoard.BoardWidth + j].Type == BilgingPieceType.Empty)
                {
                    nearestNonEmpty += 1;
                }

                // swap all empty cells with nearest cells
                while (nearestNonEmpty < BilgingBoard.BoardHeight)
                {
                    hasShifted |= true;
                    Swap(ref board, nearestEmpty * BilgingBoard.BoardWidth + j, nearestNonEmpty * BilgingBoard.BoardWidth + j);

                    // incrementing both nearestEmpty and nearestNonEmpty,
                    // note that nearestEmpty will always be empty by default
                    nearestEmpty += 1;
                    nearestNonEmpty += 1;

                    // refind nearest non-empty cell
                    while (nearestNonEmpty < BilgingBoard.BoardHeight &&
                           board[nearestNonEmpty * BilgingBoard.BoardWidth + j].Type == BilgingPieceType.Empty)
                    {
                        nearestNonEmpty += 1;
                    }
                }
            }

            return hasShifted;
        }

        int ClearCrabs(BilgingBoard board)
        {
            if (gameBoard.CrabsCount == 0)
            {
                return 0;
            }

            // note: this shift happens twice initially
            int crabsReleased = 0;

            for (int i = 0; i < BilgingBoard.BoardHeight - board.WaterLevel; i++)
            {
                for (int j = 0; j < BilgingBoard.BoardWidth; j++)
                {
                    int index = i * BilgingBoard.BoardWidth + j;

                    if (board[index].Type == BilgingPieceType.Crab)
                    {
                        crabsReleased += 1;
                        board[index] = BilgingPiece.Empty;
                    }
                }
            }

            if (Shift(board))
            {
                crabsReleased += ClearCrabs(board);
            }

            return crabsReleased;
        }

        int ClearAll(ref BilgingBoard board, int previousClears)
        {
            BilgingBoard nextGeneration = board.CreateCopy();
            int clears = 0;

            for (int i = 0; i < BilgingBoard.BoardHeight; i++)
            {
                for (int j = 0; j < BilgingBoard.BoardWidth; j++)
                {
                    int index = i * BilgingBoard.BoardWidth + j;

                    BilgingPiece piece = board[index];

                    if (piece.Type != BilgingPieceType.Movable)
                    {
                        continue;
                    }

                    // horizontal right clears
                    if (j < BilgingBoard.BoardWidth - 2 &&
                        piece.Id == board[index + 1].Id &&
                        piece.Id == board[index + 2].Id)
                    {
                        nextGeneration[index] = BilgingPiece.Empty;
                        nextGeneration[index + 1] = BilgingPiece.Empty;
                        nextGeneration[index + 2] = BilgingPiece.Empty;
                    }

                    // horizontal left clears
                    if (j > 1 &&
                        piece.Id == board[index - 1].Id &&
                        piece.Id == board[index - 2].Id)
                    {
                        nextGeneration[index] = BilgingPiece.Empty;
                        nextGeneration[index - 1] = BilgingPiece.Empty;
                        nextGeneration[index - 2] = BilgingPiece.Empty;
                    }

                    // vertial below clears
                    if (i < BilgingBoard.BoardHeight - 2 &&
                        piece.Id == board[index + BilgingBoard.BoardWidth].Id &&
                        piece.Id == board[index + BilgingBoard.BoardWidth * 2].Id)
                    {
                        nextGeneration[index] = BilgingPiece.Empty;
                        nextGeneration[index + BilgingBoard.BoardWidth] = BilgingPiece.Empty;
                        nextGeneration[index + BilgingBoard.BoardWidth * 2] = BilgingPiece.Empty;
                    }

                    // vertial above clears
                    if (i > 1 &&
                        piece.Id == board[index - BilgingBoard.BoardWidth].Id &&
                        piece.Id == board[index - BilgingBoard.BoardWidth * 2].Id)
                    {
                        nextGeneration[index] = BilgingPiece.Empty;
                        nextGeneration[index - BilgingBoard.BoardWidth] = BilgingPiece.Empty;
                        nextGeneration[index - BilgingBoard.BoardWidth * 2] = BilgingPiece.Empty;
                    }
                }
            }

            if (Shift(nextGeneration))
            {
                int crabsScore = ClearCrabs(nextGeneration) * 2;
                crabsScore *= crabsScore;
                clears += crabsScore;

                int totalClears = nextGeneration.EmptyPiecesCount;
                int currentClears = totalClears - previousClears;

                clears += currentClears * currentClears;

                // recursive case, if board was shifted -> check for combos again. note: ignoring crab combos
                ClearAll(ref nextGeneration, totalClears);
            }

            board = nextGeneration;

            return clears;
        }

        void PerformPufferfish(ref BilgingBoard board, int y, int x)
        {
            int iBegin = Math.Max(y - 1, 0);
            int iEnd = Math.Min(y + 1, BilgingBoard.BoardHeight - 1);

            int jBegin = Math.Max(x - 1, 0);
            int jEnd = Math.Min(x + 1, BilgingBoard.BoardWidth - 1);

            for (int i = iBegin; i <= iEnd; i++)
            {
                for (int j = jBegin; j <= jEnd; j++)
                {
                    board[i * BilgingBoard.BoardWidth + j] = BilgingPiece.Empty;
                }
            }

            Shift(board);
        }

        void PerformJellyFish(ref BilgingBoard board, int y, int x, int p)
        {
            board[y * BilgingBoard.BoardWidth + x] = BilgingPiece.Empty;
            int piece = board[y * BilgingBoard.BoardWidth + p].Id;

            for (int i = 0; i < BilgingBoard.BoardWidth * BilgingBoard.BoardHeight; i++)
            {
                if (board[i].Id == piece)
                {
                    board[i] = BilgingPiece.Empty;
                }
            }

            Shift(board);
        }

        /// <summary>
        /// Iterates through the board and returns a list of swaps.
        /// </summary>
        /// <returns>The moves.</returns>
        /// <param name="board">The game board.</param>
        List<BilgingMove> GenerateMoves(BilgingBoard board)
        {
            List<BilgingMove> moves = new List<BilgingMove>();

            for (int i = 0; i < BilgingBoard.BoardHeight; i++)
            {
                // Only cols - 1 swaps are possible per row
                for (int j = 0; j < BilgingBoard.BoardWidth - 1; j++)
                {
                    BilgingMove move = new BilgingMove();
                    move.X = j;
                    move.Y = i;

                    moves.Add(move);

                    // skipping duplicate pufferfish move
                    if (board[i * BilgingBoard.BoardWidth + j + 1].Type == BilgingPieceType.Pufferfish)
                    {
                        j += 1;
                    }
                }
            }

            return moves;
        }

        BilgingBoard ApplyMove(BilgingBoard board, BilgingMove move, int previousClears)
        {
            BilgingBoard newBoard = board.CreateCopy();

            int x = move.X;
            int y = move.Y;

            int index = y * BilgingBoard.BoardWidth + x;

            BilgingPiece piece0 = newBoard[index];
            BilgingPiece piece1 = newBoard[index + 1];

            // In this cases we cannot perform any action in this spot
            if ((piece0.Type == BilgingPieceType.Crab && piece1.Type != BilgingPieceType.Pufferfish) ||
                (piece1.Type == BilgingPieceType.Crab && piece0.Type != BilgingPieceType.Pufferfish))
            {
                return newBoard;
            }

            if (piece0.Type == BilgingPieceType.Movable && piece1.Type == BilgingPieceType.Movable)
            {
                PerformSwap(ref newBoard, y, x);
                move.Score += ClearAll(ref newBoard, previousClears);
            }
            else if (piece0.Type == BilgingPieceType.Pufferfish)
            {
                PerformPufferfish(ref newBoard, y, x);
                ClearAll(ref newBoard, previousClears);
            }
            else if (piece1.Type == BilgingPieceType.Pufferfish)
            {
                PerformPufferfish(ref newBoard, y, x + 1);
                ClearAll(ref newBoard, previousClears);
            }
            else if (piece0.Type == BilgingPieceType.Jellyfish && piece1.Type == BilgingPieceType.Movable)
            {
                PerformJellyFish(ref newBoard, y, x, x + 1);
                ClearAll(ref newBoard, previousClears);
            }
            else if (piece1.Type == BilgingPieceType.Jellyfish && piece0.Type == BilgingPieceType.Movable)
            {
                PerformJellyFish(ref newBoard, y, x + 1, x);
                ClearAll(ref newBoard, previousClears);
            }

            return newBoard;
        }

        BilgingMove Search(BilgingBoard board, int searchDepth)
        {
            ConcurrentBag<BilgingMove> candidates = new ConcurrentBag<BilgingMove>(GenerateMoves(board));

            ParallelOptions parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 8 };

            Parallel.ForEach(candidates, parallelOptions, candidate =>
            {
                // applies and adds clears to moves[i].Score
                BilgingBoard newBoard = ApplyMove(board, candidate, 0);

                // force combo moves before useless ones
                if (candidate.Score > 0)
                {
                    candidate.Score += searchDepth;
                }

                // recursive case
                if (searchDepth > 1)
                {
                    candidate.Score += Search(newBoard, searchDepth - 1, newBoard.EmptyPiecesCount).Score;
                }
            });

            BilgingMove winner = new BilgingMove();
            winner.Score = -1;

            foreach (BilgingMove candidate in candidates)
            {
                if (candidate.Score > winner.Score)
                {
                    winner = candidate;
                }
            }

            return winner;
        }

        BilgingMove Search(BilgingBoard board, int searchDepth, int previousClears)
        {
            List<BilgingMove> candidates = GenerateMoves(board);

            BilgingMove winner = new BilgingMove();
            winner.Score = -1;

            // TODO: Consider paralelization
            foreach (BilgingMove candidate in candidates)
            {
                // applies and adds clears to moves[i].Score
                BilgingBoard newBoard = ApplyMove(board, candidate, previousClears);

                // force combo moves before useless ones
                if (candidate.Score > 0)
                {
                    candidate.Score += searchDepth;
                }

                // recursive case
                if (searchDepth > 1)
                {
                    candidate.Score += Search(newBoard, searchDepth - 1, newBoard.EmptyPiecesCount).Score;
                }

                if (candidate.Score > winner.Score)
                {
                    winner = candidate;
                }
            }

            return winner;
        }

        void CalculateMove()
        {
            if (!IsBoardComplete())
            {
                bestMove = null;
                return;
            }

            bestMove = Search(gameBoard, 3);
        }

        void PerformSwap(ref BilgingBoard board, int y, int x)
        {
            int index = y * BilgingBoard.BoardWidth + x;

            BilgingPiece aux = board[index];
            board[index] = board[index + 1];
            board[index + 1] = aux;
        }

        void Swap(ref BilgingBoard board, int x, int y)
        {

            BilgingPiece aux = board[x];
            board[x] = board[y];
            board[y] = aux;
        }

        BilgingBoard RetrieveBoard()
        {
            BilgingBoard board = new BilgingBoard();

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
                        board[x, y] = BilgingPiece.SquareDark;
                    }
                    else if (pixel22x22.R == 004 && pixel22x22.G == 220 && pixel22x22.B == 204 ||
                             pixel22x22.R == 002 && pixel22x22.G == 133 && pixel22x22.B == 180)
                    {
                        board[x, y] = BilgingPiece.SquareLight;
                    }
                    else if (pixel22x22.R == 025 && pixel22x22.G == 200 && pixel22x22.B == 243 ||
                             pixel22x22.R == 010 && pixel22x22.G == 125 && pixel22x22.B == 195)
                    {
                        board[x, y] = BilgingPiece.CircleDark;
                    }
                    else if (pixel22x22.R == 136 && pixel22x22.G == 226 && pixel22x22.B == 197 ||
                             pixel22x22.R == 054 && pixel22x22.G == 135 && pixel22x22.B == 177)
                    {
                        board[x, y] = BilgingPiece.CircleLight;
                    }
                    else if (pixel22x22.R == 059 && pixel22x22.G == 135 && pixel22x22.B == 150 ||
                             pixel22x22.R == 024 && pixel22x22.G == 099 && pixel22x22.B == 158)
                    {
                        board[x, y] = BilgingPiece.OctogonDark;
                    }
                    else if (pixel22x22.R == 087 && pixel22x22.G == 189 && pixel22x22.B == 245 ||
                             pixel22x22.R == 035 && pixel22x22.G == 121 && pixel22x22.B == 196)
                    {
                        board[x, y] = BilgingPiece.OctogonLight;
                    }
                    else if (pixel22x22.R == 007 && pixel22x22.G == 122 && pixel22x22.B == 235 ||
                             pixel22x22.R == 003 && pixel22x22.G == 094 && pixel22x22.B == 192)
                    {
                        board[x, y] = BilgingPiece.PentagonDark;
                    }
                    else if (pixel22x22.R == 250 && pixel22x22.G == 242 && pixel22x22.B == 068 ||
                             pixel22x22.R == 100 && pixel22x22.G == 142 && pixel22x22.B == 125)
                    {
                        board[x, y] = BilgingPiece.Pufferfish;
                    }
                    else if (pixel22x22.R == 000 && pixel22x22.G == 255 && pixel22x22.B == 232 ||
                             pixel22x22.R == 000 && pixel22x22.G == 147 && pixel22x22.B == 191)
                    {
                        board[x, y] = BilgingPiece.Jellyfish;
                    }
                    else if (pixel22x22.R == 026 && pixel22x22.G == 071 && pixel22x22.B == 124)
                    {
                        board[x, y] = BilgingPiece.Crab;
                    }
                    else
                    {
                        board[x, y] = BilgingPiece.Unknown;
                    }
                }
            }

            return board;
        }
    }
}
