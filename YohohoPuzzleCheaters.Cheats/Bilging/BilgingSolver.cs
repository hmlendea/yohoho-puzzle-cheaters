using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

using YohohoPuzzleCheaters.Cheats.Bilging.Entities;
using YohohoPuzzleCheaters.Settings;

namespace YohohoPuzzleCheaters.Cheats.Bilging
{
    public class BilgingSolver
    {
        public BilgingMove CalculateMove(BilgingBoard board)
        {
            bool boardCompleted = !board.ContainsUnknownPieces && board.EmptyPiecesCount == 0;

            if (!boardCompleted)
            {
                return BilgingMove.InvalidMove;
            }

            return Search(board, SettingsManager.Instance.CheatSettings.BilgingRecursivityDepth);
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

            for (int j = 0; j < board.Width; j++)
            {
                // find the nearest (vertically) empty cell
                int nearestEmpty = 0;
                while (nearestEmpty < board.Height &&
                       board[nearestEmpty * board.Width + j].Type != BilgingPieceType.Empty)
                {
                    nearestEmpty += 1;
                }

                // find the nearest (vertically) non-empty cell after the empty cell
                int nearestNonEmpty = nearestEmpty + 1;
                while (nearestNonEmpty < board.Height &&
                       board[nearestNonEmpty * board.Width + j].Type == BilgingPieceType.Empty)
                {
                    nearestNonEmpty += 1;
                }

                // swap all empty cells with nearest cells
                while (nearestNonEmpty < board.Height)
                {
                    hasShifted |= true;
                    Swap(board, nearestEmpty * board.Width + j, nearestNonEmpty * board.Width + j);

                    // incrementing both nearestEmpty and nearestNonEmpty,
                    // note that nearestEmpty will always be empty by default
                    nearestEmpty += 1;
                    nearestNonEmpty += 1;

                    // refind nearest non-empty cell
                    while (nearestNonEmpty < board.Height &&
                           board[nearestNonEmpty * board.Width + j].Type == BilgingPieceType.Empty)
                    {
                        nearestNonEmpty += 1;
                    }
                }
            }

            return hasShifted;
        }

        int ClearCrabs(BilgingBoard board)
        {
            if (board.CrabsCount == 0)
            {
                return 0;
            }

            // note: this shift happens twice initially
            int crabsReleased = 0;

            for (int i = 0; i < board.Height - board.WaterLevel; i++)
            {
                for (int j = 0; j < board.Width; j++)
                {
                    int index = i * board.Width + j;

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

            for (int i = 0; i < board.Height; i++)
            {
                for (int j = 0; j < board.Width; j++)
                {
                    int index = i * board.Width + j;

                    BilgingPiece piece = board[index];

                    if (piece.Type != BilgingPieceType.Movable)
                    {
                        continue;
                    }

                    // horizontal right clears
                    if (j < board.Width - 2 &&
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
                    if (i < board.Height - 2 &&
                        piece.Id == board[index + board.Width].Id &&
                        piece.Id == board[index + board.Width * 2].Id)
                    {
                        nextGeneration[index] = BilgingPiece.Empty;
                        nextGeneration[index + board.Width] = BilgingPiece.Empty;
                        nextGeneration[index + board.Width * 2] = BilgingPiece.Empty;
                    }

                    // vertial above clears
                    if (i > 1 &&
                        piece.Id == board[index - board.Width].Id &&
                        piece.Id == board[index - board.Width * 2].Id)
                    {
                        nextGeneration[index] = BilgingPiece.Empty;
                        nextGeneration[index - board.Width] = BilgingPiece.Empty;
                        nextGeneration[index - board.Width * 2] = BilgingPiece.Empty;
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
            int iEnd = Math.Min(y + 1, board.Height - 1);

            int jBegin = Math.Max(x - 1, 0);
            int jEnd = Math.Min(x + 1, board.Width - 1);

            for (int i = iBegin; i <= iEnd; i++)
            {
                for (int j = jBegin; j <= jEnd; j++)
                {
                    board[i * board.Width + j] = BilgingPiece.Empty;
                }
            }

            Shift(board);
        }

        void PerformJellyFish(ref BilgingBoard board, int y, int x, int p)
        {
            board[y * board.Width + x] = BilgingPiece.Empty;
            int piece = board[y * board.Width + p].Id;

            for (int i = 0; i < board.Width * board.Height; i++)
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

            for (int i = 0; i < board.Height; i++)
            {
                // Only cols - 1 swaps are possible per row
                for (int j = 0; j < board.Width - 1; j++)
                {
                    BilgingMove move = new BilgingMove();
                    move.X = j;
                    move.Y = i;

                    moves.Add(move);

                    // skipping duplicate pufferfish move
                    if (board[i * board.Width + j + 1].Type == BilgingPieceType.Pufferfish)
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

            int index = y * board.Width + x;

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

            ParallelOptions parallelOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = SettingsManager.Instance.CheatSettings.BilgingThreadsToUse
            };

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

        void PerformSwap(ref BilgingBoard board, int y, int x)
        {
            int index = y * board.Width + x;

            BilgingPiece aux = board[index];
            board[index] = board[index + 1];
            board[index + 1] = aux;
        }

        void Swap(BilgingBoard board, int x, int y)
        {

            BilgingPiece aux = board[x];
            board[x] = board[y];
            board[y] = aux;
        }
    }
}
