using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NuciXNA.Graphics;
using NuciXNA.Gui.Screens;
using NuciXNA.Primitives;

using YohohoPuzzleCheaters.Cheats.Bilging;

namespace YohohoPuzzleCheaters.GUI.Screens
{
    /// <summary>
    /// Splash screen.
    /// </summary>
    public class BilgingScreen : Screen
    {
        readonly BilgingCheat bilgingCheat;

        Sprite piece;
        Sprite target;

        public BilgingScreen()
        {
            bilgingCheat = new BilgingCheat();
        }

        public override void LoadContent()
        {
            piece = new Sprite
            {
                ContentFile = "Cheats/Bilging/pieces"
            };
            target = new Sprite
            {
                ContentFile = "ScreenManager/FillImage",
                Tint = Colour.Red,
                Zoom = BilgingCheat.PieceSize,
                Location = new Point2D(40, 40)
            };

            bilgingCheat.LoadContent();
            piece.LoadContent();
            target.LoadContent();

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            bilgingCheat.UnloadContent();
            piece.UnloadContent();
            target.UnloadContent();

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            bilgingCheat.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
            piece.Update(gameTime);
            target.Update(gameTime);

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
                    int pieceId = bilgingCheat.GetPiece(x, y);

                    if (pieceId == (int)BilgingPiece.Unknown)
                    {
                        continue;
                    }

                    piece.SourceRectangle = new Rectangle2D(
                        (pieceId - 1) * BilgingCheat.PieceSize, 0,
                        BilgingCheat.PieceSize, BilgingCheat.PieceSize);
                    piece.Location = new Point2D(
                        x * BilgingCheat.PieceSize,
                        y * BilgingCheat.PieceSize);

                    piece.Draw(spriteBatch);
                }
            }
        }

        void DrawTarget(SpriteBatch spriteBatch)
        {
            int bestTarget = bilgingCheat.GetBestTarget();

            int targetX = bestTarget % BilgingCheat.TableColumns;
            int targetY = bestTarget / BilgingCheat.TableColumns;

            target.Location = new Point2D(
                targetX * BilgingCheat.PieceSize + BilgingCheat.PieceSize / 2,
                targetY * BilgingCheat.PieceSize);

            target.Draw(spriteBatch);
        }
    }
}
