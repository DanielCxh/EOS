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
    public partial class TreeProperty : Form
    {
        private TreeNode m_currentNode = null;

        public TreeProperty()
        {
            InitializeComponent();
        }

        public TreeProperty(TreeNode node)
        {
            InitializeComponent();

            m_currentNode = node;

            initDisplay();
        }
        
        private void SetTreeNode(TreeNode node)
        {
            m_currentNode = node;
        }

        private void initDisplay()
        {
            if (null == m_currentNode)
            {
                return;
            }

            TreeNodeJson tnj = CfgTree.GetTreeNodeContent(m_currentNode);

            if (null == tnj)
            {
                return;
            }

            textBoxTitle.Text = tnj.Title;

            if (0 == tnj.TakeFocus.ToString().CompareTo("true"))
            {
                checkBoxTakeFocus.Checked = true;
            }
            else
            {
                checkBoxTakeFocus.Checked = false;
            }
        }
    }
}
