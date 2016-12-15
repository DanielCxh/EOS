namespace EOS
{
    partial class ResEdit
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
            this.ResEditTabControl = new System.Windows.Forms.TabControl();
            this.ColorEditPage = new System.Windows.Forms.TabPage();
            this.ColorTable = new System.Windows.Forms.DataGridView();
            this.StringEditPage = new System.Windows.Forms.TabPage();
            this.FontEditPage = new System.Windows.Forms.TabPage();
            this.ResEditTabControl.SuspendLayout();
            this.ColorEditPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColorTable)).BeginInit();
            this.SuspendLayout();
            // 
            // ResEditTabControl
            // 
            this.ResEditTabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.ResEditTabControl.Controls.Add(this.ColorEditPage);
            this.ResEditTabControl.Controls.Add(this.StringEditPage);
            this.ResEditTabControl.Controls.Add(this.FontEditPage);
            this.ResEditTabControl.Location = new System.Drawing.Point(12, 12);
            this.ResEditTabControl.Multiline = true;
            this.ResEditTabControl.Name = "ResEditTabControl";
            this.ResEditTabControl.SelectedIndex = 0;
            this.ResEditTabControl.Size = new System.Drawing.Size(560, 238);
            this.ResEditTabControl.TabIndex = 0;
            this.ResEditTabControl.SelectedIndexChanged += new System.EventHandler(this.ResEditTabControl_SelectedIndexChanged);
            // 
            // ColorEditPage
            // 
            this.ColorEditPage.Controls.Add(this.ColorTable);
            this.ColorEditPage.Location = new System.Drawing.Point(4, 4);
            this.ColorEditPage.Name = "ColorEditPage";
            this.ColorEditPage.Padding = new System.Windows.Forms.Padding(3);
            this.ColorEditPage.Size = new System.Drawing.Size(552, 212);
            this.ColorEditPage.TabIndex = 0;
            this.ColorEditPage.Text = "Color";
            this.ColorEditPage.UseVisualStyleBackColor = true;
            // 
            // ColorTable
            // 
            this.ColorTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ColorTable.Location = new System.Drawing.Point(6, 6);
            this.ColorTable.Name = "ColorTable";
            this.ColorTable.RowTemplate.Height = 23;
            this.ColorTable.Size = new System.Drawing.Size(540, 200);
            this.ColorTable.TabIndex = 0;
            // 
            // StringEditPage
            // 
            this.StringEditPage.Location = new System.Drawing.Point(4, 4);
            this.StringEditPage.Name = "StringEditPage";
            this.StringEditPage.Padding = new System.Windows.Forms.Padding(3);
            this.StringEditPage.Size = new System.Drawing.Size(552, 212);
            this.StringEditPage.TabIndex = 1;
            this.StringEditPage.Text = "String";
            this.StringEditPage.UseVisualStyleBackColor = true;
            // 
            // FontEditPage
            // 
            this.FontEditPage.Location = new System.Drawing.Point(4, 4);
            this.FontEditPage.Name = "FontEditPage";
            this.FontEditPage.Padding = new System.Windows.Forms.Padding(3);
            this.FontEditPage.Size = new System.Drawing.Size(552, 212);
            this.FontEditPage.TabIndex = 2;
            this.FontEditPage.Text = "Font";
            this.FontEditPage.UseVisualStyleBackColor = true;
            // 
            // ResEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 262);
            this.Controls.Add(this.ResEditTabControl);
            this.Name = "ResEdit";
            this.Text = "ResEdit";
            this.ResEditTabControl.ResumeLayout(false);
            this.ColorEditPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ColorTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl ResEditTabControl;
        private System.Windows.Forms.TabPage ColorEditPage;
        private System.Windows.Forms.TabPage StringEditPage;
        private System.Windows.Forms.TabPage FontEditPage;
        private System.Windows.Forms.DataGridView ColorTable;


    }
}