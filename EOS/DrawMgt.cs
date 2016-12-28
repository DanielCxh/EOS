using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace EOS
{
    /* Used to save one drawed element */
    class DrawElement
    {
        public string Title { get; set; }
        
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public DrawElement(string strTitle, int iLocX, int iLocY, int iWidth, int iHeight)
        {
            Title = strTitle;
            X = iLocX;
            Y = iLocY;
            Width = iWidth;
            Height = iHeight;
        }

        public DrawElement(string strTitle, Point point, Size size)
        {
            Title = strTitle;
            X = point.X;
            Y = point.Y;
            Width = size.Width;
            Height = size.Height;
        }

        public bool OnElement(int iLocX, int iLocY)
        {
            bool bRst = false;

            if (iLocX > X && iLocX < (X + Width)
                && iLocY > Y && iLocY < (Y + Height))
            {
                bRst = true;
            }

            return bRst;
        }

        public string GetElementTitle(int iLocX, int iLocY)
        {
            if (OnElement(iLocX, iLocY))
            {
                return Title;
            }
            else
            {
                return null;
            }
        }
    }

    class DrawMgt
    {
        private Graphics m_realGraphics = null;
        private Graphics m_graphics = null;
        private Size m_panelSize = new Size(0, 0);

        private Bitmap bmp = null;

        /* Used to save all drowed element */
        private static ArrayList m_elementList = new ArrayList();
        private TreeNode currentNode = null;

        Color selectCursorColor = Color.Red;

        public DrawMgt(Panel panel)
        {
            if (null != panel)
            {
                m_panelSize = new Size(panel.Width, panel.Height);
                m_realGraphics = panel.CreateGraphics();
                bmp = new Bitmap(m_panelSize.Width, m_panelSize.Height);
                m_graphics = Graphics.FromImage(bmp);
            }
        }

        public DrawMgt(Form form)
        {
            if (null != form)
            {
                m_panelSize = new Size(form.Width, form.Height);
                m_realGraphics = form.CreateGraphics();
                bmp = new Bitmap(m_panelSize.Width, m_panelSize.Height);
                m_graphics = Graphics.FromImage(bmp);
            }
        }

        public void SetCanvas(Panel panel)
        {
            if (null != panel)
            {
                m_panelSize = new Size(panel.Width, panel.Height);
                m_realGraphics = panel.CreateGraphics();
                bmp = new Bitmap(m_panelSize.Width, m_panelSize.Height);
                m_graphics = Graphics.FromImage(bmp);
            }
        }

        public void SetCanvas(Form form)
        {
            if (null != form)
            {
                m_panelSize = new Size(form.Width, form.Height);
                m_realGraphics = form.CreateGraphics();
                bmp = new Bitmap(m_panelSize.Width, m_panelSize.Height);
                m_graphics = Graphics.FromImage(bmp);
            }
        }

        public void DrawNode(TreeNode node)
        {
            LogMgt.Debug("DrawMgt.DrawNode", "");

            if (null == m_graphics || null == bmp || null == m_realGraphics)
            {
                LogMgt.Debug("DrawMgt.DrawNode", "Not null check");
                return;
            }

            clearCanvas();
            currentNode = node;

            // Wgt
            if (ProjMgt.NodeDetailType.NT_WGT_BITMAP_IMG == ProjMgt.GetNoteDetailType(node))
            {
                DrawWgtNode(node);
            }
            // Tree
            else if (ProjMgt.NodeDetailType.NT_TREE_ITEM == ProjMgt.GetNoteDetailType(node))
            {
                DrawTree(node);
            }
            // Picture
            else if (ProjMgt.NodeDetailType.NT_PICTURE_PNG == ProjMgt.GetNoteDetailType(node)
                || ProjMgt.NodeDetailType.NT_PICTURE_JPG == ProjMgt.GetNoteDetailType(node))
            {
                DrawPictureNode(node);
            }

            if (null != m_realGraphics && null != bmp)
            {
                m_realGraphics.DrawImage(bmp, 0, 0);
            }
        }

        public void DrawWgtNode(TreeNode node)
        {
            // file name
            /*
            if (ProjMgt.NodeDetailType.NT_WGT_BITMAP_IMG == ProjMgt.GetNoteDetailType(node))
            {
                BitmapImgJson obj = (BitmapImgJson)CfgWgt.GetWgtNodeContent(node);
                DrawWgtBitmapImg(obj);
            }
            else if (ProjMgt.NodeDetailType.NT_WGT_PUSH_BUTTON == ProjMgt.GetNoteDetailType(node))
            {
                PushButtonJson obj = (PushButtonJson)CfgWgt.GetWgtNodeContent(node);
                DraWgtPushBtn(obj);
            }
             * */
            if (Common.IsStrEmpty(node.Text))
            {
                return;
            }

            DrawWgtItem(node.Text, node.Text);
        }

        public void DrawWgtBitmapImg(BitmapImgJson bij, string treeItemTitle, int iLocX = 0, int iLocY = 0)
        {
            LogMgt.Debug("DrawMgt.DrawWgtBitmapImg", "");

            if (null == bij)
            {
                return;
            }

            string strPath = ProjMgt.GetInstance().GetProjectResLoc() + "\\" + bij.FileName;

            LogMgt.Debug("DrawMgt.DrawWgtBitmapImg", strPath);

            DrawPicture(treeItemTitle, strPath, iLocX, iLocY);
        }

        public static void DraWgtPushBtn(PushButtonJson pbj)
        {
            if (null == pbj)
            {
                return;
            }

            Console.WriteLine(pbj.Title);
        }

        private void drawTreeNode()
        {
 
        }

        public bool DrawPictureNode(TreeNode node)
        {
            bool bRst = false;
            if (null == m_graphics)
            {
                return false;
            }

            Console.WriteLine(node.FullPath);

            m_graphics.Clear(Color.White);

            Image img = Image.FromFile(ProjMgt.GetInstance().GetProjectResLoc() + "\\" + node.FullPath);

            if (null == img)
            {
                return false;
            }

            int locX = (m_panelSize.Width - img.Width) / 2;
            int locY = (m_panelSize.Height - img.Height) / 2;

            m_graphics.FillRectangle(new SolidBrush(Color.White), locX, locY, img.Width, img.Height);
            m_graphics.DrawImage(img, locX, locY, img.Width, img.Height);

            m_graphics.Save();

            bRst = true;

            return bRst;
        }

        public bool DrawPicture(string treeItemTitle, string strFilePath, int iLocX = 0, int iLocY = 0)
        {
            if (Common.IsStrEmpty(strFilePath))
            {
                return false;
            }

            Console.WriteLine("img path: "  + strFilePath);

            Image img = Image.FromFile(strFilePath);

            if (null == img)
            {
                return false;
            }

            int locX = iLocX; // (m_panel.Width - img.Width) / 2;
            int locY = iLocY; // (m_panel.Height - img.Height) / 2;

            int width = img.Width;
            int height = img.Height;

            //m_graphics.FillRectangle(new SolidBrush(Color.White), locX, locY, width, height);
            m_graphics.DrawImage(img, locX, locY, width, height);

            m_graphics.Save();

            /* Save */
            DrawElement de = new DrawElement(treeItemTitle, locX, locY, width, height);
            SaveDrawedElement(de);
            Console.WriteLine("Save : " + treeItemTitle);

            return true;
        }

        public void DrawTree(TreeNode node)
        {
            LogMgt.Debug("DrawMgt.DrawTree", "");

            if (null == node)
            {
                LogMgt.Debug("DrawMgt.DrawTree", "Not null check");
                return;
            }

            string strKey = node.Text.ToString();
            string strTag = ProjMgt.GetInstance().GetNodeFullPath(node.Parent);

            TreeFile tf = TreeData.GetTreeFileByTag(strTag);

            if (null == tf)
            {
                LogMgt.Debug("DrawMgt.DrawTree", "Tree file is null return.");
                return;
            }

            TreeNodeJson tnj = tf.GetTreeNodeByKey(strKey);

            if (null != tnj)
            {
                DrawTreeNode(strKey);
            }
        }

        public void DrawWgtPushButton(PushButtonJson button, string treeItemTitle, int iParLocX = 0, int iParLocY = 0)
        {
            if (null == button)
            {
                return;
            }

            if (null != button.StateNonFocus)
            {
                DrawWgtItem(button.StateNonFocus, treeItemTitle, iParLocX, iParLocY);
            }
        }

        public void DrawWgtTextBox(TextBoxJson textBox, string treeItemTitle, int iParLocX = 0, int iParLocY = 0)
        {
            if (null == textBox)
            {
                return;
            }
            Console.WriteLine("draw text box : " + textBox.Title);

            // Font

            // Alignment

            // Color

            // Background, TEMP
            DrawWgtBitmapImg((BitmapImgJson)WgtData.GetWgtItemOutlineByTitle(textBox.Background).Object, treeItemTitle, iParLocX, iParLocY);

            // Keep last text

            // Show more indicator
        }

        public void DrawWgtItem(string strWgtTitle, string treeItemTitle, int iParLocX = 0, int iParLocY = 0)
        {
            WgtItemOutline item = WgtData.GetWgtItemOutlineByTitle(strWgtTitle);

            if (null != item)
            {
                if (CfgWgt.WgtType.BITMAP_IMG == item.Type)
                {
                    DrawWgtBitmapImg((BitmapImgJson)item.Object, treeItemTitle);
                }
                else if (CfgWgt.WgtType.PUSH_BUTTON == item.Type)
                {
                    DrawWgtPushButton((PushButtonJson)item.Object, treeItemTitle);
                }
                else if (CfgWgt.WgtType.TEXT_BOX == item.Type)
                {
                    DrawWgtTextBox((TextBoxJson)item.Object, treeItemTitle, iParLocX, iParLocY);
                }
                else
                {
                    
                }
            }
        }

        public void DrawTreeNode(string strNodeTitle, int iParentLocX = 0, int iParentLocY = 0)
        {
            LogMgt.Debug("DrawMgt.DrawTreeNode", "");

            TreeNodeJson tnj = TreeData.GetTreeNode(strNodeTitle);

            NodeResData nrd = TreeData.GetNodeResByTitle(strNodeTitle);

            if (null == nrd)
            {
                LogMgt.Debug("DrawMgt.DrawTreeNode", "nrd is null");
                return;
            }

            WgtItemOutline item = WgtData.GetWgtItemOutlineByTitle(nrd.Key);

            if (null != item)
            {
                int iLocX = iParentLocX;
                int iLocY = iParentLocY;

                if (null != tnj)
                {
                    iLocX += Int32.Parse(tnj.X);
                    iLocY += Int32.Parse(tnj.Y);
                }

                if (CfgWgt.WgtType.BITMAP_IMG == item.Type)
                {
                    LogMgt.Debug("DrawMgt.DrawTreeNode", "DrawWgtBitmapImg:START");
                    DrawWgtBitmapImg((BitmapImgJson)item.Object, strNodeTitle, iLocX, iLocY);
                    LogMgt.Debug("DrawMgt.DrawTreeNode", "DrawWgtBitmapImg:END");
                }
                else if (CfgWgt.WgtType.SOLID_IMG == item.Type)
                {
                    Console.WriteLine("sold img");
                }
                else if (CfgWgt.WgtType.TEXT_BOX == item.Type)
                {
                    Console.WriteLine("text box");
                }
                else if (CfgWgt.WgtType.PUSH_BUTTON == item.Type)
                {
                    LogMgt.Debug("DrawMgt.DrawTreeNode", "DrawWgtPushButton:START");
                    DrawWgtPushButton((PushButtonJson)item.Object, strNodeTitle, iLocX, iLocY);
                    LogMgt.Debug("DrawMgt.DrawTreeNode", "DrawWgtPushButton:END");
                }
                else if (CfgWgt.WgtType.SCROLL_BAR == item.Type)
                {
                    Console.WriteLine("scroll bar");
                }
                else if (CfgWgt.WgtType.SCHEDULE_BAR == item.Type)
                {
                    Console.WriteLine("schedule bar");
                }
                else
                {
                    //Console.WriteLine(strNodeTitle);
                }
            }
            else
            {
                LogMgt.Debug("DrawMgt.DrawTreeNode", "Item is null");
            }

            /* Scan children nodes */
            if (null != tnj && 0 < tnj.Children.Length)
            {
                LogMgt.Debug("DrawMgt.DrawTreeNode", "tnj children size = " + tnj.Children.Length);

                foreach (string str in tnj.Children)
                {
                    DrawTreeNode(str);
                }
            }
            else
            {
                LogMgt.Debug("DrawMgt.DrawTreeNode", "tnj no children");
            }
        }

        public static void AnalyzeDrawElements()
        {
            // Tree node


            // Wgt node


            // Picture node


            // .h node
        }

        public void SaveDrawedElement(DrawElement de)
        {
            m_elementList.Add(de);
        }

        public string GetDrawedElementTitle(int iX, int iY)
        {
            string strTitle = null;
            int iWidth = 0;
            int iHeight = 0;

            DrawElement tmpDe = null;

            if (null == m_elementList)
            {
                return null;
            }

            foreach (DrawElement de in m_elementList)
            {
                if (de.OnElement(iX, iY))
                {
                    if ( 0 == iWidth && 0 == iHeight)
                    {
                        iWidth = de.Width;
                        iHeight = de.Height;
                        tmpDe = de;
                    }
                    else if (iWidth >= de.Width && iHeight >= de.Height)
                    {
                        iWidth = de.Width;
                        iHeight = de.Height;
                        tmpDe = de;
                    }
                }
            }

            if (null != tmpDe)
            {
                strTitle = tmpDe.Title;
            }
            //Console.WriteLine(strTitle);
            return strTitle;
        }

        public DrawElement GetDrawedElement(int iX, int iY)
        {
            int iWidth = 0;
            int iHeight = 0;

            DrawElement tmpDe = null;

            if (null == m_elementList)
            {
                return null;
            }

            foreach (DrawElement de in m_elementList)
            {
                if (de.OnElement(iX, iY))
                {
                    if (0 == iWidth && 0 == iHeight)
                    {
                        iWidth = de.Width;
                        iHeight = de.Height;
                        tmpDe = de;
                    }
                    else if (iWidth >= de.Width && iHeight >= de.Height)
                    {
                        iWidth = de.Width;
                        iHeight = de.Height;
                        tmpDe = de;
                    }
                }
            }

            return tmpDe;
        }

        public void DrawSelectCursor(int locX, int locY)
        {
            if (null == m_graphics)
            {
                LogMgt.Debug("DrawSelectCursor", "m_graphics is null");
                return;
            }

            DrawElement element = GetDrawedElement(locX, locY);

            if (null != element)
            {
                // Clear cursor

                // Draw cursor
                m_graphics.DrawRectangle(new Pen(selectCursorColor, 2), element.X, element.Y, element.Width, element.Height);
                
                m_realGraphics.DrawImage(bmp, 0, 0);
            }
        }

        private void clearCanvas()
        {
            if (null != m_graphics)
            {
                m_graphics.Clear(Color.DarkGray);
                m_elementList.Clear();
            }
        }

        public void clearAll()
        {
            if (null != m_graphics && null != m_realGraphics && null != bmp)
            {
                m_realGraphics.Clear(Color.DarkGray);
                m_graphics.Clear(Color.DarkGray);
                m_elementList.Clear();
                bmp.Dispose();
            }
        }
    }
}
