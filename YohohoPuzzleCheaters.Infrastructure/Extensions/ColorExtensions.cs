using System;
using System.Drawing;

namespace YohohoPuzzleCheaters.Infrastructure.Extensions
{
    /// <summary>
    /// Color extensions.
    /// </summary>
    public static class ColorExtensions
    {
        public static bool EqualsWithTolerance(this Color me, Color other, int tolerance)
        {
            return Math.Abs(me.R - other.R) <= tolerance &&
                   Math.Abs(me.G - other.G) <= tolerance &&
                   Math.Abs(me.B - other.B) <= tolerance;
        }
    }
}
