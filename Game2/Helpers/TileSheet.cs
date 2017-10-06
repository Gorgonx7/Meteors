using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace Game2
{
    class TileSheet
    {
        Texture2D m_TileSheet;
        int m_Border;
        int m_Xspacing;
        int m_Yspacing;
        int m_TextureHeight;
        int m_TextureWidth;
        Rectangle[] m_TextureArray;
        public TileSheet(Texture2D pTileSheet, int numbTextures, int pTextureWidth, int pTextureHeight, int pBorder, int pXspacing, int pYspacing )
        {
            m_TileSheet = pTileSheet;
            m_Border = pBorder;
            m_Xspacing = pXspacing;
            m_Yspacing = pYspacing;
            m_TextureHeight = pTextureHeight;
            m_TextureWidth = pTextureWidth;
            m_TextureArray = new Rectangle[numbTextures];
            CompileTextureSheet();
        }
        public Texture2D GetTexture()
        {
            return m_TileSheet;
        }
        public Rectangle GetRect(int pTextureNumber)
        {
            return m_TextureArray[pTextureNumber];
        }
        private void CompileTextureSheet()
        {
            int x = m_Border;
            int y = m_Border;
            for (int i = 0; i < m_TextureArray.Length; i++)
            {
                m_TextureArray[i] = new Rectangle(x, y, m_TextureWidth, m_TextureHeight);

                x += m_TextureWidth + m_Xspacing;
                if (x > m_TileSheet.Width)
                {
                    y += m_TextureHeight + m_Yspacing;
                    x = m_Border;
                }
            }
        }
    }
}
