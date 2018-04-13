using NuciXNA.Gui;
using NuciXNA.Gui.GuiElements;
using NuciXNA.Gui.Screens;
using NuciXNA.Primitives;

using YohohoPuzzleCheaters.Cheats.Patching;
using YohohoPuzzleCheaters.Cheats.Patching.Entities;

namespace YohohoPuzzleCheaters.GUI.Screens
{
    /// <summary>
    /// Patching screen.
    /// </summary>
    public class PatchingScreen : Screen
    {
        PatchingCheat cheat;

        public override void LoadContent()
        {
            cheat = new PatchingCheat();
            cheat.Start();

            base.LoadContent();
        }
    }
}
