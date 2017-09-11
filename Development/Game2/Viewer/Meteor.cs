using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game2
{
    class Meteor
    {
        private Vector2 m_Position;
        private Vector2 m_Velocity;
        private Random m_RNG;
        private int m_WIDTH;
        private int m_HEIGHT;
        private int WALL;
        private Rectangle m_Rect;
        private Texture2D m_Texture;
        private float m_Angle;
        private Timer ExplosionTimer;
        private Texture2D m_ExplosionTexture;
        /*
          _ _ _1_ _ _
         |           |
         |           | 
        0|           |2
         |           |
         |     p     |
             */

        public Meteor(int pWidth, int pHeight, Texture2D pTexture, Texture2D pExplosionTexture, Rectangle pRect) {
            m_WIDTH = pWidth;
            m_HEIGHT = pHeight;
            m_RNG = new Random();
             setPosition();
             calculateVelocity();
            m_Texture = pTexture;
            m_Rect = pRect;
            m_ExplosionTexture = pExplosionTexture;
            ExplosionTimer = new Timer();
        }

        public float getAngle()
        {
            return m_Angle;
        }
        public void updateAngle()
        {
            if (m_Angle > 360)
            {
                m_Angle = 0;
            }
            else
            {
                m_Angle += 0.01f;
            }
        }
        public void setPosition() {
           
            int wall = m_RNG.Next(3);
            if(wall == 0)
            {
                WALL = 0;
                m_Position.X = -10;
                m_Position.Y = m_RNG.Next(m_HEIGHT - (m_HEIGHT / 4));
            } else if (wall == 1)
            {
                WALL = 1;
                m_Position.Y = -10;
                m_Position.X = m_RNG.Next(m_WIDTH);
            } else if (wall == 2)
            {
                WALL = 2;
                m_Position.X = m_WIDTH + 10;
                m_Position.Y = m_RNG.Next(m_HEIGHT - (m_HEIGHT / 4));
            }
            
        }
        public Vector2 GetPosition() {
            return m_Position;
        }
        public void Colision(Meteor meteors)
        {
            if((m_Position.X >= meteors.m_Position.X) && m_Position.X <= (meteors.m_Position.X + m_Texture.Width) && (m_Position.Y >= meteors.m_Position.Y) && (m_Position.Y <= meteors.m_Position.Y + m_Texture.Height))
            {
                Vector2 holder = m_Velocity;
                holder.X = holder.X / 0.75f;
                holder.Y = holder.Y / 0.75f;
                m_Velocity = meteors.m_Velocity;
                m_Velocity.X = m_Velocity.X / 0.75f;
                m_Velocity.Y = m_Velocity.Y / 0.75f;
                meteors.m_Velocity = holder;
            }
        }
        public Texture2D GetTexture() {
            return m_Texture;
        }
        public bool Destroy()
        {
            ExplosionTimer.Update();
            return ExplosionTimer.CheckTimer(15);
        }
        public void calculateVelocity()
        {
            
            switch (WALL)
            {
                case 0:
                    m_Velocity.X = m_RNG.Next(100) + 1;
                    m_Velocity.Y = m_RNG.Next(100);
                    if (m_RNG.Next() % 2 == 0)
                    {
                        m_Velocity.Y = -m_Velocity.Y;
                    }
                    
                    break;
                case 1:
                    m_Velocity.Y = m_RNG.Next(100) + 1;
                    m_Velocity.X = m_RNG.Next(100);
                    if (m_Position.X < m_WIDTH / 4)
                    {

                    } else if (m_Position.X > (m_WIDTH - (m_WIDTH / 4))) {
                        m_Position.X = -m_Position.X;
                    } else if (m_RNG.Next() % 2 == 0)
                    {
                        m_Velocity.Y = -m_Velocity.Y;
                    }
                    break;
                case 2:
                    m_Velocity.X = -m_RNG.Next(1000) + 1;
                    m_Velocity.Y = m_RNG.Next(1000);
                    if (m_RNG.Next() % 2 == 0)
                    {
                        m_Velocity.Y = -m_Velocity.Y;
                    }
                    break;
                default:
                    break;
            }
            
        }
        float seconds = 1.0f / 60f;
        public void Update() {
            
            m_Position = m_Position + m_Velocity * seconds;
            updateAngle();
        }
        public Rectangle getRect()
        {
            return m_Rect;
        }
        public void setRect(Rectangle pRect)
        {
            m_Rect = pRect;
        }
    }
}
