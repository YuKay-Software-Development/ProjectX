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

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteDefault;
        SpriteBatch spriteMainMenu;
        SpriteBatch spriteGame;

        enum GameState
        {
            MainMenu,
            SettingsMenu,
            Game,
            PauseMenu,
        }

        enum GameLevels
        {
            level1,
        }

        enum MenuButtonState
        {
            Default,
            Hover,
            Pressed,
        }

        GameState currentGameState = GameState.MainMenu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        Texture2D cursorTex;
        Texture2D bgTex;
        Texture2D marioTex;
        Texture2D menuPlayTex;
        Texture2D menuQuitTex;
        Texture2D menuSettingsTex;
        Vector2 testLoc;
        Vector2 mouseLoc;
        Vector2 screenCenter;
        Vector2 marioLoc = new Vector2(Vector2.Zero.X, 540);

        protected override void LoadContent()
        {
            spriteDefault = new SpriteBatch(GraphicsDevice);
            spriteMainMenu = new SpriteBatch(GraphicsDevice);
            spriteGame = new SpriteBatch(GraphicsDevice);

            cursorTex = Content.Load<Texture2D>("sword");
            bgTex = Content.Load<Texture2D>("bg");
            marioTex = Content.Load<Texture2D>("8_Bit_Mario");

            //Menu Items
            menuPlayTex = Content.Load<Texture2D>("menuPlay");
            menuQuitTex = Content.Load<Texture2D>("menuQuit");
            menuSettingsTex = Content.Load<Texture2D>("menuSettings");

        }

        protected void UpdateSprite(GameTime gametime)
        {

            int Xmax = graphics.GraphicsDevice.Viewport.Width - cursorTex.Width;
            //int Xmin = 0;
            int Ymax = graphics.GraphicsDevice.Viewport.Height - cursorTex.Height;
            //int Ymin = 0;

            screenCenter = new Vector2(Xmax / 2, Ymax / 2);

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        float sensitivity = 5; //I don't think I will need this

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

            //Don't do "else if" cuz otherwise first one has priority
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D)) marioLoc.X += sensitivity;
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A)) marioLoc.X -= sensitivity;
            //if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)) testLoc.Y -= sensitivity;
            //if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S)) testLoc.Y += sensitivity;
            if (keyboardState.IsKeyDown(Keys.Enter)) marioLoc = new Vector2(Vector2.Zero.X, 580);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteDefault.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            spriteDefault.Draw(cursorTex, new Vector2(mouseLoc.X - cursorTex.Width / 2, (mouseLoc.Y - cursorTex.Height / 2) + 25), Color.White); //sword

            if (currentGameState == GameState.MainMenu)
            {
                spriteMainMenu.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                spriteMainMenu.Draw(menuPlayTex, new Vector2((graphics.GraphicsDevice.Viewport.Width - menuPlayTex.Width) / 2, 10), Color.White);
                //spriteMainMenu.Draw(marioTex, marioLoc, Color.White); //mario
                //spriteMainMenu.Draw(bg, testLoc, Color.White); //BG
                spriteMainMenu.End();
            }

            spriteDefault.End();
            base.Draw(gameTime);
        }
    }
}
