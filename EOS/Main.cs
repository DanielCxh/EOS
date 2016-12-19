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
        bool m_bMouseDown = false;

        TreeNode m_crtNode = null;

        MouseDirection direction = MouseDirection.None;

        int m_iBaseX;
        int m_iBaseY;

        WgtProperty wp = null;

        public enum MouseDirection
        {
            Herizontal,     
            Vertical,
            Declining,   
            None
        }

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
                wp.Close();
                DrawMgt.SetCanvas(SplitContainerDetail.Panel1);
                DrawMgt.DrawNode(e.Node);
                m_crtNode = e.Node;
            }
            else if (CfgRes.IsColorResNode(e.Node))
            {
                Console.WriteLine(e.Node.FullPath);
                ResEdit re = new ResEdit(ProjMgt.GetInstance().GetProjectResLoc() + "\\" + e.Node.Parent.FullPath);
                re.Show();
            }

            if (ProjMgt.NodeDetailType.NT_WGT_BITMAP_IMG == ProjMgt.GetNoteDetailType(e.Node))
            {
                 DrawMgt.SetCanvas(SplitContainerDetail.Panel1);
                 DrawMgt.DrawNode(e.Node);
                 m_crtNode = e.Node;
            }

            BitmapImgJson obj = (BitmapImgJson)CfgWgt.GetWgtNodeContent(e.Node);

            if (null != obj)
            {
                SplitContainerDetail.Panel2.Controls.Clear();

                strExt = obj.Title;
                wp = new WgtProperty();
                wp.TopLevel = false;
                wp.Width = SplitContainerDetail.Panel2.Width;

                SplitContainerDetail.Panel2.Controls.Add(wp);
                wp.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
                

                wp.SetWgtNode(e.Node);
                wp.Show();
            }

            StatusBarInfo.Text = strExt;
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            m_bMouseDown = true;
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Location.X >= this.Width - 5 && e.Location.Y > this.Height - 5)
            {
                this.Cursor = Cursors.SizeNWSE;
                direction = MouseDirection.Declining;
            }
            //
            else if ((e.Location.X >= 0 && e.Location.X <=5 )
                || e.Location.X >= this.Width - 5)
            {
                this.Cursor = Cursors.SizeWE;
                direction = MouseDirection.Herizontal;
            }
            //
            else if (e.Location.Y >= this.Height - 5)
            {
                this.Cursor = Cursors.SizeNS;
                direction = MouseDirection.Vertical;

            }
            // Otherwise.
            else
            {
                this.Cursor = Cursors.Arrow;
                direction = MouseDirection.None;
            }

            if (MouseDirection.None != direction)
            {
                // Resize window.
                //resizeWindow();
            }
        }

        private void Main_MouseUp(object sender, MouseEventArgs e)
        {
            m_bMouseDown = false;
        }

        private void resizeWindow()
        {
            if (false == m_bMouseDown)
            {
                return;
            }

            if (direction == MouseDirection.Declining) 
            {
                this.Cursor = Cursors.SizeNWSE; 
               
                this.Width = MousePosition.X - this.Left; 
                this.Height = MousePosition.Y - this.Top; 
            }

            if (direction == MouseDirection.Herizontal) 
            {
                this.Cursor = Cursors.SizeWE; 
                this.Width = MousePosition.X - this.Left; 
            }
            else if (direction == MouseDirection.Vertical)
            {
                this.Cursor = Cursors.SizeNS;
                this.Height = MousePosition.Y - this.Top;
            }
            else
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private void SplitContainerDetail_Panel2_SizeChanged(object sender, EventArgs e)
        {
            if (null != wp)
            {
                wp.Width = SplitContainerDetail.Panel2.Width;
            }
        }

        private void SplitContainerDetail_Panel1_Paint(object sender, PaintEventArgs e)
        {
            if (null != m_crtNode)
            {
                DrawMgt.SetCanvas(SplitContainerDetail.Panel1);
                DrawMgt.DrawNode(m_crtNode);
            }
        }
    }
}
