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
        Sprite highlightSprite;

        public override void LoadContent()
        {
            cheat = new PatchingCheat();

            pieceSprite = new Sprite
            {
                ContentFile = "Cheats/Patching/pieces"
            };
            highlightSprite = new Sprite
            {
                ContentFile = "ScreenManager/FillImage",
                Opacity = 0.33f
            };

            pieceSprite.LoadContent();
            highlightSprite.LoadContent();

            cheat.Start();

            base.LoadContent();
        }

        public override void UnloadContent()
        {
            pieceSprite.UnloadContent();
            highlightSprite.UnloadContent();

            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            pieceSprite.Update(gameTime);
            highlightSprite.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawSolution(spriteBatch);

            base.Draw(spriteBatch);
        }

        void DrawSolution(SpriteBatch spriteBatch)
        {
            PatchingBoard board = cheat.Board;
            PatchingBoard solution = cheat.Solution;

            if (board == null || solution == null)
            {
                return;
            }

            int pieceSize = ScreenManager.Instance.Size.Width / solution.Width;

            if (FullPieceSize != pieceSize)
            {
                pieceSprite.Scale = new Scale2D(
                    (float)pieceSize / FullPieceSize,
                    (float)pieceSize / FullPieceSize);
            }
            highlightSprite.Scale = new Scale2D(pieceSize, pieceSize);

            for (int y = 0; y < solution.Height; y++)
            {
                for (int x = 0; x < solution.Width; x++)
                {
                    PatchingPiece piece = solution[x, y];

                    Colour tint = Colour.White;

                    if (piece.Type == PatchingPieceType.Unknown)
                    {
                        continue;
                    }

                    if (board[x, y].Type != solution[x, y].Type)
                    {
                        tint = Colour.Green;
                    }
                    else if (board[x, y].Direction != solution[x, y].Direction)
                    {
                        tint = Colour.CornflowerBlue;
                    }

                    DrawPiece(spriteBatch, piece, new Point2D(x * pieceSize, y * pieceSize), tint);
                }
            }
        }

        void DrawPiece(SpriteBatch spriteBatch, PatchingPiece piece, Point2D location, Colour tint)
        {
            int spriteX = piece.Value % SpriteSheetColumns;
            int spriteY = piece.Value / SpriteSheetColumns;

            pieceSprite.Tint = tint;
            pieceSprite.Location = location;
            pieceSprite.SourceRectangle = new Rectangle2D(
                spriteX * FullPieceSize, spriteY * FullPieceSize,
                FullPieceSize, FullPieceSize);

            pieceSprite.Draw(spriteBatch);
        }
    }
}
