using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace Game2
{
    class Player
    {
        int m_hp;
        int m_PilotSkill;
        int m_GunnerSkill;
        int m_RepairSkill;
        int m_EngineSkill;
        int m_PlayerIndex;
        Texture2D PlaceHolder;
        Vector2 m_Position;
        Vector2 m_Velocity;
        TileSheet m_TileSheet;
        Animator[] m_Animator;
        Game1.GameState currentstate;
        
        Job currentJob = Job.Repair;
        public enum Job
        {
            pilot, Gunner, Engine, Repair
        }
        public Player(int pPlayerIndex, Texture2D pPlayerTexture)
        {
            m_hp = 10;
            m_PilotSkill = 1;
            m_GunnerSkill = 1;
            m_EngineSkill = 1;
            m_RepairSkill = 1;
            m_Animator = new Animator[7];
            currentstate = Game1.GameState.Crew;
            m_Position = new Vector2(0, 0);
            m_Velocity = new Vector2(0, 0);
            m_PlayerIndex = pPlayerIndex;
            PlaceHolder = pPlayerTexture;
        }
        public void UpdateAsCrew()
        {

            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);
            
            // You can also check the controllers "type"
            
            if (capabilities.IsConnected)
            {
                // Get the current state of Controller1
                GamePadState state = GamePad.GetState(PlayerIndex.One);

                // You can check explicitly if a gamepad has support for a certain feature
                if (capabilities.HasLeftXThumbStick)
                {
                    // Check teh direction in X axis of left analog stick
                    if (state.ThumbSticks.Left.X < -0.5f)
                        m_Velocity.X = 1.0f;
                    if (state.ThumbSticks.Left.X > 0.5f)
                        m_Velocity.X = 1.0f;
                }
            }
            
            if (capabilities.GamePadType == GamePadType.GamePad)
                {
                GamePadState state = GamePad.GetState(PlayerIndex.One);
                    if (state.IsButtonDown(Buttons.A))
                    {
                        // Game1.m_Exit();
                    }
                }
            

            m_Position = m_Position + m_Velocity * 1.0f / 60f;
        }
        public void UpdateAsPilot()
        {
            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);
            if (capabilities.IsConnected)
            {
                // Get the current state of Controller1
                GamePadState state = GamePad.GetState(PlayerIndex.One);

                // You can check explicitly if a gamepad has support for a certain feature
                if (capabilities.HasLeftXThumbStick)
                {
                    // Check teh direction in X axis of left analog stick
                    if (state.ThumbSticks.Left.X < -0.5f)
                        Game1.getPilot().getShip().moveLeft();
                    if (state.ThumbSticks.Left.X > 0.5f)
                        Game1.getPilot().getShip().moveRight();
                }
            }
        }
        public Vector2 getPosition()
        {
            return m_Position;
        }
        public Game1.GameState getCurrentState()
        {
            return currentstate;
        }
        public Texture2D getPlaceHolder()
        {
            return PlaceHolder;
        }
        
        

        
    }
}
