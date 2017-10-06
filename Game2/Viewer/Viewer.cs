using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Game2
{
    public class Viewer
    {
        /*\
         *  So this class should render the outside of the ship for the gunners and poilet
         *  This class needs to be updated and assessed, it does not handle graphics at all itself it just acts like
         *  a controller for the other classes that view the game world such as window, pilot and gunner
         *  
         *  This class will not break emancipation but may have to have elements of self awairness using gets and sets 
         *  from the main class which also needs to be organised
         *  
         *  after organisign the main class this class can be split into like 3 different functions 
         *      initialise
         *      update
         *      draw
         * it should also contain information that may be useful to all the classes such as textures and stuff
         *  
         * 
        \*/
        private PilotMannager m_Pilot;
        private int m_Width;
        private int m_Height;
        private Texture2D m_ShipTexture = TextureDictionary.FindTexture("speedship");
        private Rectangle m_ShipRectange;
        public Viewer(int pWidth, int pHeight, Rectangle pShipRectange)
        {
            m_Width = pWidth;
            m_Height = pHeight;
           
            m_ShipRectange = pShipRectange;   
        }
        public PilotMannager createNewPiolet(Ship player)
        {
            m_Pilot = new PilotMannager(m_Width, m_Height, m_ShipRectange, player);
            return m_Pilot;
        }
        public void Update(GameTime gametime)
        {
            m_Pilot.Update();
        }
        public void Draw(SpriteBatch spritebatch)
        {

        }
    }
}
