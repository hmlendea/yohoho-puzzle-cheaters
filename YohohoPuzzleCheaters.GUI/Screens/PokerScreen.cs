using NuciXNA.Gui;
using NuciXNA.Gui.GuiElements;
using NuciXNA.Gui.Screens;
using NuciXNA.Primitives;

namespace YohohoPuzzleCheaters.GUI.Screens
{
    /// <summary>
    /// Poker screen.
    /// </summary>
    public class PokerScreen : Screen
    {
        GuiText text;

        public override void LoadContent()
        {
            text = new GuiText
            {
                Size = new Size2D(100, 30),
                Text = "POKER SCREEN",
                FontName = "MenuFont"
            };

            GuiManager.Instance.GuiElements.Add(text);

            base.LoadContent();
        }

        protected override void SetChildrenProperties()
        {
            text.Location = new Point2D(
                (ScreenManager.Instance.Size.Width - text.Size.Width) / 2,
                (ScreenManager.Instance.Size.Height - text.Size.Height) / 2);
        }
    }
}
