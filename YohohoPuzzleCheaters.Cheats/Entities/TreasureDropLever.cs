using YohohoPuzzleCheaters.Cheats.Enumerations;

namespace YohohoPuzzleCheaters.Cheats.Entities
{
    /// <summary>
    /// Treasure Drop lever.
    /// </summary>
    public class TreasureDropLever
    {
        /// <summary>
        /// Gets or sets the state of the lever.
        /// </summary>
        /// <value>The state of the lever.</value>
        public TreasureDropLeverState LeverState { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TreasureDropLever"/> has a coin.
        /// </summary>
        /// <value><c>true</c> if it has a coin; otherwise, <c>false</c>.</value>
        public bool HasCoin { get; set; }
    }
}
