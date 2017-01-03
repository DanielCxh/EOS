using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EOS
{
    class KeywordColor
    {
        public string Keyword { get; set; }
        public Color KColor { get; set; }

        public KeywordColor(string strKey, Color sColor)
        {
            Keyword = strKey;
            KColor = sColor;
        }
    }
}
