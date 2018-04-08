using System;
using System.IO;
using System.Reflection;

namespace YohohoPuzzleCheaters.Settings
{
    /// <summary>
    /// Application paths.
    /// </summary>
    public static class ApplicationPaths
    {
        static readonly string rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// Gets the user data directory.
        /// </summary>
        /// <value>The user data directory.</value>
        public static string UserDataDirectory => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "YohohoPuzzleCheaters");

        /// <summary>
        /// Gets the options file.
        /// </summary>
        /// <value>The options file.</value>
        public static string SettingsFile => Path.Combine(UserDataDirectory, "Settings.xml");
    }
}
