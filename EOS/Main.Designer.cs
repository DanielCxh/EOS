﻿namespace EOS
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
            this.File = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenProject = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stringEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusBarInfo = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.ProjectTree = new System.Windows.Forms.TreeView();
            this.FreePanel = new System.Windows.Forms.Panel();
            this.ProjTreeImgList = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TitleBG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseImg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResizeImg)).BeginInit();
            this.MenuBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.SuspendLayout();
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
            this.File,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.MenuBar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MenuBar.Location = new System.Drawing.Point(0, 25);
            this.MenuBar.MinimumSize = new System.Drawing.Size(800, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(816, 24);
            this.MenuBar.TabIndex = 4;
            this.MenuBar.Text = "File";
            // 
            // File
            // 
            this.File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenProject,
            this.newFileToolStripMenuItem});
            this.File.Name = "File";
            this.File.Size = new System.Drawing.Size(39, 20);
            this.File.Text = "File";
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
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stringEditToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // stringEditToolStripMenuItem
            // 
            this.stringEditToolStripMenuItem.Name = "stringEditToolStripMenuItem";
            this.stringEditToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.stringEditToolStripMenuItem.Text = "String Edit";
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
            // SplitContainer
            // 
            this.SplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SplitContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this.SplitContainer.Location = new System.Drawing.Point(0, 50);
            this.SplitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.BackColor = System.Drawing.Color.Gray;
            this.SplitContainer.Panel1.Controls.Add(this.ProjectTree);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.BackColor = System.Drawing.Color.DimGray;
            this.SplitContainer.Size = new System.Drawing.Size(800, 400);
            this.SplitContainer.SplitterDistance = 266;
            this.SplitContainer.TabIndex = 7;
            this.SplitContainer.SplitterMoving += new System.Windows.Forms.SplitterCancelEventHandler(this.SplitContainer_SplitterMoving);
            this.SplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer_SplitterMoved);
            // 
            // ProjectTree
            // 
            this.ProjectTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProjectTree.BackColor = System.Drawing.Color.DimGray;
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
            // FreePanel
            // 
            this.FreePanel.Location = new System.Drawing.Point(0, 0);
            this.FreePanel.Name = "FreePanel";
            this.FreePanel.Size = new System.Drawing.Size(10, 10);
            this.FreePanel.TabIndex = 0;
            // 
            // ProjTreeImgList
            // 
            this.ProjTreeImgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ProjTreeImgList.ImageSize = new System.Drawing.Size(16, 16);
            this.ProjTreeImgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.FreePanel);
            this.Controls.Add(this.SplitContainer);
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
            ((System.ComponentModel.ISupportInitialize)(this.TitleBG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseImg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ResizeImg)).EndInit();
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.SplitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox TitleBG;
        private System.Windows.Forms.PictureBox CloseImg;
        private System.Windows.Forms.PictureBox ResizeImg;
        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem File;
        private System.Windows.Forms.ToolStripMenuItem OpenProject;
        private System.Windows.Forms.ToolStripMenuItem newFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stringEditToolStripMenuItem;
        private System.Windows.Forms.Label StatusBarInfo;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer SplitContainer;
        private System.Windows.Forms.Panel FreePanel;
        private System.Windows.Forms.TreeView ProjectTree;
        private System.Windows.Forms.ImageList ProjTreeImgList;
    }
}