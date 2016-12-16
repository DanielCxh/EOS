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
    public partial class Main : Form
    {
        StartUp m_Startup = null;
        bool m_bPressTitleBar = false;
        int m_iBaseX;
        int m_iBaseY;

        public Main()
        {
            InitializeComponent();
        }

        public Main(StartUp startup)
        {
            InitializeComponent();

            if (null != startup)
            {
                m_Startup = startup;
            }

            m_Startup.Hide();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Text = "LLLL";
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_Startup.Close();
        }

        private void Main_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void ResizeImg_MouseEnter(object sender, EventArgs e)
        {
            ResizeImg.BackColor = Color.Blue;
        }

        private void ResizeImg_MouseLeave(object sender, EventArgs e)
        {
            ResizeImg.BackColor = Color.DarkBlue;
        }

        private void CloseImg_MouseEnter(object sender, EventArgs e)
        {
            CloseImg.BackColor = Color.Red;
        }

        private void CloseImg_MouseLeave(object sender, EventArgs e)
        {
            CloseImg.BackColor = Color.DarkRed;
        }

        private void CloseImg_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResizeImg_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterParent;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void TitleBG_DragDrop(object sender, DragEventArgs e)
        {
           
           
        }

        private void TitleBG_MouseDown(object sender, MouseEventArgs e)
        {
            m_bPressTitleBar = true;
            m_iBaseX = e.X;
            m_iBaseY = e.Y;
        }

        private void TitleBG_MouseMove(object sender, MouseEventArgs e)
        {
            if (true == m_bPressTitleBar)
            {
                this.Location = new Point(this.Location.X + e.X - m_iBaseX, this.Location.Y + e.Y - m_iBaseY);
            }
        }

        private void TitleBG_MouseUp(object sender, MouseEventArgs e)
        {
            m_bPressTitleBar = false;
            m_iBaseX = 0;
            m_iBaseY = 0;
        }

        private void TitleBG_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                this.StartPosition = FormStartPosition.CenterParent;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void OpenProject_Click(object sender, EventArgs e)
        {
            ProjMgt.GetInstance().SetProjectTree(ProjectTree);
            ProjMgt.GetInstance().ShowFileDialog();
        }

        private void SplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            FreePanel.Hide();

            /* Set the focus on project tree, not split container. */
            ProjectTree.Focus();
        }

        private void SplitContainer_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            if (false == FreePanel.Visible)
            {
                //FreePanel.Show();
            }

            /*
            FreePanel.Location = new Point(0, 50);
            //FreePanel.BackColor = Color.FromArgb(0, Color.Green);
            FreePanel.BackColor = Color.FromArgb(80, 0, 255, 0);
            FreePanel.Width =  e.SplitX;
            FreePanel.Height = SplitContainer.Panel1.Height;
             * */
        }

        private void ProjectTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string strExt = "";

            if (e.Node.FullPath.EndsWith(".png") || e.Node.FullPath.EndsWith(".jpg"))
            {
               // Graphics g = this.CreateGraphics();

                Image img = Image.FromFile(ProjMgt.GetInstance().GetProjectResLoc() +"\\" + e.Node.FullPath);

                Graphics g = SplitContainer.Panel2.CreateGraphics();
                g.Clear(System.Drawing.Color.DimGray);

                int locX = (SplitContainer.Panel2.Width - img.Width) / 2;
                int locY = (SplitContainer.Panel2.Height - img.Height) / 2;

                g.FillRectangle(new SolidBrush(Color.White), locX, locY, img.Width, img.Height);
                g.DrawImage(img, locX, locY, img.Width, img.Height);

                g.Save();

                strExt = ProjMgt.GetInstance().GetProjectResLoc() + "\\" + e.Node.FullPath;

            }
            else if (CfgRes.IsColorResNode(e.Node))
            {
                Console.WriteLine(e.Node.FullPath);
                ResEdit re = new ResEdit(ProjMgt.GetInstance().GetProjectResLoc() + "\\" + e.Node.Parent.FullPath);
                re.Show();
            }

            BitmapImgJson obj = (BitmapImgJson)CfgWgt.GetWgtNodeContent(e.Node);
            
            StatusBarInfo.Text = strExt;
        }
    }
}
