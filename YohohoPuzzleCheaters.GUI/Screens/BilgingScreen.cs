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

            bilgingCheat.LoadContent();
            pieceSprite.LoadContent();
            targetSprite.LoadContent();

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            bilgingCheat.UnloadContent();
            pieceSprite.UnloadContent();
            targetSprite.UnloadContent();

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            bilgingCheat.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
            pieceSprite.Update(gameTime);
            targetSprite.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawTable(spriteBatch);

            if (!bilgingCheat.ContainsUnknownPieces())
            {
                DrawTarget(spriteBatch);
            }

            base.Draw(spriteBatch);
        }

        void DrawTable(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < BilgingCheat.TableRows; y++)
            {
                for (int x = 0; x < BilgingCheat.TableColumns; x++)
                {
                    BilgingPieceType piece = bilgingCheat.GetPiece(x, y);

                    if (piece == BilgingPieceType.Unknown)
                    {
                        continue;
                    }

                    pieceSprite.SourceRectangle = new Rectangle2D(
                        ((int)piece - 1) * BilgingCheat.PieceSize, 0,
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
            BilgingResult bestTarget = bilgingCheat.GetBestTarget();

            targetSprite.Location = new Point2D(
                bestTarget.Selection1.X * BilgingCheat.PieceSize + BilgingCheat.PieceSize / 2,
                bestTarget.Selection1.Y * BilgingCheat.PieceSize);

            targetSprite.Draw(spriteBatch);
        }
    }
}
