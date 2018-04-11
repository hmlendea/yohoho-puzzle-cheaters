namespace YohohoPuzzleCheaters.Settings
{
    public class CheatSettings
    {
        /// <summary>
        /// Gets or sets the bilger recursivity depth.
        /// Higher values will yield better results, but at the cost of an exponential performance hit.
        /// Recommended value is around 3 (+- 1).
        /// </summary>
        /// <value>The bilger recursivity depth.</value>
        public int BilgingRecursivityDepth { get; set; }

        /// <summary>
        /// Gets or sets the number of threads to use for the Bilger.
        /// More threads will result in faster computations, but too many will cause performance degradation due to context switching.
        /// Recommended value is the number of threads of the local CPU (most likely 8).
        /// </summary>
        /// <value>The thread count.</value>
        public int BilgingThreadsToUse { get; set; }

        public CheatSettings()
        {
            BilgingRecursivityDepth = 3;
            BilgingThreadsToUse = 8;
        }
    }
}
