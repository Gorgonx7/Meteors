using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors
{
    public enum Gamestate { active, pause, suspended, loading };
    
    class GameManager
    {
        public Gamestate m_Gamestate;
        public void Update() {

            switch (m_Gamestate) {

                case Gamestate.active:

                    break;
                case Gamestate.loading:

                    break;
                case Gamestate.pause:

                    break;
                case Gamestate.suspended:

                    break;
            }


        }
        
    }
}
