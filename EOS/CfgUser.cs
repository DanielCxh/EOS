using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EOS
{
    class UserDefine
    {
        public string DefineName { get; set;}
        public string DefineVal { get; set; }
        public int DefineNum { get; set; }

        public UserDefine()
        {
            DefineNum = UserData.INVALID_NUM;
        }

        public UserDefine(string strDefineName, string strDefineVal)
        {
            DefineName = strDefineName;
            DefineVal = strDefineVal;
            DefineNum = UserData.INVALID_NUM;
        }

        public int GetNum()
        {
            return DefineNum;
        }
    }

    class CfgUser
    {
        public const string HEAD_SUFFIX = ".h";

        public CfgUser()
        {
 
        }

        public static bool IsHeadFile(TreeNode node)
        {
            bool bRst = false;

            if (null == node)
            {
                return false;
            }

            if (node.FullPath.EndsWith(HEAD_SUFFIX))
            {
                bRst = true;
            }

            return bRst;
        }
    }
}
