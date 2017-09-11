using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Game2
{
    class Timer
    {
        private float timer;
        private float m_GameTime;
        private float totalTime;
        bool isSet;
        public Timer() {
            m_GameTime = 0.5f;
            isSet = false;
        }
        
        public bool CheckTimer(float TIMER)
        {
            
            if (!isSet)
            {
                totalTime = TIMER;
                timer = TIMER;
                isSet = true;
            }

            
            if (timer < 0)
            {
                
                timer = 0;
                isSet = false;
                return true;  //Reset Timer
            }
            else
            {
                
                
            }
            return false;
        }
        public float getPersentageTimeLeft()
        {
           
            float persentage = (timer / totalTime) * 100;
            return persentage;

        }
        public void Update()
        {

            if (isSet)
            {
                timer -= m_GameTime;
                
            }
                
        
        }
        public bool isNotSet() {
            return isSet;
        }
    }
}
