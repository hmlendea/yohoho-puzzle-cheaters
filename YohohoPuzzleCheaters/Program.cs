using System;
using System.Drawing;

using YohohoPuzzleCheaters.Common.Windows;

using Timer = System.Timers.Timer;

namespace YohohoPuzzleCheaters
{
    class MainClass
    {
        static Timer timer;

        public static void Main(string[] args)
        {
            LoadContent();

            timer.Start();

            while (true)
            {
                int cursorX = Console.CursorLeft;
                int cursorY = Console.CursorTop;

                Console.ReadKey();

                Console.SetCursorPosition(cursorX, cursorY);
                Console.Write(' ');
                Console.SetCursorPosition(cursorX, cursorY);
            }
        }

        public static void LoadContent()
        {
            timer = new Timer();
            timer.Interval = 500;
            timer.Elapsed += delegate { Update(); };

            WindowManager.Instance.WindowLocation = new Point(278, 95);
        }

        public static void Update()
        {
            WindowManager.Instance.Update();
        }
    }
}
