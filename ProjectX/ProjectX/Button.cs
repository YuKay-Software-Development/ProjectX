using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectX
{
    class Button
    {
        public enum ButtonState
        {
            Default,
            Hover,
            Pressed
        }

        public Button(string name, Vector2 position, Texture2D defaultTexture, Texture2D hoverTexture, Texture2D pressedTexture, Action callback)
        {
            this.name = name;
            this.callback = callback;
            this.position = position;
            this.defTexture = defaultTexture;
            this.hoverTexture = hoverTexture;
            this.pressedTexture = pressedTexture;
        }

        private string name;
        private Action callback;
        private ButtonState state = ButtonState.Default;
        private Vector2 position;
        private Texture2D currentTexture;
        private Texture2D defTexture;
        private Texture2D hoverTexture;
        private Texture2D pressedTexture;

        public string Name { get { return name; } }
        public ButtonState State { get { return state; } }
        public Rectangle Rectangle { get { return new Rectangle((int)position.X, (int)position.Y, defTexture.Width, defTexture.Height); } }
        public Texture2D Texture { get { return currentTexture; } }
    }
}
