using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Game2
{
    class Projectile
    {
        private Vector2 m_Position;
        private Vector2 m_Velocity;
        private Random m_RNG;
        private Texture2D Texture;
        private int ProjectileSpeed;
        public Projectile(Vector2 pPosition, Texture2D pTexture) {
            m_Position = pPosition;
            ProjectileSpeed = -5;
            setVelocity();
            m_RNG = new Random();
            Texture = pTexture;
            
        }
        private void setVelocity() {

            m_Velocity.Y = ProjectileSpeed;
        }
        private void randomiseSidewaysVelocity() {

            float velocity = (m_RNG.Next(11) / 10);
            if (m_RNG.Next() % 2 == 0)
            {
                velocity = -velocity;
            }
           
            m_Velocity.X = velocity;
        }
        public bool MeteorColision(Meteor meteor) {
             Rectangle Projectile = new Rectangle((int)m_Position.X, (int)m_Position.Y, Texture.Width, Texture.Height);
             Rectangle Meteor = meteor.getRect();
             Meteor.X -= 40;
             Meteor.Y -= 40;
             return Projectile.Intersects(Meteor);
            /*
            Vector2 pos = meteor.GetPosition();
            if (pos.X < m_Position.X) return false;
            if (pos.X > (m_Position.X + Texture.Width)) return false;
            if (pos.Y < m_Position.Y) return false;
            if (pos.Y > (m_Position.Y + Texture.Height)) return false;*/
            return true;
        }
        public Vector2 GetPosition()
        {
            return m_Position;
        }
        public void Update(float Time) {

            randomiseSidewaysVelocity();
            m_Position = m_Position + m_Velocity * Time * 150;

        }
    }
}
