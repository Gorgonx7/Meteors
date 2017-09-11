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
   public class PilotMannager : Viewer
    {
        private Texture2D m_ShipTexture;
        private Ship player;
        
        public PilotMannager(int pWidth, int pHeight, Texture2D pShipTexture, Rectangle pShipRectange, Ship pPiolet ) : base(pWidth, pHeight, pShipTexture, pShipRectange) 
        {
            m_ShipTexture = pShipTexture;
            player = pPiolet;


        }
        public Vector2 returnShipPosition()
        {
            return player.getPosition();
        }
        public Ship getShip()
        {
            return player;
        }
        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {

                player.moveLeft();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                player.moveRight();
            }
            if (Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D))
            {
                player.normalise();
            }
            player.update(1.0f/60.0f);
        }

    }
}
