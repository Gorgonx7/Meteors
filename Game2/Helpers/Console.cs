using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Game2
{
    class Console
    {
        private List<string> logs;
        private SpriteFont m_Font;
        private Vector2 m_Position;
        private bool paused;
        private Texture2D line;
        private Vector2 m_PositionOfCurser;
        private bool isDrawn = false;
        private string id;

        public Console(SpriteFont pFont, Texture2D pTexture) {
            logs = new List<string>();
            m_Font = pFont;
            m_Position = new Vector2();
            line = pTexture;
            addLog("0");
            addLog("0");
            addLog("0");
            
            
            
        }
        public void setPaused(bool pPaused) {
            paused = pPaused;
        }
        public void addLog(string input)
        {
            logs.Add(input);

        }
        public void removeLog()
        {
            logs.RemoveAt(logs.Count - 1);
        }
        public void TakeInput() {
            if(paused == true)
            {

            }

        }
        public void ChangeLog(string pLog, int pIndex)
        {
            try
            {
                logs[pIndex] = pLog;
            }
            catch
            {
                logs.Add(pLog);
            }
        }
        public void Update(string Meteor, string missiles, string pmemerate)
        {
            logs[0] = Meteor;
            logs[1] = missiles;
            logs[2] = pmemerate;
        }
        public void drawLogs(SpriteBatch spritebatch)
        {
            
            for (int x = 0; x < logs.Count; x++) {
                spritebatch.DrawString(m_Font, logs[x],m_Position, Color.Yellow);
                m_Position.Y += 14;
            }
            if (paused)
            {

                m_PositionOfCurser = new Vector2(m_Position.Y, 2);
                if (isDrawn)
                {
                    DrawLine(spritebatch, m_PositionOfCurser, new Vector2(m_PositionOfCurser.X, m_PositionOfCurser.Y + 7));
                    isDrawn = true;
                } else
                    {
                    isDrawn = false;
                }
            }
            m_Position.Y = 0;
        }
        public void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle = (float)Math.Atan2(edge.Y, edge.X);
            sb.Draw(line, new Rectangle((int)start.X, (int)start.Y, (int)edge.Length(), 1), null, Color.Yellow, angle, new Vector2(0, 0), SpriteEffects.None, 0);

        }
        public List<string> getLogs() {
            return logs;
        }
    }
}
