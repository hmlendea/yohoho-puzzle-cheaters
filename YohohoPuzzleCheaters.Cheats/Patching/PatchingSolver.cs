using System;
using System.Collections.Generic;
using System.Linq;

using YohohoPuzzleCheaters.Cheats.Patching.Entities;

namespace YohohoPuzzleCheaters.Cheats.Patching
{
    public class PatchingSolver
    {
        Random rnd;
        PatchingBoard gameBoard;

        int immovablesCount;
        int spoolIndex;
        List<int> tieOffIndexes;
        bool[] seenPositions;

        public PatchingSolver()
        {
            rnd = new Random();
        }

        public PatchingBoard CalculateSolution(PatchingBoard board)
        {
            if (board.ContainsUnknownPieces)
            {
                return null;
            }

            PatchingBoard solution = null;
            this.gameBoard = board.CreateCopy();
            InitialiseData();

            int generations = 0;
            int maxScore = -1;

            while (generations < 10)
            {
                PatchingBoard candidate = RandomPermutation();

                int score = IsValid();

                if (score == 0)
                {
                    continue;
                }

                if (score > maxScore)
                {
                    maxScore = score;
                    solution = candidate.CreateCopy();
                }

                generations += 1;
            }

            return solution;
        }

        void InitialiseData()
        {
            tieOffIndexes = new List<int>();

            for (int i = 0; i < gameBoard.Size; i++)
            {
                if (!gameBoard[i].IsMoveable)
                {
                    immovablesCount += 1;
                }

                if (gameBoard[i].Type == PatchingPieceType.Spool)
                {
                    spoolIndex = i;
                }

                if (gameBoard[i].Type == PatchingPieceType.TieOff)
                {
                    tieOffIndexes.Add(i);
                }
            }

            seenPositions = new bool[gameBoard.Size];
        }

        PatchingBoard RandomPermutation()
        {
            int immoveablesLeft = immovablesCount;

            for (int i = 0; i < gameBoard.Size; i++)
            {
                PatchingPiece piece = gameBoard[i];

                if (!piece.IsMoveable)
                {
                    immoveablesLeft -= 1;
                    continue;
                }

                int piecesLeft = gameBoard.Size - i;

                if (piecesLeft - 1 > immoveablesLeft)
                {
                    // find random index that has not been placed yet
                    int randomIndex = rnd.Next(piecesLeft);
                    while (!gameBoard[randomIndex].IsMoveable)
                    {
                        randomIndex = rnd.Next(piecesLeft);
                    }

                    Swap(gameBoard, i, randomIndex);
                }

                int basePiece = (gameBoard[i].Value / 4) * 4;
                int randomRotation = 0;

                if (gameBoard[i].Type == PatchingPieceType.Straight)
                {
                    randomRotation = rnd.Next(2);
                }
                else
                {
                    randomRotation = rnd.Next(4);
                }

                gameBoard[i] = (PatchingPiece)(basePiece + randomRotation);
            }

            return gameBoard;
        }

        int IsValid()
        {
            for (int i = 0; i < gameBoard.Size; i++)
            {
                seenPositions[i] = false;
            }

            PatchingLocation location = new PatchingLocation
            {
                IsActive = true,
                Position = spoolIndex,
                Direction = gameBoard[spoolIndex].Value
            };

            seenPositions[spoolIndex] = true; // mark start position

            List<PatchingLocation> locations = new List<PatchingLocation>();
            locations.Add(location);

            bool isValid = true;
            int score = 0;

            while (isValid && !IsFinished(locations))
            {
                isValid &= AdvancePositions(locations);
                score += locations.Count;

                isValid &= IsAccepting(locations);
                UpdateDirections(locations);
            }

            if (isValid)
            {
                return score;
            }

            return 0;
        }

        bool AdvancePositions(List<PatchingLocation> locations)
        {
            foreach (PatchingLocation location in locations)
            {
                // if we're terminal (spool is not terminal) then don't advance
                if (!(gameBoard[location.Position].IsMoveable &&
                      gameBoard[location.Position].Type != PatchingPieceType.Grommet) &&
                    gameBoard[location.Position].Type != PatchingPieceType.Spool)
                {
                    continue;
                }

                if (!location.IsActive)
                {
                    continue;
                }

                switch (location.Direction)
                {
                    case (int)PatchingPieceDirection.Left:
                        if (location.Position % gameBoard.Width == 0)
                        {
                            return false;
                        }
                        location.Position -= 1;
                        break;

                    case (int)PatchingPieceDirection.Up:
                        if (location.Position < gameBoard.Width)
                        {
                            return false;
                        }
                        location.Position -= gameBoard.Width;
                        break;

                    case (int)PatchingPieceDirection.Right:
                        if (location.Position % gameBoard.Width == gameBoard.Width - 1)
                        {
                            return false;
                        }
                        location.Position += 1;
                        break;

                    case (int)PatchingPieceDirection.Down:
                        if (location.Position >= gameBoard.Width * (gameBoard.Height - 1))
                        {
                            return false;
                        }
                        location.Position += gameBoard.Width;
                        break;

                    default:
                        return false;
                }
            }

            return true;
        }

        bool IsAccepting(List<PatchingLocation> locations)
        {
            foreach (PatchingLocation location in locations)
            {
                if (!IsAccepting(location))
                {
                    return false;
                }
            }

            return true;
        }

