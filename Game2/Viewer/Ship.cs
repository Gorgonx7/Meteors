using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Game2
{
    public class Ship
    {
        Vector2 m_Position;
        Vector2 m_Velocity;
        public static int m_Height;
        public static int m_Width;
        public static Rectangle shipRectange;
        int HP;
        int XP;
        private int moveSpeed = 10;
        public bool canMoveRight = true;
        public bool canMoveLeft = true;
        public Ship(Rectangle pShipRectange, int pWidth, int pHeight)
        {
            shipRectange = pShipRectange;
            m_Height = pHeight;
            m_Width = pWidth;
            m_Position = new Vector2(((m_Width - (shipRectange.Width)) / 2), m_Height - shipRectange.Height);
            m_Velocity = new Vector2(0, 0);
        }
        public void moveLeft()
        {
            getcanMove(ref canMoveLeft, ref canMoveRight);
            if (canMoveLeft)
            {
                m_Velocity.X = -moveSpeed;
                canMoveRight = true;
            }
        }
        public void moveRight()
        {
            getcanMove(ref canMoveLeft, ref canMoveRight);
            if (canMoveRight)
            {
                m_Velocity.X = moveSpeed;
                canMoveLeft = true;
            }
        }
        float seconds = 1.0f / 60.0f;
        public void normalise()
        {
            if (m_Velocity.X < 0)
            {
                m_Velocity.X++;
            }
            if (m_Velocity.X > 0)
            {
                m_Velocity.X--;
            }
        }
        public void getcanMove(ref bool left, ref bool right)
        {

            if (m_Position.X <= 0)
            {
                m_Position.X = 0;
                left = false;
            }
            else
            {
                left = true;
            }

            if (m_Position.X >= m_Width - shipRectange.Width)
            {
                m_Position.X = m_Width - shipRectange.Width;
                right = false;

            }
            else
            {
                right = true;
            }
            
        }
        public void update(float time)
        {
            getcanMove(ref canMoveLeft, ref canMoveRight);
            
            m_Position = m_Position + m_Velocity * time * 50;

        }
        public Vector2 getPosition()
        {
            return m_Position;
        }
    }
}
