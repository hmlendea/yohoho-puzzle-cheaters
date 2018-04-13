using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NuciXNA.Graphics;
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
        const int SpriteSheetColumns = 4;
        const int FullPieceSize = 56;

        PatchingCheat cheat;

        Sprite pieceSprite;

        public override void LoadContent()
        {
            cheat = new PatchingCheat();

            pieceSprite = new Sprite
            {
                ContentFile = "Cheats/Patching/pieces"
            };

            pieceSprite.LoadContent();

            cheat.Start();

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            pieceSprite.UnloadContent();

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            pieceSprite.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawBoard(spriteBatch);

            base.Draw(spriteBatch);
        }

        void DrawBoard(SpriteBatch spriteBatch)
        {
            PatchingBoard solution = cheat.Solution;

            if (solution == null)
            {
                return;
            }

            int pieceSize = ScreenManager.Instance.Size.Width / solution.Width;

            if (FullPieceSize != pieceSize)
            {
                pieceSprite.Zoom = (float)pieceSize / FullPieceSize;
            }

            for (int y = 0; y < solution.Height; y++)
            {
                for (int x = 0; x < solution.Width; x++)
                {
                    PatchingPiece piece = solution[x, y];

                    if (piece.Type == PatchingPieceType.Unknown)
                    {
                        continue;
                    }

                    int spriteX = piece.Value % SpriteSheetColumns;
                    int spriteY = piece.Value / SpriteSheetColumns;

                    pieceSprite.SourceRectangle = new Rectangle2D(
                        spriteX * FullPieceSize, spriteY * FullPieceSize,
                        FullPieceSize, FullPieceSize);
                    pieceSprite.Location = new Point2D(
                        x * pieceSize,
                        y * pieceSize);

                    pieceSprite.Draw(spriteBatch);
                }
            }
        }
    }
}
