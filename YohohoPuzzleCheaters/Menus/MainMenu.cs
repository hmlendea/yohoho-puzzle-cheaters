using System;

using YohohoPuzzleCheaters.Cheats.TreasureDrop;

namespace YohohoPuzzleCheaters.Menus
{
    /// <summary>
    /// Main menu.
    /// </summary>
    public class MainMenu : Menu
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenu"/> class.
        /// </summary>
        public MainMenu()
        {
            Title = "Yohoho! Puzzle Cheaters";

            AddCommand("td", "Predict next move in Treasure Drop", PredictNextMove);
        }

        void PredictNextMove()
        {
            TreasureDropCheat td = new TreasureDropCheat();
            int bestColumn = td.GetNextMove();

            Console.WriteLine($"Best column: {bestColumn}");
        }
    }
}