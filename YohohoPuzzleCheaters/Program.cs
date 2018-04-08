using System;
using System.Drawing;

using YohohoPuzzleCheaters.Cheats.Bilging;
using YohohoPuzzleCheaters.Common.Windows;
using YohohoPuzzleCheaters.Infrastructure;

namespace YohohoPuzzleCheaters
{
    class MainClass
    {
        static BilgingCheat bilgingCheat;

        static float elapsedTime;

        static DateTime lastUpdate;

        public static void Main(string[] args)
        {
            LoadContent();

            lastUpdate = DateTime.Now;
            while (true)
            {
                elapsedTime += (float)(DateTime.Now - lastUpdate).TotalMilliseconds;
                lastUpdate = DateTime.Now;

                Update(elapsedTime);
                Draw();
            }
        }

        public static void LoadContent()
        {
            bilgingCheat = new BilgingCheat();
            bilgingCheat.LoadContent();

            WindowManager.Instance.WindowLocation = new Point(278, 95);
            WindowManager.Instance.WindowSize = new Size(810, 604);
            WindowManager.Instance.LoadContent();

            SettingsManager.Instance.DebugMode = true;
            SettingsManager.Instance.LoadContent();
        }

        public static void UnloadContent()
        {
            bilgingCheat.UnloadContent();

            WindowManager.Instance.UnloadContent();
            SettingsManager.Instance.UnloadContent();
        }

        public static void Update(float elapsedTime)
        {
            WindowManager.Instance.Update(elapsedTime);
            SettingsManager.Instance.Update(elapsedTime);

            if (WindowManager.Instance.CurrentScreen == ScreenType.BilgingScreen)
            {
                bilgingCheat.Update(elapsedTime);
            }
        }

        public static void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Console.Clear();

            if (SettingsManager.Instance.DebugMode)
            {
                Console.WriteLine($"Elapsed time: {TimeSpan.FromMilliseconds(elapsedTime)}");
            }

            Console.WriteLine($"Screen: {WindowManager.Instance.CurrentScreen}");

            if (WindowManager.Instance.CurrentScreen == ScreenType.BilgingScreen)
            {
                bilgingCheat.Draw();
            }
        }
    }
}
