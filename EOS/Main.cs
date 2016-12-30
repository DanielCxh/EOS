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
        bool m_bCanvasMouseDown = false;
        TreeNodeJson m_movedTreeNode = null;

        int m_iCanvasBaseX = -1;
        int m_iCanvasBaseY = -1;
        int m_iEleX = -1;
        int m_iEleY = -1;

        TreeNode m_crtNode = null;

        MouseDirection direction = MouseDirection.None;

        int m_iBaseX = -1;
        int m_iBaseY = -1;
       
        TreeNodeJson selectTreeNode = null;

        WgtProperty wp = null;
        TreeProperty tp = null;

        DrawMgt detailPanel = null;

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

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
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
            /* Press title bar and current window is not max size. */
            if (true == m_bPressTitleBar && this.WindowState != FormWindowState.Maximized)
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
            bool showWgtPropertyPanel = false;

            if (null != wp)
            {
                wp.Close();
            }

            if (e.Node.FullPath.EndsWith(".png") || e.Node.FullPath.EndsWith(".jpg"))
            {
                detailPanel = new DrawMgt(SplitContainerDetail.Panel1);
                detailPanel.DrawNode(e.Node);
                m_crtNode = e.Node;

                return;
            }
            else if (CfgRes.IsColorResNode(e.Node))
            {
                Console.WriteLine(e.Node.FullPath);
                ResEdit re = new ResEdit(ProjMgt.GetInstance().GetProjectResLoc() + "\\" + e.Node.Parent.FullPath);
                re.Show();
            }
            else if (CfgWgt.IsWgtNode(e.Node))
            {
                LogMgt.Debug("Main.ProjectTree_NodeMouseDoubleClick", "Is wgt node.");

                 detailPanel = new DrawMgt(SplitContainerDetail.Panel1);
                 detailPanel.DrawNode(e.Node);
                 m_crtNode = e.Node;

                 showWgtPropertyPanel = true;
            }
            else if (ProjMgt.NodeDetailType.NT_TREE_ITEM == ProjMgt.GetNoteDetailType(e.Node))
            {
                onShowTreeNode(e.Node);

                showWgtPropertyPanel = false;
            }

            if (true == showWgtPropertyPanel)
            {
                SplitContainerDetail.Panel2.Controls.Clear();

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
                detailPanel = new DrawMgt(SplitContainerDetail.Panel1);
                detailPanel.DrawNode(m_crtNode);
            }
        }

        private void SplitContainerDetail_Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void SplitContainerDetail_Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //string strName = detailPanel.GetDrawedElementTitle(e.X, e.Y);

                detailPanel.DrawSelectCursor(e.X, e.Y);

                m_iCanvasBaseX = e.X;
                m_iCanvasBaseY = e.Y;
                

                string strName = detailPanel.GetDrawedElementTitle(e.X, e.Y);
                m_movedTreeNode = TreeData.GetTreeNode(strName);

                if (null != m_movedTreeNode)
                {
                    int.TryParse(m_movedTreeNode.X.ToString(), out m_iEleX);
                    int.TryParse(m_movedTreeNode.Y.ToString(), out m_iEleY);
                }

                m_bCanvasMouseDown = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                Console.WriteLine(e.X + ":" + e.Y);
                //contextMenuDraw.Show();
                SplitContainerDetail.Panel1.ContextMenuStrip = contextMenuDrawPanel;

            }
        }

        private void SplitContainerDetail_Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            m_bCanvasMouseDown = false;
            m_movedTreeNode = null;
        }

        private void SplitContainerDetail_Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (true == m_bCanvasMouseDown && null != m_movedTreeNode)
            {

                if (-1 != m_iEleX && -1 != m_iEleY)
                {
                    m_movedTreeNode.X = (m_iEleX + (e.X - m_iCanvasBaseX)).ToString();
                    m_movedTreeNode.Y = (m_iEleY + (e.Y - m_iCanvasBaseY)).ToString();

                    Console.WriteLine("{0},{1}", m_movedTreeNode.X, m_movedTreeNode.Y);
                    detailPanel.DrawNode(m_crtNode);
                    //detailPanel.DrawSelectCursor((m_iEleX + (e.X - m_iCanvasBaseX)), (m_iEleY + (e.Y - m_iCanvasBaseY)));
                }
            }
        }

        private void SplitContainerDetail_Panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LogMgt.Debug("SplitContainerDetail_Panel1_MouseDoubleClick", "");

                string strName = detailPanel.GetDrawedElementTitle(e.X, e.Y);
                m_movedTreeNode = TreeData.GetTreeNode(strName);

                if (null == m_movedTreeNode)
                {
                    LogMgt.Debug("SplitContainerDetail_Panel1_MouseDoubleClick", "m_movedTreeNode is null");
                    return;
                }

                TreeNode tn = ProjMgt.GetInstance().GetTreeNodeByTitle(null, m_movedTreeNode.Title);

                LogMgt.Debug("SplitContainerDetail_Panel1_MouseDoubleClick", strName);

                if (null != tn)
                {
                    onShowTreeNode(tn);
                }
            }
        }

        private void previewSubMenuItem_Click(object sender, EventArgs e)
        {
            if (null != m_crtNode)
            {
                LogMgt.Debug("SplitContainerDetail_Panel1_MouseDoubleClick", "Show preview panel.");

                PreviewPanel panel = new PreviewPanel();

                DrawMgt dm = new DrawMgt(panel);

                panel.Show();

                /* Need show panel first than draw graphic */
                dm.DrawNode(m_crtNode);
            }
            else 
            {
                LogMgt.Debug("SplitContainerDetail_Panel1_MouseDoubleClick", "Current tree node is null.");
            }
        }

        /*************************************************************************/

        private void onShowTreeNode(TreeNode node)
        {
            LogMgt.Debug("onShowTreeNode", "");

            if (null != detailPanel)
            {
                detailPanel.clearAll();
            }

            detailPanel = new DrawMgt(SplitContainerDetail.Panel1);
            detailPanel.DrawNode(node);
            m_crtNode = node;

            SplitContainerDetail.Panel2.Controls.Clear();

            selectTreeNode = TreeData.GetTreeNode(node.Text);

            tp = new TreeProperty(m_crtNode);
            tp.TopLevel = false;
            tp.Width = SplitContainerDetail.Panel2.Width - 16;

            SplitContainerDetail.Panel2.Controls.Add(tp);
            tp.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;

            tp.Show();
        }
    }
}
