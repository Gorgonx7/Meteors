using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;
using Lidgren.Network;

namespace Game2 
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 
    /* Stuff todo
     * sort out ship interior
     * figure out enemy ai
     * while I'm at it figure out friendly ai
     * 
     * Research network programming in XNA
     * get enemys up and running in XNA
     * Figure out how to show this to simon or alex without looking like a attention seeking dick
     * 
     */
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D m_ShipTexture, m_spaceBackground, m_ProjectileTexture,m_ProjectileLoadBar, m_MeteorTexture, m_ProjectileLoadBorder;
        Rectangle m_LoadBarRect, m_MeteorRect, m_ShipRectange, m_persentageRect, m_backgroundRect, m_projectileRect;
        Console console;
        KeyboardState oldState = Keyboard.GetState();
        Viewer m_Viewer;
        static PilotMannager m_Pilot;
        Ship playerOne;
        MainMenu mainmenu;
        bool paused = false;
        int MeteorRate = 40;
        SpriteFont m_font;
        Timer timer;
        Timer MeteorTimer;
        Timer ExplostionTimer;
        Texture2D MeteorExplosoion;
        Random m_RNG;
        List<Projectile> projectiles = new List<Projectile>();
        List<Meteor> Meteors = new List<Meteor>();
        Player[] players = new Player[4] { null,null,null,null };
        List<Meteor> Explosions = new List<Meteor>();
        public enum GameState {
            MainMenu,
            Pilot,
            Crew
        }
        
        static GameState currentState;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }
       /* public static void m_Exit()
        {
            Exit();
        }*/

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
        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            m_ShipTexture = Content.Load<Texture2D>("speedship");
            m_ShipRectange = new Rectangle(0, 0, m_ShipTexture.Width, m_ShipTexture.Height);
            m_spaceBackground = Content.Load<Texture2D>("background");
            m_backgroundRect = new Rectangle(0, 0, m_spaceBackground.Width, m_spaceBackground.Height);
            m_ProjectileTexture = Content.Load<Texture2D>("laser");
            m_projectileRect = new Rectangle(0, 0, m_ProjectileTexture.Width, m_ProjectileTexture.Height);
            m_ProjectileLoadBar = Content.Load<Texture2D>("LoadBarMissileBackground");
            m_ProjectileLoadBorder = Content.Load<Texture2D>("LoadBarMissile");
            m_font = Content.Load<SpriteFont>("SpriteFont");
            m_LoadBarRect = new Rectangle(5, 465, m_ProjectileLoadBar.Width, m_ProjectileLoadBar.Height);
            m_MeteorTexture = Content.Load<Texture2D>("Meteor");
            m_MeteorRect = new Rectangle(0, 0, m_MeteorTexture.Width, m_MeteorTexture.Height);
            MeteorExplosoion = Content.Load<Texture2D>("Explosion");
            
            console = new Console(Content.Load<SpriteFont>("SpriteFont"), Content.Load<Texture2D>("Pixel"));
            //console.addLog("testLog");
            m_Viewer = new Viewer(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, m_ShipTexture, m_ShipRectange);
            playerOne = new Ship(m_ShipRectange, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            m_Pilot = m_Viewer.createNewPiolet(playerOne);
            timer = new Timer();
            MeteorTimer = new Timer();
            m_RNG = new Random();
            mainmenu = new MainMenu(new Texture2D[0], Content.Load<SpriteFont>("SpriteFont"), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            currentState = GameState.MainMenu;
            ExplostionTimer = new Timer();
            
            // TODO: use this.Content to load your game content here

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
        protected override void Update(GameTime gameTime)
        {
            
            switch (currentState)
            {
                case GameState.Pilot:
                    UpdateScreen();
                    break;
                case GameState.MainMenu:
                    UpdateMainMenu();
                    break;
            }
            


            base.Update(gameTime);
        }
        public void UpdateScreen()
        {
            
            bool ControllerIsConnected = false;
            for(int x = 0; x < mainmenu.getNumberOfPlayers(); x++)
            {
                GamePadCapabilities[] capabilities = new GamePadCapabilities[] { GamePad.GetCapabilities(PlayerIndex.One), GamePad.GetCapabilities(PlayerIndex.Two), GamePad.GetCapabilities(PlayerIndex.Three), GamePad.GetCapabilities(PlayerIndex.Four) };
                if (capabilities[x].IsConnected && players[x] == null)
                {
                    paused = false;
                    players[x] = new Player(x, m_ShipTexture);
                } else if(players[x] != null && !(capabilities[x].IsConnected))
                {
                    paused = true;
                    
                }
                if (capabilities[x].IsConnected)
                {
                    ControllerIsConnected = true;
                }
            }
           /* if (!ControllerIsConnected)
            {
                paused = true;

            }
            else
            {
                paused = false;
            }*/

            if (!paused)
            {
                console.setPaused(false);
                KeyboardState newState = Keyboard.GetState();
                for (int x = 0; x < players.Length; x++)
                {
                    if (players[x] == null)
                    {
                        continue;
                    }
                    if (players[x].getCurrentState() == GameState.Pilot)
                    {
                        players[x].UpdateAsPilot();
                    }
                }
                if (oldState.IsKeyUp(Keys.Back) && newState.IsKeyDown(Keys.Back))
                {
                    paused = true;
                }
                newState = oldState;
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                float seconds = 1.0f / 60.0f;
                // TODO: Add your update logic here

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (timer.CheckTimer(7f))
                    {
                        generateMisile();
                    }


                }

                for (int i = 0; i < projectiles.Count; i++)
                {

                    projectiles[i].Update(seconds);

                    if ((projectiles[i].GetPosition().Y < -50) || (projectiles[i].GetPosition().Y > graphics.PreferredBackBufferHeight))
                    {
                        projectiles.RemoveAt(i);
                    }
                }

                for (int i = 0; i < projectiles.Count; i++)
                {

                    for (int x = 0; x < Meteors.Count; x++)
                    {
                       try
                        {
                            if (projectiles[i].MeteorColision(Meteors[x]))
                            {
                                Explosions.Add(Meteors[x]);
                                projectiles.RemoveAt(i);
                                Meteors.RemoveAt(x);
                            }
                       }
                        catch
                        {
                            continue;
                        }
                    }

                }
                if(MeteorRate < 30)
                {
                    MeteorRate = 40;
                }
                if (m_RNG.Next() % MeteorRate == 0)
                {
                    Meteors.Add(new Meteor(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, m_MeteorTexture, MeteorExplosoion, m_MeteorRect));
                }
                for (int i = 0; i < Meteors.Count; i++)
                {
                    Meteors[i].Update();
                    for (int x = 0; x < Meteors.Count; x++)
                    {
                        if (i == x)
                        {
                            continue;
                        }
                        Meteors[i].Colision(Meteors[x]);
                    }
                    if ((Meteors[i].GetPosition().X < -100) || (Meteors[i].GetPosition().X > graphics.PreferredBackBufferWidth + 100) || (Meteors[i].GetPosition().Y < -100) || (Meteors[i].GetPosition().Y > graphics.PreferredBackBufferHeight + 100))
                    {
                        Meteors.RemoveAt(i);
                    }
                }

                timer.Update();
                console.Update("" + Meteors.Count + "", "" + projectiles.Count , "" + MeteorRate);
                m_Pilot.Update();
                if (MeteorTimer.CheckTimer(30f))
                {
                    MeteorRate--;
                }
                MeteorTimer.Update();
                
            }
            else
            {
                console.setPaused(true);
                KeyboardState newState = Keyboard.GetState();
                // handle paused menu
                console.Update("" + Meteors.Count + "", "" + projectiles.Count , "" + MeteorRate);
                if (oldState.IsKeyUp(Keys.Back) && newState.IsKeyDown(Keys.Back))
                {
                    paused = false;
                }
                newState = oldState;
            }
        }
        public void UpdateMainMenu()
        {
            mainmenu.Update();
        }
        public void generateMisile()
        {

            projectiles.Add(new Projectile(m_Pilot.returnShipPosition(), m_ProjectileTexture));


        }
        public void DrawOutside(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_spaceBackground, m_backgroundRect, Color.White);
            Vector2 playerPosition = m_Pilot.returnShipPosition();

            m_ShipRectange.X = (int)playerPosition.X;
            m_ShipRectange.Y = (int)playerPosition.Y;

            for (int x = 0; x < projectiles.Count; x++)
            {
                Vector2 projectilePosition = projectiles[x].GetPosition();
                m_projectileRect.X = (int)projectilePosition.X + (m_ShipTexture.Width / 2) - 6;
                m_projectileRect.Y = (int)projectilePosition.Y;
                spriteBatch.Draw(m_ProjectileTexture, m_projectileRect, Color.White);
            }
            for (int x = 0; x < Meteors.Count; x++)
            {
                Vector2 MeteorPosition = Meteors[x].GetPosition();

                float angle = Meteors[x].getAngle();
                MeteorPosition = Meteors[x].GetPosition();
                m_MeteorRect.X = (int)MeteorPosition.X;
                m_MeteorRect.Y = (int)MeteorPosition.Y;
                Vector2 MeteorCenter = new Vector2((m_MeteorTexture.Width / 2), (m_MeteorTexture.Height / 2));
                Meteors[x].setRect(m_MeteorRect);
                // console.addLog(m_MeteorRect.X + " " + m_MeteorRect.Y);
                spriteBatch.Draw(m_MeteorTexture, m_MeteorRect, new Rectangle(0, 0, m_MeteorTexture.Width, m_MeteorTexture.Height), Color.White, angle, MeteorCenter/* Vector2.Zero*/, SpriteEffects.None, 0f);
            }
            for(int x = 0; x < Explosions.Count; x++)
            {
                Vector2 ExplosionPosition = Explosions[x].GetPosition();
                Rectangle ExplosionRect = new Rectangle((int)ExplosionPosition.X, (int)ExplosionPosition.Y, MeteorExplosoion.Width, MeteorExplosoion.Height);
                spriteBatch.Draw(MeteorExplosoion, new Vector2(ExplosionRect.X - (50), ExplosionRect.Y - (50)), null, Color.White, 0, Vector2.Zero, new Vector2(0.5f,0.5f), SpriteEffects.None, 0f);
                if (Explosions[x].Destroy())
                {
                    Explosions.RemoveAt(x);
                }
            }
            
            spriteBatch.Draw(m_ShipTexture, m_ShipRectange, Color.White);
            spriteBatch.Draw(m_ProjectileLoadBar, m_LoadBarRect, Color.White);
            m_persentageRect = new Rectangle(0, 0, getPersentageCharge(), m_ProjectileLoadBar.Height);
            spriteBatch.Draw(m_ProjectileLoadBorder, m_LoadBarRect, m_persentageRect, Color.White);
            ExplostionTimer.Update();
            console.drawLogs(spriteBatch);
            //console.removeLog();
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            switch (currentState)
            {
                case GameState.Pilot:
                    DrawOutside(gameTime, spriteBatch);
                    break;
                case GameState.Crew:
                    DrawInterior(spriteBatch);
                    break;
                case GameState.MainMenu:
                    mainmenu.Draw(spriteBatch);
                    break;
            }
            
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
        public void DrawInterior(SpriteBatch spriteBatch)
        {
            for(int x = 0; x < players.Length; x++)
            {
                if(players[x] == null)
                {
                    continue;
                }
                if(players[x].getCurrentState() == GameState.Crew)
                {

                    spriteBatch.Draw(players[x].getPlaceHolder(), new Rectangle((int)players[x].getPosition().X, (int)players[x].getPosition().Y, players[x].getPlaceHolder().Width, players[x].getPlaceHolder().Height), Color.White);
                }
                if(players[x].getCurrentState() == GameState.Pilot)
                {
                    //draw at pilot
                }
            }
            
        }
        public GraphicsDeviceManager getGraphics()
        {
            return graphics;
        }
        public Rectangle getShipRectange()
        {
            return m_ShipRectange;
        }
        public int getPersentageCharge()
        {
            int output = (int)timer.getPersentageTimeLeft();
            if (output == 0)
            {
                output = 100;
            }
            return output;

        }
        public static void setGameState(GameState pGameState)
        {
            currentState = pGameState;
        }
        public static PilotMannager getPilot()
        {
            
            
                return m_Pilot;
            
        }
    }
}
