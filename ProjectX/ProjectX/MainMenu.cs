using System;
using Microsoft.Xna.Framework;

namespace ProjectX
{
    public class MainMenu
    {
        public Button btnPlay;
        public Button btnSettings;
        public MainMenu()
        {
            //Add Textures

            btnPlay = new Button(Game1.GameState.MainMenu, new Vector2(Game1.getScreenCenterX(Game1.menuPlayTex), 10), Game1.menuPlayTex, Game1.menuPlayTex, Game1.menuPlayTex, new Action(()=>
            {
                Game1.currentGameState = Game1.GameState.Game;
            }));

            btnSettings = new Button(Game1.GameState.MainMenu, new Vector2(Game1.getScreenCenterX(Game1.menuSettingsTex), 120), Game1.menuSettingsTex, Game1.menuSettingsTex, Game1.menuSettingsTex, new Action(() =>
            {
                Game1.currentGameState = Game1.GameState.SettingsMenu;
            }));
        }
    }
}
