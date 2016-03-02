using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ProjectX
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        Texture2D debugTex;
        Vector2 debugPos = Vector2.Zero;
        Vector2 debugSpd = new Vector2(50.0f, 50.0f);

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            debugTex = Content.Load<Texture2D>("debug");

            // TODO: use this.Content to load your game content here
        }

        protected void UpdateSprite(GameTime gametime)
        {

            debugPos += debugSpd * (float)gametime.ElapsedGameTime.TotalSeconds;

            int Xmax = graphics.GraphicsDevice.Viewport.Width - debugTex.Width;
            int Xmin = 0;
            int Ymax = graphics.GraphicsDevice.Viewport.Height - debugTex.Height;
            int Ymin = 0;

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        Vector2 testLoc;
        protected override void Update(GameTime gameTime)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (gamePadState.IsConnected)
            {
                // Allows the game to exit
                if (gamePadState.Buttons.Back == ButtonState.Pressed) this.Exit();

                //test
                float leftThumbStickX = gamePadState.ThumbSticks.Left.X * 10;
                float leftThumbStickY = gamePadState.ThumbSticks.Left.Y * 10;

                testLoc = new Vector2(testLoc.X + leftThumbStickX, testLoc.Y - leftThumbStickY);

                //loc reset
                if (gamePadState.Buttons.A == ButtonState.Pressed)
                {
                    testLoc = Vector2.Zero;
                    testLoc = Vector2.Zero;
                }

            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SandyBrown);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Draw(debugTex, testLoc, Color.Blue);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
