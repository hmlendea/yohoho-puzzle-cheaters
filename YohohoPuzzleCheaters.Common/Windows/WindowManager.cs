using System.Drawing;

namespace YohohoPuzzleCheaters.Common.Windows
{
    public class WindowManager
    {
        static volatile WindowManager instance;
        static object syncRoot = new object();

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static WindowManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new WindowManager();
                        }
                    }
                }

                return instance;
            }
        }

        public Point WindowLocation { get; set; }

        public ScreenType CurrentScreen { get; set; }

        public void Update()
        {
            Color clr000x000 = GetPixel(0, 0);
            Color clr449x000 = GetPixel(449, 0);
            Color clr191x009 = GetPixel(191, 9);
            Color clr234x025 = GetPixel(234, 25);
            Color clr367x015 = GetPixel(367, 15);

            if (clr000x000.R == 141 && clr000x000.G == 116 && clr000x000.B == 070 &&
                clr449x000.R == 105 && clr449x000.G == 081 && clr449x000.B == 041 &&
                clr191x009.R == 033 && clr191x009.G == 151 && clr191x009.B == 197 &&
                clr234x025.R == 223 && clr234x025.G == 208 && clr234x025.B == 137 &&
                clr367x015.R == 133 && clr367x015.G == 089 && clr367x015.B == 037)
            {
                CurrentScreen = ScreenType.BilgingScreen;
            }
            else
            {
                CurrentScreen = ScreenType.UnknownScreen;
            }
        }

        public Color GetPixel(int x, int y)
        {
            Bitmap screenBitmap = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(screenBitmap);

            Point source = new Point(WindowLocation.X + x, WindowLocation.Y + y);
            Point destination = new Point(0, 0);

            g.CopyFromScreen(source, destination, screenBitmap.Size, CopyPixelOperation.SourceCopy);

            return screenBitmap.GetPixel(0, 0);
        }
    }
}
