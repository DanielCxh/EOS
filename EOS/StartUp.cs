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
    public partial class StartUp : Form
    {
        public StartUp()
        {
            InitializeComponent();
        }

        private void StartUp_DoubleClick(object sender, EventArgs e)
        {

        }

        public void StartUpClose()
        {
            this.Close();
        }

        private void StartUp_Load(object sender, EventArgs e)
        {
            Main main = new Main(this);
            main.ShowDialog();

            this.Hide();
        }
    }
}
