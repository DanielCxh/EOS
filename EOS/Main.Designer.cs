namespace EOS
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TitleBG = new System.Windows.Forms.PictureBox();
            this.CloseImg = new System.Windows.Forms.PictureBox();
            this.ResizeImg = new System.Windows.Forms.PictureBox();
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stringEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previewSubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.docSubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutSubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusBarInfo = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SplitContainerFull = new System.Windows.Forms.SplitContainer();
            this.ProjectTree = new System.Windows.Forms.TreeView();
            this.ProjTreeImgList = new System.Windows.Forms.ImageList(this.components);
            this.SplitContainerDetail = new System.Windows.Forms.SplitContainer();
            this.FreePanel = new System.Windows.Forms.Panel();
            this.contextMenuDrawPanel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TitleBG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResizeImg)).BeginInit();
            this.MenuBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerFull)).BeginInit();
            this.SplitContainerFull.Panel1.SuspendLayout();
            this.SplitContainerFull.Panel2.SuspendLayout();
            this.SplitContainerFull.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerDetail)).BeginInit();
            this.SplitContainerDetail.SuspendLayout();
            this.contextMenuDrawPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TitleBG
            // 
            this.TitleBG.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TitleBG.BackColor = System.Drawing.Color.DarkCyan;
            this.TitleBG.Location = new System.Drawing.Point(0, 0);
            this.TitleBG.Name = "TitleBG";
            this.TitleBG.Size = new System.Drawing.Size(816, 25);
            this.TitleBG.TabIndex = 0;
            this.TitleBG.TabStop = false;
            this.TitleBG.DragDrop += new System.Windows.Forms.DragEventHandler(this.TitleBG_DragDrop);
            this.TitleBG.DoubleClick += new System.EventHandler(this.TitleBG_DoubleClick);
            this.TitleBG.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBG_MouseDown);
            this.TitleBG.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TitleBG_MouseMove);
            this.TitleBG.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TitleBG_MouseUp);
            // 
            // CloseImg
            // 
            this.CloseImg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseImg.BackColor = System.Drawing.Color.DarkRed;
            this.CloseImg.Location = new System.Drawing.Point(771, 4);
            this.CloseImg.Name = "CloseImg";
            this.CloseImg.Size = new System.Drawing.Size(18, 18);
            this.CloseImg.TabIndex = 2;
            this.CloseImg.TabStop = false;
            this.CloseImg.Click += new System.EventHandler(this.CloseImg_Click);
            this.CloseImg.MouseEnter += new System.EventHandler(this.CloseImg_MouseEnter);
            this.CloseImg.MouseLeave += new System.EventHandler(this.CloseImg_MouseLeave);
            // 
            // ResizeImg
            // 
            this.ResizeImg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ResizeImg.BackColor = System.Drawing.Color.DarkBlue;
            this.ResizeImg.Location = new System.Drawing.Point(746, 4);
            this.ResizeImg.Name = "ResizeImg";
            this.ResizeImg.Size = new System.Drawing.Size(18, 18);
            this.ResizeImg.TabIndex = 3;
            this.ResizeImg.TabStop = false;
            this.ResizeImg.Click += new System.EventHandler(this.ResizeImg_Click);
            this.ResizeImg.MouseEnter += new System.EventHandler(this.ResizeImg_MouseEnter);
            this.ResizeImg.MouseLeave += new System.EventHandler(this.ResizeImg_MouseLeave);
            // 
            // MenuBar
            // 
            this.MenuBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MenuBar.AutoSize = false;
            this.MenuBar.BackColor = System.Drawing.Color.DimGray;
            this.MenuBar.Dock = System.Windows.Forms.DockStyle.None;
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.editMenuItem,
            this.toolsMenuItem,
            this.viewMenuItem,
            this.helpMenuItem});
            this.MenuBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MenuBar.Location = new System.Drawing.Point(0, 25);
            this.MenuBar.MinimumSize = new System.Drawing.Size(800, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(816, 24);
            this.MenuBar.TabIndex = 4;
            this.MenuBar.Text = "File";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenProject,
            this.newFileToolStripMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(39, 20);
            this.fileMenuItem.Text = "File";
            // 
            // OpenProject
            // 
            this.OpenProject.Name = "OpenProject";
            this.OpenProject.Size = new System.Drawing.Size(152, 22);
            this.OpenProject.Text = "Open Project";
            this.OpenProject.Click += new System.EventHandler(this.OpenProject_Click);
            // 
            // newFileToolStripMenuItem
            // 
            this.newFileToolStripMenuItem.Name = "newFileToolStripMenuItem";
            this.newFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newFileToolStripMenuItem.Text = "New File";
            // 
            // editMenuItem
            // 
            this.editMenuItem.Name = "editMenuItem";
            this.editMenuItem.Size = new System.Drawing.Size(42, 20);
            this.editMenuItem.Text = "Edit";
            // 
            // toolsMenuItem
            // 
            this.toolsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stringEditToolStripMenuItem});
            this.toolsMenuItem.Name = "toolsMenuItem";
            this.toolsMenuItem.Size = new System.Drawing.Size(52, 20);
            this.toolsMenuItem.Text = "Tools";
            // 
            // stringEditToolStripMenuItem
            // 
            this.stringEditToolStripMenuItem.Name = "stringEditToolStripMenuItem";
            this.stringEditToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.stringEditToolStripMenuItem.Text = "String Edit";
            // 
            // viewMenuItem
            // 
            this.viewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.previewSubMenuItem});
            this.viewMenuItem.Name = "viewMenuItem";
            this.viewMenuItem.Size = new System.Drawing.Size(47, 20);
            this.viewMenuItem.Text = "View";
            // 
            // previewSubMenuItem
            // 
            this.previewSubMenuItem.Name = "previewSubMenuItem";
            this.previewSubMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.previewSubMenuItem.Size = new System.Drawing.Size(166, 22);
            this.previewSubMenuItem.Text = "Preview";
            this.previewSubMenuItem.Click += new System.EventHandler(this.previewSubMenuItem_Click);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.docSubMenuItem,
            this.toolStripSeparator1,
            this.aboutSubMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(47, 20);
            this.helpMenuItem.Text = "Help";
            // 
            // docSubMenuItem
            // 
            this.docSubMenuItem.Name = "docSubMenuItem";
            this.docSubMenuItem.Size = new System.Drawing.Size(135, 22);
            this.docSubMenuItem.Text = "Document";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(132, 6);
            // 
            // aboutSubMenuItem
            // 
            this.aboutSubMenuItem.Name = "aboutSubMenuItem";
            this.aboutSubMenuItem.Size = new System.Drawing.Size(135, 22);
            this.aboutSubMenuItem.Text = "About";
            // 
            // StatusBarInfo
            // 
            this.StatusBarInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.StatusBarInfo.ForeColor = System.Drawing.Color.White;
            this.StatusBarInfo.Location = new System.Drawing.Point(10, 461);
            this.StatusBarInfo.Margin = new System.Windows.Forms.Padding(0, 3, 10, 0);
            this.StatusBarInfo.Name = "StatusBarInfo";
            this.StatusBarInfo.Size = new System.Drawing.Size(780, 12);
            this.StatusBarInfo.TabIndex = 6;
            this.StatusBarInfo.Text = "StatusBar";
            // 
            // SplitContainerFull
            // 
            this.SplitContainerFull.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SplitContainerFull.Cursor = System.Windows.Forms.Cursors.Default;
            this.SplitContainerFull.Location = new System.Drawing.Point(0, 50);
            this.SplitContainerFull.Margin = new System.Windows.Forms.Padding(0);
            this.SplitContainerFull.Name = "SplitContainerFull";
            // 
            // SplitContainerFull.Panel1
            // 
            this.SplitContainerFull.Panel1.BackColor = System.Drawing.Color.DimGray;
            this.SplitContainerFull.Panel1.Controls.Add(this.ProjectTree);
            // 
            // SplitContainerFull.Panel2
            // 
            this.SplitContainerFull.Panel2.BackColor = System.Drawing.Color.Black;
            this.SplitContainerFull.Panel2.Controls.Add(this.SplitContainerDetail);
            this.SplitContainerFull.Size = new System.Drawing.Size(800, 400);
            this.SplitContainerFull.SplitterDistance = 266;
            this.SplitContainerFull.TabIndex = 7;
            this.SplitContainerFull.SplitterMoving += new System.Windows.Forms.SplitterCancelEventHandler(this.SplitContainer_SplitterMoving);
            this.SplitContainerFull.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer_SplitterMoved);
            // 
            // ProjectTree
            // 
            this.ProjectTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProjectTree.BackColor = System.Drawing.Color.DimGray;
            this.ProjectTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProjectTree.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ProjectTree.ImageIndex = 0;
            this.ProjectTree.ImageList = this.ProjTreeImgList;
            this.ProjectTree.Location = new System.Drawing.Point(0, 0);
            this.ProjectTree.Margin = new System.Windows.Forms.Padding(0);
            this.ProjectTree.Name = "ProjectTree";
            this.ProjectTree.SelectedImageIndex = 0;
            this.ProjectTree.Size = new System.Drawing.Size(266, 400);
            this.ProjectTree.TabIndex = 0;
            this.ProjectTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ProjectTree_NodeMouseDoubleClick);
            // 
            // ProjTreeImgList
            // 
            this.ProjTreeImgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ProjTreeImgList.ImageSize = new System.Drawing.Size(16, 16);
            this.ProjTreeImgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // SplitContainerDetail
            // 
            this.SplitContainerDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SplitContainerDetail.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.SplitContainerDetail.Location = new System.Drawing.Point(0, 0);
            this.SplitContainerDetail.Name = "SplitContainerDetail";
            // 
            // SplitContainerDetail.Panel1
            // 
            this.SplitContainerDetail.Panel1.BackColor = System.Drawing.Color.DimGray;
            this.SplitContainerDetail.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.SplitContainerDetail_Panel1_Paint);
            this.SplitContainerDetail.Panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SplitContainerDetail_Panel1_MouseClick);
            this.SplitContainerDetail.Panel1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SplitContainerDetail_Panel1_MouseDoubleClick);
            this.SplitContainerDetail.Panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SplitContainerDetail_Panel1_MouseDown);
            this.SplitContainerDetail.Panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SplitContainerDetail_Panel1_MouseMove);
            this.SplitContainerDetail.Panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SplitContainerDetail_Panel1_MouseUp);
            // 
            // SplitContainerDetail.Panel2
            // 
            this.SplitContainerDetail.Panel2.AutoScroll = true;
            this.SplitContainerDetail.Panel2.BackColor = System.Drawing.Color.Maroon;
            this.SplitContainerDetail.Panel2.SizeChanged += new System.EventHandler(this.SplitContainerDetail_Panel2_SizeChanged);
            this.SplitContainerDetail.Size = new System.Drawing.Size(530, 400);
            this.SplitContainerDetail.SplitterDistance = 300;
            this.SplitContainerDetail.TabIndex = 0;
            // 
            // FreePanel
            // 
            this.FreePanel.Location = new System.Drawing.Point(0, 0);
            this.FreePanel.Name = "FreePanel";
            this.FreePanel.Size = new System.Drawing.Size(10, 10);
            this.FreePanel.TabIndex = 0;
            // 
            // contextMenuDrawPanel
            // 
            this.contextMenuDrawPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpen});
            this.contextMenuDrawPanel.Name = "contextMenuDraw";
            this.contextMenuDrawPanel.Size = new System.Drawing.Size(194, 26);
            // 
            // toolStripMenuItemOpen
            // 
            this.toolStripMenuItemOpen.Name = "toolStripMenuItemOpen";
            this.toolStripMenuItemOpen.Size = new System.Drawing.Size(193, 22);
            this.toolStripMenuItemOpen.Text = "Open Selected WGT";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.FreePanel);
            this.Controls.Add(this.SplitContainerFull);
            this.Controls.Add(this.StatusBarInfo);
            this.Controls.Add(this.ResizeImg);
            this.Controls.Add(this.CloseImg);
            this.Controls.Add(this.MenuBar);
            this.Controls.Add(this.TitleBG);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.MenuBar;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Main";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.DoubleClick += new System.EventHandler(this.Main_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Main_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Main_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Main_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.TitleBG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResizeImg)).EndInit();
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.SplitContainerFull.Panel1.ResumeLayout(false);
            this.SplitContainerFull.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerFull)).EndInit();
            this.SplitContainerFull.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerDetail)).EndInit();
            this.SplitContainerDetail.ResumeLayout(false);
            this.contextMenuDrawPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox TitleBG;
        private System.Windows.Forms.PictureBox CloseImg;
        private System.Windows.Forms.PictureBox ResizeImg;
        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenProject;
        private System.Windows.Forms.ToolStripMenuItem newFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stringEditToolStripMenuItem;
        private System.Windows.Forms.Label StatusBarInfo;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer SplitContainerFull;
        private System.Windows.Forms.Panel FreePanel;
        private System.Windows.Forms.TreeView ProjectTree;
        private System.Windows.Forms.ImageList ProjTreeImgList;
        private System.Windows.Forms.SplitContainer SplitContainerDetail;
        private System.Windows.Forms.ToolStripMenuItem viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previewSubMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutSubMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem docSubMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuDrawPanel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpen;
    }
}