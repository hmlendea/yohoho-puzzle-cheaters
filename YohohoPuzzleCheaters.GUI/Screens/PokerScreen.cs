using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using NuciXNA.Graphics.Enumerations;
using NuciXNA.Gui;
using NuciXNA.Gui.GuiElements;
using NuciXNA.Gui.Screens;
using NuciXNA.Primitives;

using YohohoPuzzleCheaters.Cheats.Poker;
using YohohoPuzzleCheaters.Cheats.Poker.Entities;

namespace YohohoPuzzleCheaters.GUI.Screens
{
    /// <summary>
    /// Poker screen.
    /// </summary>
    public class PokerScreen : Screen
    {
        readonly PokerCheat pokerCheat;

        GuiText handText;
        GuiText deckText;

        public PokerScreen()
        {
            pokerCheat = new PokerCheat();
        }

        public override void LoadContent()
        {
            handText = new GuiText
            {
                Text = "Hand",
                FontName = "MenuFont",
                VerticalAlignment = VerticalAlignment.Centre,
                HorizontalAlignment = HorizontalAlignment.Centre
            };
            deckText = new GuiText
            {
                Text = "Deck",
                FontName = "MenuFont",
                VerticalAlignment = VerticalAlignment.Centre,
                HorizontalAlignment = HorizontalAlignment.Centre
            };

            GuiManager.Instance.GuiElements.Add(handText);
            GuiManager.Instance.GuiElements.Add(deckText);

            pokerCheat.Start();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            List<PokerCard> hand = pokerCheat.GetHand();
            List<PokerCard> deck = pokerCheat.GetDeck();

            handText.Text = $"Hand:{Environment.NewLine}";
            deckText.Text = $"Deck ({deck.Count}):{Environment.NewLine}";

            foreach (PokerCard card in hand)
            {
                handText.Text += $"{card.Number} of {card.Colour}{Environment.NewLine}";
            }

            foreach (PokerCard card in deck)
            {
                deckText.Text += $"{card.Number} of {card.Colour}{Environment.NewLine}";
            }
        }

        protected override void SetChildrenProperties()
        {
            int boxWidth = ScreenManager.Instance.Size.Width;
            int boxHeight = ScreenManager.Instance.Size.Height / 2;

            handText.Size = new Size2D(boxWidth, boxHeight);
            deckText.Size = new Size2D(boxWidth, boxHeight);

            handText.Location = new Point2D(0, 0);
            deckText.Location = new Point2D(0, boxHeight);
        }
    }
}
