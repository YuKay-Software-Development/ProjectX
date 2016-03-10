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

        enum GameState
        {
            MainMenu,
            SettingsMenu,
            Game,
        }

        GameState currentGameState = GameState.MainMenu;

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
        Texture2D bg;
        Texture2D mario;
        Vector2 debugPos = Vector2.Zero;
        Vector2 debugSpd = new Vector2(50.0f, 50.0f);
        Vector2 testLoc;
        Vector2 mouseLoc;
        Vector2 screenCenter;
        Vector2 marioLoc = new Vector2(Vector2.Zero.X, 200);

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            debugTex = Content.Load<Texture2D>("sword");
            bg = Content.Load<Texture2D>("bg");
            mario = Content.Load<Texture2D>("8_Bit_Mario");

        }

        protected void UpdateSprite(GameTime gametime)
        {

            debugPos += debugSpd * (float)gametime.ElapsedGameTime.TotalSeconds;

            int Xmax = graphics.GraphicsDevice.Viewport.Width - debugTex.Width;
            //int Xmin = 0;
            int Ymax = graphics.GraphicsDevice.Viewport.Height - debugTex.Height;
            //int Ymin = 0;

            screenCenter = new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2);

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

        float sensitivity = 5;

        //test
        Vector2 velocity;
        readonly Vector2 gravity = new Vector2(0, -9.8f);

        protected override void Update(GameTime gameTime)
        {
            GamePadState gamePadState1 = GamePad.GetState(PlayerIndex.One); //PlayerIndex = Controller ID
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            mouseLoc = new Vector2(mouseState.X, mouseState.Y);

            if (gamePadState1.IsConnected)
            {
                // Allows the game to exit
                if (gamePadState1.Buttons.Back == ButtonState.Pressed) this.Exit();

                //test
                float leftThumbStickX = gamePadState1.ThumbSticks.Left.X * sensitivity;
                float leftThumbStickY = gamePadState1.ThumbSticks.Left.Y * sensitivity;

                marioLoc = new Vector2(marioLoc.X + leftThumbStickX, marioLoc.Y - leftThumbStickY);

                //loc reset
                if (gamePadState1.Buttons.A == ButtonState.Pressed)
                {
                    testLoc = Vector2.Zero;
                }

            }

            //Don't do else if cuz otherwise first one has priority
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D)) { marioLoc.X += sensitivity; }
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A)) { marioLoc.X -= sensitivity; }
            //if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)) { testLoc.Y -= sensitivity; }
            //if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S)) { testLoc.Y += sensitivity; }
            if (keyboardState.IsKeyDown(Keys.Enter)) { marioLoc = new Vector2(Vector2.Zero.X, 200); }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                //float time = (float)GameTime.ElapsedGameTime.TotalSeconds;
                velocity += gravity * 1;
                marioLoc += velocity * 1;

                
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
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Draw(debugTex, mouseLoc, Color.White); //sword
            spriteBatch.Draw(mario, marioLoc, Color.White); //mario
            //spriteBatch.Draw(bg, testLoc, Color.White); //BG
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
