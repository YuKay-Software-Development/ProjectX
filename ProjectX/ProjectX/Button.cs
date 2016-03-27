using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectX
{
    public class Button
    {
        public enum ButtonState
        {
            Default,
            Hover,
            Pressed
        }

        public Button(Game1.GameState enabledGameState, Vector2 position, Texture2D defaultTexture, Texture2D hoverTexture, Texture2D pressedTexture, Action callback)
        {
            this.enabledGameState = enabledGameState;
            this.callback = callback;
            this.position = position;
            this.defaultTexture = defaultTexture;
            this.hoverTexture = hoverTexture;
            this.pressedTexture = pressedTexture;
        }

        private Game1.GameState enabledGameState;
        private Action callback;
        private ButtonState state = ButtonState.Default;
        private Vector2 position;
        private Texture2D currentTexture;
        private Texture2D defaultTexture;
        private Texture2D hoverTexture;
        private Texture2D pressedTexture;
        private Vector2 mouseLoc;
        private bool pressed = false;
        private bool playSound = true;

        public ButtonState State { get { return state; } }
        public Rectangle Rectangle { get { return new Rectangle((int)position.X, (int)position.Y, defaultTexture.Width, defaultTexture.Height); } }
        public Texture2D Texture { get { return currentTexture; } }
        public void Update(MouseState mouseState)
        {
            mouseLoc = new Vector2(mouseState.X, mouseState.Y);

            if (Rectangle.Contains((int)mouseLoc.X, (int)mouseLoc.Y) && Game1.currentGameState == enabledGameState)
            {
                state = ButtonState.Hover;
                if (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    state = ButtonState.Pressed;
                    pressed = true;
                }

                if (pressed == true && mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
                {
                    callback.Invoke();
                    pressed = false;
                    state = ButtonState.Default;
                }
            }
            else state = ButtonState.Default;

            if (state == ButtonState.Hover)
            {
                currentTexture = hoverTexture;
                if (playSound == true)
                {
                    Game1.menuButtonSound.Play();
                    playSound = false;
                }
            }
            else if (state == ButtonState.Pressed) currentTexture = pressedTexture;
            else
            {
                currentTexture = defaultTexture;
                playSound = true;
            }
        }
    }
}
