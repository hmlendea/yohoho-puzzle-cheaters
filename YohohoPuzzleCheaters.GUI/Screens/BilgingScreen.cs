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
                Zoom = BilgingCheat.PieceSize,
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
            for (int y = 0; y < BilgingCheat.TableRows; y++)
            {
                for (int x = 0; x < BilgingCheat.TableColumns; x++)
                {
                    BilgingPiece piece = bilgingCheat.GetPiece(x, y);

                    if (piece.Type == BilgingPieceType.Unknown ||
                        piece.Type == BilgingPieceType.Empty)
                    {
                        continue;
                    }

                    pieceSprite.SourceRectangle = new Rectangle2D(
                        piece.Id * BilgingCheat.PieceSize, 0,
                        BilgingCheat.PieceSize, BilgingCheat.PieceSize);
                    pieceSprite.Location = new Point2D(
                        x * BilgingCheat.PieceSize,
                        y * BilgingCheat.PieceSize);

                    pieceSprite.Draw(spriteBatch);
                }
            }
        }

        void DrawTarget(SpriteBatch spriteBatch)
        {
            BilgingMove bestMove = bilgingCheat.GetBestTarget();

            targetSprite.Location = new Point2D(
                bestMove.X * BilgingCheat.PieceSize + BilgingCheat.PieceSize / 2,
                bestMove.Y * BilgingCheat.PieceSize);

            targetSprite.Draw(spriteBatch);
        }
    }
}
