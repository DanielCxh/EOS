using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

namespace EOS
{
    class DrawMgt
    {
        public const uint DRAW_ST_IDLE      = 0;
        public const uint DRAW_ST_DRAWING   = 1;

        private Graphics m_canvas = null;

        public DrawMgt()
        {
 
        }

        public void RegisterCanvas(Graphics canvas)
        {
            if (null != canvas)
            {
                m_canvas = canvas;
            }
        }

        private void doDraw()
        {
 
        }
    }
}
