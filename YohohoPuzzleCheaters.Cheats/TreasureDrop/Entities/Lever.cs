using YohohoPuzzleCheaters.Cheats.TreasureDrop.Enumerations;

namespace YohohoPuzzleCheaters.Cheats.TreasureDrop.Entities
{
    /// <summary>
    /// Treasure Drop lever.
    /// </summary>
    public class Lever
    {
        /// <summary>
        /// Gets or sets the state of the lever.
        /// </summary>
        /// <value>The state of the lever.</value>
        public LeverState LeverState { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Lever"/> has a coin.
        /// </summary>
        /// <value><c>true</c> if it has a coin; otherwise, <c>false</c>.</value>
        public bool HasCoin { get; set; }
    }
}
