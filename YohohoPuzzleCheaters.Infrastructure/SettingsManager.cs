namespace YohohoPuzzleCheaters.Infrastructure
{
    /// <summary>
    /// Settings manager.
    /// </summary>
    public class SettingsManager
    {
        static volatile SettingsManager instance;
        static object syncRoot = new object();

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static SettingsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new SettingsManager();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Gets or sets the debug mode.
        /// </summary>
        /// <value>The debug mode.</value>
        public bool DebugMode { get; set; }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        public void LoadContent()
        {

        }

        /// <summary>
        /// Unloads the settings.
        /// </summary>
        public void UnloadContent()
        {

        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveContent()
        {

        }

        /// <summary>
        /// Update the settings.
        /// </summary>
        /// <param name="elapsedTime">Elapsed time.</param>
        public void Update(float elapsedTime)
        {

        }
    }
}
