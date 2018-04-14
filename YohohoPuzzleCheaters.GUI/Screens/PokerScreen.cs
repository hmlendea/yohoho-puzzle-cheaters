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
        GuiText oddsText;

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
                VerticalAlignment = VerticalAlignment.Left
            };
            deckText = new GuiText
            {
                Text = "Deck",
                FontName = "MenuFont",
                VerticalAlignment = VerticalAlignment.Left
            };
            oddsText = new GuiText
            {
                Text = "Odds",
                FontName = "MenuFont",
                VerticalAlignment = VerticalAlignment.Left
            };

            GuiManager.Instance.GuiElements.Add(handText);
            GuiManager.Instance.GuiElements.Add(deckText);
            GuiManager.Instance.GuiElements.Add(oddsText);

            pokerCheat.Start();

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            List<PokerCard> hand = pokerCheat.GetPocket();
            List<PokerCard> deck = pokerCheat.GetDeck();
            PokerOdds playerOdds = pokerCheat.GetPlayerOdds();
            PokerOdds opponentOdds = pokerCheat.GetOpponentOdds();
            int playersCount = pokerCheat.GetPlayersCount();

            string strPlayerWinOdds = string.Format("{0:##0.0}%", playerOdds.WinningOdds);
            string strOpponentWinOdds = string.Format("{0:##0.0}%", opponentOdds.WinningOdds);

            handText.Text = $"> Hand{Environment.NewLine}";
            deckText.Text = $"> Deck ({deck.Count}){Environment.NewLine}";
            oddsText.Text = $"> Odds ({strPlayerWinOdds} vs {strOpponentWinOdds}), {playersCount} players{Environment.NewLine}";

            foreach (PokerCard card in hand)
            {
                handText.Text += $"{card.Rank} of {card.Suit}{Environment.NewLine}";
            }

            foreach (PokerCard card in deck)
            {
                deckText.Text += $"{card.Rank} of {card.Suit}{Environment.NewLine}";
            }

            oddsText.Text += GetOddsString(playerOdds, opponentOdds);
        }

        protected override void SetChildrenProperties()
        {
            int boxWidth = ScreenManager.Instance.Size.Width;
            int boxHeight = ScreenManager.Instance.Size.Height / 3;

            handText.Size = new Size2D(boxWidth, boxHeight);
            deckText.Size = new Size2D(boxWidth, boxHeight);
            oddsText.Size = new Size2D(boxWidth, boxHeight);

            handText.Location = new Point2D(0, 0);
            deckText.Location = new Point2D(0, boxHeight);
            oddsText.Location = new Point2D(0, boxHeight * 2);
        }

        string OddsToString(double odds)
        {
            if (Math.Abs(odds) > 0.0)
            {
                if (odds * 100.0 >= 1.0)
                {
                    return string.Format("{0:##0.0}%", odds * 100.0);
                }

                return "<1%";
            }

            return "0%";
        }

        string GetOddsString(PokerOdds playerOdds, PokerOdds opponentOdds)
        {
            string oddsStr = string.Empty;

            if (playerOdds.HighCard > 0.0 && opponentOdds.HighCard > 0.0)
            {
                oddsStr += $"HighC: {OddsToString(playerOdds.HighCard)} vs {OddsToString(opponentOdds.HighCard)}{Environment.NewLine}";
            }

            if (playerOdds.OnePair > 0.0 && opponentOdds.OnePair > 0.0)
            {
                oddsStr += $"1Pair:  {OddsToString(playerOdds.OnePair)} vs {OddsToString(opponentOdds.OnePair)}{Environment.NewLine}";
            }

            if (playerOdds.TwoPair > 0.0 && opponentOdds.TwoPair > 0.0)
            {
                oddsStr += $"2Pair:  {OddsToString(playerOdds.TwoPair)} vs {OddsToString(opponentOdds.TwoPair)}{Environment.NewLine}";
            }

            if (playerOdds.ThreeOfKind > 0.0 && opponentOdds.ThreeOfKind > 0.0)
            {
                oddsStr += $"3Kind: {OddsToString(playerOdds.ThreeOfKind)} vs {OddsToString(opponentOdds.ThreeOfKind)}{Environment.NewLine}";
            }

            if (playerOdds.Straight > 0.0 && opponentOdds.Straight > 0.0)
            {
                oddsStr += $"Strai:   {OddsToString(playerOdds.Straight)} vs {OddsToString(opponentOdds.Straight)}{Environment.NewLine}";
            }

            if (playerOdds.Flush > 0.0 && opponentOdds.Flush > 0.0)
            {
                oddsStr += $"Flush: {OddsToString(playerOdds.Flush)} vs {OddsToString(opponentOdds.Flush)}{Environment.NewLine}";
            }

            if (playerOdds.FullHouse > 0.0 && opponentOdds.FullHouse > 0.0)
            {
                oddsStr += $"FullH: {OddsToString(playerOdds.FullHouse)} vs {OddsToString(opponentOdds.FullHouse)}{Environment.NewLine}";
            }

            if (playerOdds.FourOfKind > 0.0 && opponentOdds.FourOfKind > 0.0)
            {
                oddsStr += $"4Kind: {OddsToString(playerOdds.FourOfKind)} vs {OddsToString(opponentOdds.FourOfKind)}{Environment.NewLine}";
            }

            if (playerOdds.StraightFlush > 0.0 && opponentOdds.StraightFlush > 0.0)
            {
                oddsStr += $"StrFl:   {OddsToString(playerOdds.StraightFlush)} vs {OddsToString(opponentOdds.StraightFlush)}{Environment.NewLine}";
            }

            return oddsStr;
        }
    }
}
