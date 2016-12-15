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
    public partial class ResEdit : Form
    {
        private string strEditType = "";
        private string strFilePath = "";

        public ResEdit()
        {
            InitializeComponent();
        }

        public ResEdit(string title)
        {
            InitializeComponent();
            strFilePath = title;

            ResEditTabControl.SelectedIndex = 0;

            updateTitle();
            initTable();
        }

        private void ResEditTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateTitle();

        }

        private void updateTitle()
        {
            strEditType = "[" + ResEditTabControl.SelectedTab.Text + "]";

            this.Text = strFilePath + strEditType;
        }

        private void initTable()
        {
            // Init Color Table
            initColorTable();

            // Init String Table

            // Init Font Table
        }

        private void initColorTable()
        {
            ColorData cd = ResData.GetResColorDataByTag(strFilePath);

            if (null == cd)
            {
                return;
            }
           

            // Init head
            DataGridViewTextBoxColumn colorNameColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn formatColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn valueColumn = new DataGridViewTextBoxColumn();

            colorNameColumn.HeaderText = "Color ID";
            formatColumn.HeaderText = "Format";
            valueColumn.HeaderText = "Value";

            ColorTable.Columns.Add(colorNameColumn);
            ColorTable.Columns.Add(formatColumn);
            ColorTable.Columns.Add(valueColumn);
            
            // Init data
            foreach (ColorJson cj in cd.GetData())
            {
                DataGridViewRow row = new DataGridViewRow();

                row.CreateCells(ColorTable);

                row.Cells[0].Value = cj.Title;
                row.Cells[1].Value = cj.Format;

                string val = String.Format("[{0:d}, {1:d}, {2:d}, {3:d}]"
                    , Int16.Parse(cj.Value[0])
                    , Int16.Parse(cj.Value[1])
                    , Int16.Parse(cj.Value[2])
                    , Int16.Parse(cj.Value[3]));

                row.Cells[2].Value = val;
                row.Cells[2].Style.BackColor = Color.FromArgb(Int16.Parse(cj.Value[0])
                    , Int16.Parse(cj.Value[1])
                    , Int16.Parse(cj.Value[2])
                    , Int16.Parse(cj.Value[3]));

                row.Cells[2].Style.ForeColor = Color.FromArgb(255
                    , 255 - Int16.Parse(cj.Value[1])
                    , 255 - Int16.Parse(cj.Value[2])
                    , 255 - Int16.Parse(cj.Value[3]));

                ColorTable.Rows.Add(row);
            }
        }
    }
}
