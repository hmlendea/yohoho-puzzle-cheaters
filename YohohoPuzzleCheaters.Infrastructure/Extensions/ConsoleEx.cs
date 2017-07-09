using System;

namespace YohohoPuzzleCheaters.Infrastructure.Extensions
{
    /// <summary>
    /// Console extras.
    /// </summary>
    public static class ConsoleEx
    {
        /// <summary>
        /// Writes the coloured text to standart output.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="foreColour">Foreground colour.</param>
        public static void WriteColoured(string text, ConsoleColor foreColour)
        {
            ConsoleColor oldForeColour = Console.ForegroundColor;

            Console.ForegroundColor = foreColour;

            Console.Write(text);

            Console.ForegroundColor = oldForeColour;
        }

        /// <summary>
        /// Writes the coloured text to standart output.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="foreColour">Foreground colour.</param>
        /// <param name="backColour">Background colour.</param>
        public static void WriteColoured(string text, ConsoleColor foreColour, ConsoleColor backColour)
        {
            ConsoleColor oldForeColour = Console.ForegroundColor;
            ConsoleColor oldBackColour = Console.BackgroundColor;

            Console.ForegroundColor = foreColour;
            Console.BackgroundColor = backColour;

            Console.Write(text);

            Console.ForegroundColor = oldForeColour;
            Console.BackgroundColor = oldBackColour;
        }

        /// <summary>
        /// Writes the coloured line to standart output.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="foreColour">Foreground colour.</param>
        public static void WriteLineColoured(string text, ConsoleColor foreColour)
        {
            ConsoleColor oldForeColour = Console.ForegroundColor;

            Console.ForegroundColor = foreColour;

            Console.WriteLine(text);

            Console.ForegroundColor = oldForeColour;
        }

        /// <summary>
        /// Writes the coloured line to standart output.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="foreColour">Foreground colour.</param>
        /// <param name="backColour">Background colour.</param>
        public static void WriteLineColoured(string text, ConsoleColor foreColour, ConsoleColor backColour)
        {
            ConsoleColor oldForeColour = Console.ForegroundColor;
            ConsoleColor oldBackColour = Console.BackgroundColor;

            Console.ForegroundColor = foreColour;
            Console.BackgroundColor = backColour;

            Console.WriteLine(text);

            Console.ForegroundColor = oldForeColour;
            Console.BackgroundColor = oldBackColour;
        }
    }
}
