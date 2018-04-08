using NuciXNA.Primitives;

namespace YohohoPuzzleCheaters.Settings
{
    public class GraphicsSettings
    {
        /// <summary>
        /// Gets or sets the resolution.
        /// </summary>
        /// <value>The resolution.</value>
        public Size2D Resolution { get; set; }

        /// <summary>
        /// Gets or sets the window location.
        /// </summary>
        /// <value>The window location.</value>
        public Point2D WindowLocation { get; set; }

        public GraphicsSettings()
        {
            Resolution = new Size2D(270, 540);
        }
    }
}
