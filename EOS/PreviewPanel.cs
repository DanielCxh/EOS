using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EOS
{
    public partial class PreviewPanel : Form
    {
        public PreviewPanel()
        {
            InitializeComponent();
        }

        public PreviewPanel(Graphics g)
        {
            InitializeComponent();
        }

        private void PreviewPanel_MouseClick(object sender, MouseEventArgs e)
        {
            string strItem = DrawMgt.GetDrawedElement(e.X, e.Y);
            Console.WriteLine(strItem);
        }
    }
}
