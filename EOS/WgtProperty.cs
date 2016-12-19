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
    public partial class WgtProperty : Form
    {
        public const int WGT_TYPE_MAX = 6;

        public enum WgtTabType
        {
            BITMAP_IMAGE = 0,
            SOLD_IMAGE = 1,
            TEXT_BOX = 2,
            PUSH_BUTTON = 3,
            SCROLL_BAR = 4,
            SCHEDULE_BAR = 5
        }
        public WgtProperty()
        {
            InitializeComponent();

            wgtTypeTabControl.Width = this.Width + 10;

            showTabPage(WgtTabType.BITMAP_IMAGE);
        }

        private bool showTabPage(WgtTabType eType)
        {
            bool bRst = false;

            bitmapImgTabPage.Parent = null;
            soldImgTabPage.Parent = null;
            textBoxTabPage.Parent = null;
            pushBtnTabPag.Parent = null;
            scrollBarTabPage.Parent = null;
            scheduleBarTabPage.Parent = null;

            if (WgtTabType.BITMAP_IMAGE == eType)
            {
                bitmapImgTabPage.Parent = wgtTypeTabControl;
            }

            return bRst;
        }

        public void SetWgtNodePath(string strPath)
        {
 
        }

        public void SetWgtNode(TreeNode node)
        {
            if (null == node)
            {
                return;
            }

            BitmapImgJson bij = (BitmapImgJson)CfgWgt.GetWgtNodeContent(node);

            if (null != bij)
            {
                string strTitle = bij.Title;
                string strFileName = bij.FileName;
                string strWidth = bij.Width;
                string strHeight = bij.Height;
                string strFormat = bij.Format;
                string strCompress = bij.Compress;
                string strStaticCompiled = bij.StaticCompiled;
                string strStorage = bij.Storage;
                string strOutlineImage = bij.OutlineImage;
                string strOutlineColor = bij.OutlineColor;
                string strOutlineWidth = bij.OutlineWidth;

                BitmapImgTextBox.Text = strTitle;
                FileNameTextBox.Text = strFileName;
                widthTextBox.Text = strWidth;
                heightTextBox.Text = strHeight;
                formatTextBox.Text = strFormat;

                for (int i = 0; i < compressComboBox.Items.Count; i++ )
                {
                    if (0 == compressComboBox.Items[i].ToString().CompareTo(strCompress))
                    {
                        compressComboBox.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        /*
        private void WgtTypeTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            SolidBrush tabColor = new SolidBrush(Color.DimGray);
            SolidBrush txtColor = new SolidBrush(Color.Black);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            Rectangle rec = wgtTypeTabControl.GetTabRect(0);
            e.Graphics.FillRectangle(tabColor, rec);

            e.Graphics.DrawString(wgtTypeTabControl.TabPages[0].Text, new Font("宋体", 10), txtColor, rec, sf);
        }
         * */
    }
}
