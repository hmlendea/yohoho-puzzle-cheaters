using System;
using System.Timers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NuciXNA.DataAccess.Resources;
using NuciXNA.Graphics;
using NuciXNA.Gui.Screens;
using NuciXNA.Input;
using NuciXNA.Primitives;

using YohohoPuzzleCheaters.Cheats.Bilging;
using YohohoPuzzleCheaters.Common.Windows;
using YohohoPuzzleCheaters.GUI;
using YohohoPuzzleCheaters.GUI.Screens;
using YohohoPuzzleCheaters.Settings;

namespace YohohoPuzzleCheaters
{
    /// <summary>
    /// This is the main type for the game.
    /// </summary>
    public class MainWindow : Game
    {
        readonly Timer windowReaderUpdateTimer;
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        readonly FpsIndicator fpsIndicator;
        readonly FakeCursor cursor;

        // TODO: Add this functionality to the NuciXNA.Gui package
        Type currentScreen;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            windowReaderUpdateTimer = new Timer();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            fpsIndicator = new FpsIndicator();
            cursor = new FakeCursor();

            currentScreen = typeof(UnknownScreen);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            Window.Position = new Point(0, 0);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            SettingsManager.Instance.LoadContent();

            WindowManager.Instance.WindowLocation = SettingsManager.Instance.GameWindowLocation;
            WindowManager.Instance.WindowSize = new Size2D(810, 604);
            WindowManager.Instance.LoadContent();

            GraphicsManager.Instance.SpriteBatch = spriteBatch;
            GraphicsManager.Instance.Graphics = graphics;

            ResourceManager.Instance.LoadContent(Content, GraphicsDevice);

            ScreenManager.Instance.SpriteBatch = spriteBatch;
            ScreenManager.Instance.StartingScreenType = typeof(UnknownScreen);
            ScreenManager.Instance.LoadContent();

            fpsIndicator.LoadContent();
            cursor.LoadContent();

            windowReaderUpdateTimer.Interval = 100;
            windowReaderUpdateTimer.Elapsed += delegate { WindowManager.Instance.Update(0); };
            windowReaderUpdateTimer.Start();
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        protected override void UnloadContent()
        {
            WindowManager.Instance.UnloadContent();
            SettingsManager.Instance.UnloadContent();
            ScreenManager.Instance.UnloadContent();

            fpsIndicator.UnloadContent();
            cursor.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            double elapsedTime = gameTime.ElapsedGameTime.TotalMilliseconds;

            SettingsManager.Instance.Update(elapsedTime);
            ScreenManager.Instance.Update(gameTime);

            InputManager.Instance.Update();

            fpsIndicator.Update(gameTime);
            cursor.Update(gameTime);

            base.Update(gameTime);

            if (WindowManager.Instance.CurrentScreen == ScreenType.BilgingScreen)
            {
                cursor.ReferenceLocation = new Point2D(
                    WindowManager.Instance.WindowLocation.X + BilgingBoardReader.BoardX,
                    WindowManager.Instance.WindowLocation.Y + BilgingBoardReader.BoardY);

                ChangeScreens(typeof(BilgingScreen));
            }
            else if (WindowManager.Instance.CurrentScreen == ScreenType.PokerScreen)
            {
                cursor.ReferenceLocation = SettingsManager.Instance.GraphicsSettings.WindowLocation;

                ChangeScreens(typeof(PokerScreen));
            }
            else
            {
                cursor.ReferenceLocation = SettingsManager.Instance.GraphicsSettings.WindowLocation;

                ChangeScreens(typeof(UnknownScreen));
            }

            // TODO: Move this to the NuciXNA.Graphics package
            SettingsManager.Instance.GraphicsSettings.WindowLocation = new Point2D(Window.Position.X, Window.Position.Y);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            ScreenManager.Instance.Draw(spriteBatch);

            fpsIndicator.Draw(spriteBatch);
            cursor.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        void ChangeScreens(Type screenType)
        {
            if (currentScreen != screenType)
            {
                ScreenManager.Instance.ChangeScreens(screenType);
                currentScreen = screenType;
            }
        }
    }
}
