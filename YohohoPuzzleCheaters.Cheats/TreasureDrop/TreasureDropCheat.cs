using YohohoPuzzleCheaters.Cheats.TreasureDrop.Entities;

namespace YohohoPuzzleCheaters.Cheats.TreasureDrop
{
    /// <summary>
    /// Treasure drop cheat.
    /// --------------------
    /// Algorithm originally developed by Jon Lund Steffensen
    /// Original source code (C language) can be found here: https://github.com/jonls/td-minimax
    /// </summary>
    public class TreasureDropCheat
    {
        Game game;
        TdHashTable gameHt;

        readonly int[] roundTarget;
        readonly int[,] roundScores;

        public TreasureDropCheat()
        {
            game = new Game();
            gameHt = new TdHashTable();

            roundTarget = new int[4] { 10, 40, 20, 80 };
            roundScores = new int[4, 16] {
                { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
                { 34, 21, 13, 8, 5, 3, 2, 1, 1, 2, 3, 5, 8, 13, 21, 34 },
                { 9, 8, 7, 6, 5, 4, 3, 2, 2, 3, 4, 5, 6, 7, 8, 9 },
                { 64, 49, 36, 25, 16, 9, 4, 1, 1, 4, 9, 16, 25, 36, 49, 64 }
            };

            InitialiseGame(false);
            InitializeHashtable(16 * 1024 * 1024);

            // TODO: Handle hastable initialisation failure
        }

        public int GetNextMove()
        {
            return -1;
        }

        void InitialiseGame(bool lastRoundHalved)
        {
            game.LastRoundHalved = lastRoundHalved;
        }

        void InitializeHashtable(int size)
        {
            gameHt.Size = size;
        }
    }
}
