using System.ComponentModel.DataAnnotations;

namespace YohohoPuzzleCheaters.Cheats.Entities
{
    /// <summary>
    /// Treasure Drop game.
    /// </summary>
    public class TreasureDropGame
    {
        /// <summary>
        /// Gets or sets the round.
        /// </summary>
        /// <value>The round.</value>
        public int Round { get; set; }

        /// <summary>
        /// Gets or sets the round scores.
        /// </summary>
        /// <value>The round scores.</value>
        public int[] RoundScores { get; set; }

        /// <summary>
        /// Gets or sets the scores.
        /// </summary>
        /// <value>The scores.</value>
        public int[] Scores { get; set; }

        /// <summary>
        /// Gets or sets the levers.
        /// </summary>
        /// <value>The levers.</value>
        public TreasureDropLever[] Levers { get; set; }

        /// <summary>
        /// Gets or sets the current player index.
        /// </summary>
        /// <value>The current player.</value>
        [Range(0, 1)]
        public int CurrentPlayer { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreasureDropGame"/> class.
        /// </summary>
        public TreasureDropGame()
        {
            RoundScores = new int[2];
            Scores = new int[2];
            Levers = new TreasureDropLever[40];
        }
    }
}
