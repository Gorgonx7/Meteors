using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game2
{
    class MainMenu
    {
        Texture2D m_Background;
        Texture2D m_Title;
        SpriteFont m_Font;
        string[] m_Options;
        int Width;
        int Height;
        int m_ActiveOption;
        int currentNumberofPlayers;
        enum SubMenu
        {
            MainMenu, ShipSelect, Lobby
        }
        SubMenu CurrentMenu;
        public MainMenu(Texture2D[] pAssets, SpriteFont pFont, int pWidth, int pHeight)
        {
            // m_Background = pAssets[0];
            //  m_Title = pAssets[1];
            m_Font = pFont;
            m_Options = new string[] { "Single Player", "Split Screen" };
            m_ActiveOption = 0;
            Width = pWidth;
            Height = pHeight;
            CurrentMenu = SubMenu.MainMenu;
            currentNumberofPlayers = 0;
        }
        public void Update()
        {
            switch (CurrentMenu)
            {
                case SubMenu.MainMenu:
                    UpdateMainMenu();
                    break;
                case SubMenu.Lobby:
                    UpdateLoby();
                    break;
            }

        }
        private void UpdateMainMenu()
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.W))
            {
                if (m_ActiveOption != 0)
                {
                    m_ActiveOption--;
                }
            }
            state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.S))
            {
                if (m_ActiveOption != m_Options.Length - 1)
                {
                    m_ActiveOption++;
                }
            }
            state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Enter))
            {
                ChangeState();
            }
        }
        private void UpdateLoby()
        {
            GamePadCapabilities[] capabilities = new GamePadCapabilities[] { GamePad.GetCapabilities(PlayerIndex.One), GamePad.GetCapabilities(PlayerIndex.Two), GamePad.GetCapabilities(PlayerIndex.Three), GamePad.GetCapabilities(PlayerIndex.Four) };
            currentNumberofPlayers = 0;
            for (int x = 0; x < capabilities.Length; x++)
            {
                if (capabilities[x].IsConnected)
                {
                    currentNumberofPlayers++;
                }
                
            }
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.A))
            {
                Game1.setGameState(Game1.GameState.Pilot);
            }
        }

        public int getNumberOfPlayers()
        {
            return currentNumberofPlayers;
        }
        private void ChangeState()
        {
            switch(m_ActiveOption)
            {
                case 0:
                    currentNumberofPlayers = 1;
                    Game1.setGameState(Game1.GameState.Pilot);
                    break;
                case 1:
                    CurrentMenu = SubMenu.Lobby;
                    break;
            }
        }
        public void Draw(SpriteBatch spritebatch)
        {
            switch (CurrentMenu)
            {
                case SubMenu.MainMenu:
                    DrawMainMenu(spritebatch);
                    break;
                case SubMenu.Lobby:
                    DrawLobby(spritebatch);
                    break;
                case SubMenu.ShipSelect:


                    break;
            }
        }
        private void DrawLobby(SpriteBatch spritebatch)
        {
            Vector2 Position = new Vector2(Width / 2, 230);
            GamePadCapabilities[] capabilities = new GamePadCapabilities[] { GamePad.GetCapabilities(PlayerIndex.One), GamePad.GetCapabilities(PlayerIndex.Two), GamePad.GetCapabilities(PlayerIndex.Three), GamePad.GetCapabilities(PlayerIndex.Four) };
            spritebatch.DrawString(m_Font, "Press Return to go Back, Press enter to start", new Vector2(Position.X, Position.Y - 30), Color.White);
            for (int x = 0; x < capabilities.Length; x++)
            {
                if (capabilities[x].IsConnected)
                {
                    spritebatch.DrawString(m_Font, "Player " + (x + 1) + " is connected", Position, Color.White);
                }
                else
                {
                    spritebatch.DrawString(m_Font, "Player " + (x + 1) + " is not connected", Position, Color.Gray);
                }
                Position.Y += 30;
            }
        }
        private void DrawMainMenu(SpriteBatch spritebatch)
        {
            Vector2 Position = new Vector2(Width / 2 - 50, 250);
            for (int x = 0; x < m_Options.GetLength(0); x++)
            {

                if (x == m_ActiveOption)
                {
                    spritebatch.DrawString(m_Font, m_Options[x], Position, Color.White);
                }
                else
                {

                    spritebatch.DrawString(m_Font, m_Options[x], Position, Color.Gray);
                }
                Position.Y += 30;

            }
        }
    }
}