        bool IsAccepting(PatchingLocation location)
        {
            PatchingPiece piece = gameBoard[location.Position];

            // TODO: This doesn't work ???
            if (piece.IsMoveable &&
                piece.Type != PatchingPieceType.Grommet &&
                seenPositions[location.Position])
            {
                location.IsActive = false;
            }

            if (!location.IsActive)
            {
                return true;
            }

            int direction = location.Direction;

            switch (piece.Type)
            {
                case PatchingPieceType.Blocker:
                    return false;

                case PatchingPieceType.Straight:
                    return direction % 2 == piece.Value % 2;

                case PatchingPieceType.Elbow:
                    return isTurnAccepting[(int)piece.Direction, direction];

                case PatchingPieceType.Tee:
                    return (piece.Value - PatchingPiece.TeeLeft.Value + 2) % 4 != direction;

                case PatchingPieceType.Grommet:
                    return isAdvanceAccepting[(int)piece.Direction, direction];

                case PatchingPieceType.TieOff:
                    return isAdvanceAccepting[(int)piece.Direction, direction];

                case PatchingPieceType.Spool:
                    // check against opposite direction
                    if (isAdvanceAccepting[(int)piece.Direction, (direction + 2) % 4])
                    {
                        location.IsActive = false;
                        return false;
                    }

                    return isAdvanceAccepting[(int)piece.Direction, direction];
            }

            return false;
        }

        void UpdateDirections(List<PatchingLocation> locations)
        {
            int initialSize = locations.Count;

            // we modify the list so we cannot foreach
            for (int i = 0; i < initialSize; i++)
            {
                PatchingLocation location = locations[i];

                if (!location.IsActive)
                {
                    continue;
                }

                PatchingPiece piece = gameBoard[location.Position];
                int previousDirection = location.Direction;

                if (piece.Type == PatchingPieceType.Elbow)
                {
                    location.Direction = turnToDirection[(int)piece.Direction, previousDirection];
                }
                else if (piece.Type == PatchingPieceType.Tee)
                {
                    // determine if it is a reoccurring loop. if yes, then remove the location from the list
                    if (seenPositions[location.Position])
                    {
                        location.IsActive = false;
                    }
                    else
                    {
                        int direction1 = teeToDirection[(int)piece.Direction, previousDirection, 0];
                        int direction2 = teeToDirection[(int)piece.Direction, previousDirection, 1];

                        PatchingLocation loc = new PatchingLocation
                        {
                            IsActive = true,
                            Position = location.Position,
                            Direction = direction2
                        };

                        locations.Add(loc);
                        location.Direction = direction1;
                    }
                }
            }
        }

        bool IsFinished(List<PatchingLocation> locations)
        {
            List<int> foundTieOffs = new List<int>();

            foreach (PatchingLocation location in locations)
            {
                if (!location.IsActive)
                {
                    continue;
                }

                PatchingPiece piece = gameBoard[location.Position];

                if (piece.Type == PatchingPieceType.TieOff)
                {
                    foundTieOffs.Add(location.Position);
                    continue;
                }

                if (piece.Type != PatchingPieceType.Grommet)
                {
                    return false;
                }
            }

            if (tieOffIndexes.Count > foundTieOffs.Count)
            {
                return false;
            }

            return tieOffIndexes.Count == foundTieOffs.Count &&
                   tieOffIndexes.All(foundTieOffs.Contains);
        }

        void Swap(PatchingBoard board, int index1, int index2)
        {
            PatchingPiece aux = board[index1];
            board[index1] = board[index2];
            board[index2] = aux;
        }

        bool[,] isTurnAccepting =
        {
            { true, false, false, true},
            { false, false, true, true},
            { false, true, true, false},
            { true, true, false, false}
        };

        bool[,] isAdvanceAccepting =
        {
            { true, false, false ,false },
            { false, false, false, true },
            { false, false, true, false },
            { false, true, false, false }
        };

        // given the "turn" piece you're looking at AND current direction it returns the direction you will end up at
        int[,] turnToDirection =
        {
            { (int)PatchingPieceDirection.Up, 0, 0, (int)PatchingPieceDirection.Right },
            { 0, 0, (int)PatchingPieceDirection.Up, (int)PatchingPieceDirection.Left },
            { 0, (int)PatchingPieceDirection.Left, (int)PatchingPieceDirection.Down, 0 },
            { (int)PatchingPieceDirection.Down, (int)PatchingPieceDirection.Right, 0, 0 }
        };

        // given the "turn + straight" piece you're looking at AND curent direction it returns (index 0 and 1) array of directions you split into
        int[,,] teeToDirection =
        {
            {
                { (int)PatchingPieceDirection.Up, (int)PatchingPieceDirection.Down },
                { (int)PatchingPieceDirection.Up, (int)PatchingPieceDirection.Right },
                { 0, 0 },
                { (int)PatchingPieceDirection.Down, (int)PatchingPieceDirection.Right }
            },
            {
                { (int)PatchingPieceDirection.Left, (int)PatchingPieceDirection.Down },
                { (int)PatchingPieceDirection.Left, (int)PatchingPieceDirection.Right },
                { (int)PatchingPieceDirection.Down, (int)PatchingPieceDirection.Right },
                { 0, 0 }
            },
            {
                { 0, 0 },
                { (int)PatchingPieceDirection.Left, (int)PatchingPieceDirection.Up },
                { (int)PatchingPieceDirection.Up, (int)PatchingPieceDirection.Down },
                { (int)PatchingPieceDirection.Left, (int)PatchingPieceDirection.Down }
            },
            {
                { (int)PatchingPieceDirection.Left, (int)PatchingPieceDirection.Up},
                { 0, 0 },
                { (int)PatchingPieceDirection.Up, (int)PatchingPieceDirection.Right },
                { (int)PatchingPieceDirection.Left, (int)PatchingPieceDirection.Right }
            }
        };
    }
}
