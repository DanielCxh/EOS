using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections;

namespace EOS
{
    public partial class ResEdit : Form
    {
        private string strEditType = "";
        private string strFilePath = "";

        private string m_strCrtEditVal = "";

        private const short ARGB_IMIN_VALUE = 0;
        private const short ARGB_MAX_VALUE = 255;

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
            ResFile rf = ResData.GetResFileByTag(strFilePath);

            if (null == rf)
            {
                return;
            }

            ArrayList colorData = rf.GetColorList();
           
            // Init head
            DataGridViewTextBoxColumn colorNameColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn formatColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn valueAColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn valueRColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn valueGColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn valueBColumn = new DataGridViewTextBoxColumn();

            colorNameColumn.HeaderText = "Color ID";
            formatColumn.HeaderText = "Format";
            valueAColumn.HeaderText = "A(alpha)";
            valueRColumn.HeaderText = "R(red)";
            valueGColumn.HeaderText = "G(green)";
            valueBColumn.HeaderText = "B(blue)";

            ColorTable.Columns.Add(colorNameColumn);
            ColorTable.Columns.Add(formatColumn);
            ColorTable.Columns.Add(valueAColumn);
            ColorTable.Columns.Add(valueRColumn);
            ColorTable.Columns.Add(valueGColumn);
            ColorTable.Columns.Add(valueBColumn);
            
            // Init data
            foreach (ColorJson cj in colorData)
            {
                DataGridViewRow row = new DataGridViewRow();

                row.CreateCells(ColorTable);

                row.Cells[0].Value = cj.Title;
                row.Cells[1].Value = cj.Format;

                row.Cells[2].Value = Int16.Parse(cj.Value[0]);
                row.Cells[3].Value = Int16.Parse(cj.Value[1]);
                row.Cells[4].Value = Int16.Parse(cj.Value[2]);
                row.Cells[5].Value = Int16.Parse(cj.Value[3]);

                // Set background color of cell
                row.Cells[2].Style.BackColor = Color.FromArgb(Int16.Parse(cj.Value[0])
                    , Int16.Parse(cj.Value[1])
                    , Int16.Parse(cj.Value[2])
                    , Int16.Parse(cj.Value[3]));

                row.Cells[3].Style.BackColor = row.Cells[2].Style.BackColor;
                row.Cells[4].Style.BackColor = row.Cells[2].Style.BackColor;
                row.Cells[5].Style.BackColor = row.Cells[2].Style.BackColor;

                // Set font color of cell
                row.Cells[2].Style.ForeColor = Color.FromArgb(255
                    , 255 - Int16.Parse(cj.Value[1])
                    , 255 - Int16.Parse(cj.Value[2])
                    , 255 - Int16.Parse(cj.Value[3]));

                row.Cells[3].Style.ForeColor = row.Cells[2].Style.ForeColor;
                row.Cells[4].Style.ForeColor = row.Cells[2].Style.ForeColor;
                row.Cells[5].Style.ForeColor = row.Cells[2].Style.ForeColor;

                ColorTable.Rows.Add(row);
            }
        }

        private void ColorTable_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            
        }

        private void ColorTable_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            m_strCrtEditVal = ColorTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        private void ColorTable_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 2)
            {
                string strVal = ColorTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().TrimStart('0');

                short rst = 0;
                if (Int16.TryParse(strVal, out rst))
                {
                    if (ARGB_IMIN_VALUE > rst || ARGB_MAX_VALUE < rst)
                    {
                        ColorTable.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Range error";
                        ColorTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = m_strCrtEditVal;
                    }
                    else
                    {
                        ColorTable.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "";
                        ColorTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = strVal;
                    }
                }
                else
                {
                    ColorTable.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Invalid type";
                    ColorTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = m_strCrtEditVal;
                }
            }
        }
    }
}
