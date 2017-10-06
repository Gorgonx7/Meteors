using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Game2
{
    /*
     * Animation stages
     * Player --------------------------------
     * Right
     * left
     * Up
     * Down
     * Death
     * Fire
     * Working
     * ---------------------------------------    
     */

    class Animator
    {
        private List<Rectangle> m_Frames;
        private int m_CurrentFrame;

        public Animator( List<Rectangle> pFrames)
        {
            
            m_Frames = pFrames;
            m_CurrentFrame = 0;
        }

        public Rectangle NextFrame()
        {
            if (m_CurrentFrame > m_Frames.Count)
            {
                m_CurrentFrame = 0;
            }
            Rectangle Holder = m_Frames[m_CurrentFrame];
            m_CurrentFrame++;
            return Holder;
        }

    }
}
