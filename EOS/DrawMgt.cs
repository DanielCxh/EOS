using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace EOS
{
    class DrawMgt
    {
        public static Graphics m_graphics = null;
        private static Panel m_panel = null;

        public DrawMgt()
        {
 
        }

        public static void SetCanvas(Panel panel)
        {
            if (null != panel)
            {
                m_panel = panel;
                m_graphics = panel.CreateGraphics();
            }
        }

        public static void DrawNode(TreeNode node)
        {
            if (null == m_graphics)
            {
                return;
            }

            m_graphics.Clear(Color.White);

            if (ProjMgt.NodeDetailType.NT_WGT_BITMAP_IMG == ProjMgt.GetNoteDetailType(node))
            {
                // Wgt
                DrawWgtNode(node);
            }

            // Tree

            // Picture
            //DrawPictureNode(node);
        }

        public static void DrawWgtNode(TreeNode node)
        {
            // file name
            BitmapImgJson obj = (BitmapImgJson)CfgWgt.GetWgtNodeContent(node);

            if (null != obj)
            {
                string strPath = ProjMgt.GetInstance().GetProjectResLoc() + "\\" + obj.FileName;
                DrawPicture(strPath);
            }
        }

        private void drawTreeNode()
        {
 
        }

        public static bool DrawPictureNode(TreeNode node)
        {
            bool bRst = false;
            if (null == m_graphics || null == m_panel)
            {
                return false;
            }

            m_graphics.Clear(Color.White);

            Image img = Image.FromFile(ProjMgt.GetInstance().GetProjectResLoc() + "\\" + node.FullPath);

            if (null == img)
            {
                return false;
            }

            int locX = (m_panel.Width - img.Width) / 2;
            int locY = (m_panel.Height - img.Height) / 2;

            m_graphics.FillRectangle(new SolidBrush(Color.White), locX, locY, img.Width, img.Height);
            m_graphics.DrawImage(img, locX, locY, img.Width, img.Height);

            m_graphics.Save();

            bRst = true;

            return bRst;
        }

        public static bool DrawPicture(string strFilePath)
        {
            if (Common.IsStrEmpty(strFilePath))
            {
                return false;
            }

            Console.WriteLine(strFilePath);

            Image img = Image.FromFile(strFilePath);

            if (null == img)
            {
                return false;
            }

            int locX = (m_panel.Width - img.Width) / 2;
            int locY = (m_panel.Height - img.Height) / 2;

            int width = img.Width;
            int height = img.Height;

            m_graphics.FillRectangle(new SolidBrush(Color.White), locX, locY, width, height);
            m_graphics.DrawImage(img, locX, locY, img.Width, img.Height);

            m_graphics.Save();

            return true;
        }
    }
}
