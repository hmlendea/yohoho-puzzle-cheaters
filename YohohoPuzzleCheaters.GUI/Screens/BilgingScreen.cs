using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NuciXNA.Graphics;
using NuciXNA.Gui.Screens;
using NuciXNA.Primitives;

using YohohoPuzzleCheaters.Cheats.Bilging;
using YohohoPuzzleCheaters.Cheats.Bilging.Entities;

namespace YohohoPuzzleCheaters.GUI.Screens
{
    /// <summary>
    /// Splash screen.
    /// </summary>
    public class BilgingScreen : Screen
    {
        public const int BoardWidth = 6;
        public const int BoardHeight = 12;
        public const int PieceSize = 45;

        readonly BilgingCheat bilgingCheat;

        Sprite pieceSprite;
        Sprite targetSprite;

        public BilgingScreen()
        {
            BackgroundColour = Colour.CornflowerBlue;

            bilgingCheat = new BilgingCheat();
        }

        public override void LoadContent()
        {
            pieceSprite = new Sprite
            {
                ContentFile = "Cheats/Bilging/pieces"
            };
            targetSprite = new Sprite
            {
                ContentFile = "ScreenManager/FillImage",
                Tint = Colour.Red,
                Zoom = PieceSize,
                Location = new Point2D(40, 40)
            };

            pieceSprite.LoadContent();
            targetSprite.LoadContent();

            bilgingCheat.Start();

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            pieceSprite.UnloadContent();
            targetSprite.UnloadContent();

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            pieceSprite.Update(gameTime);
            targetSprite.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawTable(spriteBatch);
            DrawTarget(spriteBatch);

            base.Draw(spriteBatch);
        }

        void DrawTable(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < BoardHeight; y++)
            {
                for (int x = 0; x < BoardWidth; x++)
                {
                    BilgingPiece piece = bilgingCheat.GetPiece(x, y);

                    if (piece.Type == BilgingPieceType.Unknown ||
                        piece.Type == BilgingPieceType.Empty)
                    {
                        continue;
                    }

                    pieceSprite.SourceRectangle = new Rectangle2D(
                        piece.Id * PieceSize, 0,
                        PieceSize, PieceSize);
                    pieceSprite.Location = new Point2D(
                        x * PieceSize,
                        y * PieceSize);

                    pieceSprite.Draw(spriteBatch);
                }
            }
        }

        void DrawTarget(SpriteBatch spriteBatch)
        {
            BilgingMove bestMove = bilgingCheat.GetBestTarget();

            if (bestMove == null)
            {
                return;
            }

            targetSprite.Location = new Point2D(
                bestMove.X * PieceSize + PieceSize / 2,
                bestMove.Y * PieceSize);

            targetSprite.Draw(spriteBatch);
        }
    }
}
