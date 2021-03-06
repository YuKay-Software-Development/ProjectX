using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectX
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteDefault;
        SpriteBatch spriteMainMenu;
        SpriteBatch spriteGame;

        public enum GameState
        {
            MainMenu,
            SettingsMenu,
            Game,
            PauseMenu
        }

        enum GameLevels //fezrbthdyfyndubhdjgvfgbgn
        {
            level1
        }

        public static GameState currentGameState = GameState.MainMenu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 720;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        Texture2D cursorTex;
        Texture2D marioTex;
        public static Texture2D menuPlayTex;
        public static Texture2D menuQuitTex;
        public static Texture2D menuSettingsTex;

        public static SoundEffect menuButtonSound;

        Vector2 mouseLoc;
        Vector2 marioLoc = new Vector2(Vector2.Zero.X, 540);

        MainMenu mainMenu;

        protected override void LoadContent()
        {
            spriteDefault = new SpriteBatch(GraphicsDevice);
            spriteMainMenu = new SpriteBatch(GraphicsDevice);
            spriteGame = new SpriteBatch(GraphicsDevice);

            cursorTex = Content.Load<Texture2D>("Textures/sword");
            marioTex = Content.Load<Texture2D>("Textures/3310");

            //Menu Items
            menuPlayTex = Content.Load<Texture2D>("Textures/menuPlay");
            menuQuitTex = Content.Load<Texture2D>("Textures/menuQuit");
            menuSettingsTex = Content.Load<Texture2D>("Textures/menuSettings");

            menuButtonSound = Content.Load<SoundEffect>("SoundFX/MenuButton");

            mainMenu = new MainMenu();
        }

        protected void UpdateSprite(GameTime gametime)
        {

        }

        public static int getScreenCenterX(Texture2D texture) { return (graphics.GraphicsDevice.Viewport.Width - texture.Width) / 2; }
        public static int getScreenCenterY(Texture2D texture) { return (graphics.GraphicsDevice.Viewport.Height - texture.Height) / 2; }

        protected override void UnloadContent()
        {
            //TODO: Unload any non ContentManager content here
        }

        float sensitivity = 10; //I don't think I will need this

        protected override void Update(GameTime gameTime)
        {
            GamePadState gamePadState1 = GamePad.GetState(PlayerIndex.One); //PlayerIndex = Controller ID
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (gamePadState1.IsConnected)
            {
                if (gamePadState1.Buttons.Back == ButtonState.Pressed) this.Exit();

                //test
                float leftThumbStickX = gamePadState1.ThumbSticks.Left.X * sensitivity;
                float leftThumbStickY = gamePadState1.ThumbSticks.Left.Y * sensitivity;

                marioLoc = new Vector2(marioLoc.X + leftThumbStickX, marioLoc.Y - leftThumbStickY); 

            }
            
            //Don't do "else if" cuz otherwise first one has priority
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D)) marioLoc.X += sensitivity;
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A)) marioLoc.X -= sensitivity;
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)) marioLoc.Y -= sensitivity;
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S)) marioLoc.Y += sensitivity;
            if (keyboardState.IsKeyDown(Keys.Enter)) marioLoc = new Vector2(Vector2.Zero.X, 580);

            if (keyboardState.IsKeyDown(Keys.F1)) currentGameState = GameState.MainMenu;
            if (keyboardState.IsKeyDown(Keys.F2)) currentGameState = GameState.SettingsMenu;
            if (keyboardState.IsKeyDown(Keys.F3)) currentGameState = GameState.Game;

            //-------------------------------------------------------------------------------------------------------------------------------------------

            if (currentGameState != GameState.Game) mouseLoc = new Vector2(mouseState.X, mouseState.Y);
            else Mouse.SetPosition(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);

            if (IsActive)
            {
                mainMenu.btnPlay.Update(mouseState);
                mainMenu.btnSettings.Update(mouseState);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteDefault.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            if (currentGameState != GameState.Game) spriteDefault.Draw(cursorTex, new Vector2(mouseLoc.X - cursorTex.Width / 2, (mouseLoc.Y - cursorTex.Height / 2) + 25), Color.White); //sword

            if (currentGameState == GameState.MainMenu)
            {
                spriteMainMenu.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                spriteMainMenu.Draw(mainMenu.btnPlay.Texture, mainMenu.btnPlay.Rectangle, Color.White);
                spriteMainMenu.Draw(mainMenu.btnSettings.Texture, mainMenu.btnSettings.Rectangle, Color.White);
                spriteMainMenu.End();
            }
            else if (currentGameState == GameState.SettingsMenu)
            {

            }
            else if (currentGameState == GameState.Game)
            {
                spriteGame.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                spriteGame.Draw(marioTex, marioLoc, Color.White); //mario
                spriteGame.End();
            }

            spriteDefault.End();
            base.Draw(gameTime);
        }
    }
}
