using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Game2
{
    struct FontElement
    {
        private SpriteFont m_Font;
        private string m_Accessor;
        public FontElement(SpriteFont pFont, string pAccessor)
        {
            m_Font = pFont;
            m_Accessor = pAccessor;
        }
        public string GetAccessor()
        {
            return m_Accessor;
        }
        public SpriteFont GetFont()
        {
            return m_Font;
        }
    }
    public static class FontDictionary
    {
        private static List<FontElement> m_FontList = new List<FontElement>();
        public static void Addfont(SpriteFont pFont, string pString)
        {
            m_FontList.Add(new FontElement(pFont, pString));
        }
        public static SpriteFont GetFont(string pSearchTerm)
        {
            foreach(FontElement i in m_FontList)
            {
                if(i.GetAccessor() == pSearchTerm)
                {
                    return i.GetFont();
                }
            }
            throw new Exception("Font Not Found" + pSearchTerm);
        }
        
    }
}
