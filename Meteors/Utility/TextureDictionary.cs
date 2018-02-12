using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meteors
{
    public struct TextureSet
    {
        public Texture2D m_Texture;
        public string m_Reference;
        public TextureSet(Texture2D pTexture, string pReference)
        {
            m_Texture = pTexture;
            m_Reference = pReference;
        }
    }
    public static class TextureDictionary
    {
        private static List<TextureSet> m_TextureDirecotry = new List<TextureSet>();
        public static void AddTexture(Texture2D pTexture, string pReference)
        {
            m_TextureDirecotry.Add(new TextureSet(pTexture, pReference));
        }
        public static void RemoveTexture(string pReference)
        {
            for (int x = 0; x < m_TextureDirecotry.Count; x++)
            {
                if (m_TextureDirecotry[x].m_Reference == pReference)
                {
                    m_TextureDirecotry.Remove(m_TextureDirecotry[x]);
                }
            }

        }
        public static Texture2D FindTexture(string pReference)
        {
            for (int x = 0; x < m_TextureDirecotry.Count; x++)
            {
                if (m_TextureDirecotry[x].m_Reference == pReference)
                {
                    return m_TextureDirecotry[x].m_Texture;
                }
            }
            throw new Exception("Texture Not Found Exception");
        }
    }
}
