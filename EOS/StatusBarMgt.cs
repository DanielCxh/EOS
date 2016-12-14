using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EOS
{
    class StatusBarMgt
    {
        private volatile static StatusBarMgt m_instance = null;
        private static readonly object lockHelper = new object();

        private Label statusBarLabel = null;

        private StatusBarMgt()
        {
 
        }

        public static StatusBarMgt getInstance()
        {
            if (m_instance == null)
            {
                lock (lockHelper)
                {
                    if (m_instance == null)
                    {
                        m_instance = new StatusBarMgt();
                    }
                }
            }
            return m_instance;
        }

        public void SetStatusBarLabel(Label lable)
        {
            statusBarLabel = lable;
        }

        public void SetStatusBarInfo(string strInfo)
        {
            if (null == statusBarLabel || 0 == strInfo.CompareTo(""))
            {
                return;
            }

            statusBarLabel.Text = strInfo;
        }
    }
}
