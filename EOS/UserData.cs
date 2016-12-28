using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace EOS
{
    class UserFile
    {
        public string m_strTag = "";

        ArrayList m_defineList = null;

        public UserFile(string strTag)
        {
            m_strTag = strTag;
            m_defineList = new ArrayList();
        }

        public string GetTag()
        {
            return m_strTag;
        }

        public ArrayList GetDataList()
        {
            return m_defineList;
        }

        public string GetValByName(string strName)
        {
            string str = "";

            if (null == strName || null == m_defineList || 0 >= m_defineList.Count)
            {
                return "";
            }

            return str;
        }

        public void AddDefineData(UserDefine ud)
        {
            if (null == m_defineList)
            {
                m_defineList = new ArrayList();
            }

            if (null != ud)
            {
                m_defineList.Add(ud);
            }
        }
    }

    class UserData
    {
        /* Save all user define date */
        static ArrayList m_DefineFileGroup = new ArrayList();

        const string STR_KEY_SHAP_DEFINE = "#define";
        const string STR_KEY_SHAP_IF_NO_DEFINE = "#ifndef";
        const string STR_KEY_SHAP_IF = "#if";
        const string STR_KEY_SHAP_ELSE = "#else";
        const string STR_KEY_SHAP_END_IF = "#endif";
        
        const string STR_KEY_FRONT_COMMENT = "/*";
        const string STR_KEY_BACK_COMMENT = "*/";
        const string STR_KEY_SIGNAL_COMMENT = "//";
        const string STR_KEY_LEFT_BRACKET = "(";
        const string STR_KEY_RIGHT_BRACKET = ")";
        const char CHAR_KEY_OPERATION_PLUS = '+';
        const char CHAR_KEY_OPERATION_MINU = '-';
        const char CHAR_KEY_OPERATION_TIME = '*';
        const char CHAR_KEY_OPERATION_DIVISION = '/';

        public const int INVALID_NUM = -2147483648;

        bool m_bMultiComment = false;

        private static UserData m_instance = null;
        private static readonly object lockHelper = new object();

        private UserData()
        {

        }

        public static UserData GetInstance()
        {
            if (m_instance == null)
            {
                lock (lockHelper)
                {
                    if (m_instance == null)
                    {
                        m_instance = new UserData();
                    }
                }
            }
            return m_instance;
        }

        public bool syncUserFile(string strUserFilePath)
        {
            bool bRst = false;

            if (Common.IsStrEmpty(strUserFilePath))
            {
                return false;
            }

            ArrayList lines = Common.GetFileContentLines(strUserFilePath);
            UserFile userFile = null;

            userFile = getFileByTag(strUserFilePath);

            if (null == userFile)
            {
                userFile = new UserFile(strUserFilePath);
            }

            foreach (string str in lines)
            {
                analyzeHeadContent(userFile, str);
            }

            if (null == getFileByTag(strUserFilePath))
            {
                m_DefineFileGroup.Add(userFile);
            }

            // tmp
            decodeUserFile(getFileByTag(strUserFilePath));

            /*
            foreach (UserDefine ud in getFileByTag(strUserFilePath).GetDataList())
            {
                Console.WriteLine(String.Format("{0}:{1}:{2}", ud.DefineName, ud.DefineVal, ud.DefineNum));
            }
             * */
            
            return bRst;
        }

        private void analyzeHeadContent(UserFile userFile, string str)
        {
            if (null == userFile || Common.IsStrEmpty(str))
            {
                return;
            }

            if (str.TrimEnd().EndsWith(STR_KEY_BACK_COMMENT) && true == m_bMultiComment)
            {
                m_bMultiComment = false;
                return;
            }
            else if (str.TrimStart().StartsWith(STR_KEY_FRONT_COMMENT) && false == m_bMultiComment)
            {
                m_bMultiComment = true;
                return;
            }
            else if (str.TrimStart().StartsWith(STR_KEY_SIGNAL_COMMENT))
            {
                return;
            }
            else if (str.TrimStart().StartsWith(STR_KEY_SHAP_IF_NO_DEFINE))
            {
                return;
            }
            else if (str.TrimStart().StartsWith(STR_KEY_SHAP_IF))
            {
                return;
            }
            else if (str.TrimStart().StartsWith(STR_KEY_SHAP_ELSE))
            {
                return;
            }
            else if (str.TrimStart().StartsWith(STR_KEY_SHAP_END_IF))
            {
                return;
            }

            string strContent = str.Trim();

            if (strContent.Contains(STR_KEY_SIGNAL_COMMENT))
            {
                strContent = strContent.Substring(0, strContent.IndexOf(STR_KEY_SIGNAL_COMMENT));
            }

            if (strContent.Contains(STR_KEY_FRONT_COMMENT))
            {
                strContent = strContent.Substring(0, strContent.IndexOf(STR_KEY_FRONT_COMMENT));
            }

            // Define
            if (strContent.StartsWith(STR_KEY_SHAP_DEFINE))
            {
                // Remove unwanted spaces and split string by one space
                string strTmp = System.Text.RegularExpressions.Regex.Replace(strContent, @"\b\s+\b", " ");
                string[] sArray = strTmp.Split(new Char[] { ' ' }, 3);
                
                UserDefine userDefine = null;
                int iNewEleIndex = 0;

                foreach (string content in sArray)
                {
                    if (false == Common.IsStrEmpty(content))
                    {
                        if (0 == content.Trim().CompareTo(STR_KEY_SHAP_DEFINE))
                        {
                            userDefine = new UserDefine();
                            iNewEleIndex = 1;
                        }
                        else if (1 == iNewEleIndex && null != userDefine)
                        {
                            userDefine.DefineName = content.Trim();
                            iNewEleIndex = 2;
                        }
                        else if (2 == iNewEleIndex && null != userDefine)
                        {
                            userDefine.DefineVal = content.Trim();
                            string strNum = "";
                            
                            if (content.Trim().StartsWith(STR_KEY_LEFT_BRACKET) && content.Trim().EndsWith(STR_KEY_RIGHT_BRACKET) && 3 <= content.Trim().Length)
                            {
                                strNum = content.Trim().Substring(1, content.Trim().Length - 2);
                            }

                            int outVal = INVALID_NUM;

                            if (int.TryParse(strNum.Trim(), out outVal))
                            {
                                userDefine.DefineNum = outVal;
                            }
                            else
                            {
                                userDefine.DefineNum = INVALID_NUM;
                            }
                            iNewEleIndex = 3;
                        }
                    }
                }

                if (null != userDefine)
                {
                    userFile.AddDefineData(userDefine);
                }
            }
        }

        private void decodeUserFile(UserFile userFile)
        {
            if (null == userFile)
            {
                return;
            }

            if (0 >= userFile.GetDataList().Count)
            {
                LogMgt.Debug("decodeUserFile", "Count is 0.");
                return;
            }

            foreach (UserDefine ud in userFile.GetDataList())
            {
                if (ud.DefineNum == INVALID_NUM && null != ud.DefineVal)
                {
                    string content = ud.DefineVal.ToString().Trim();
                    string str = "";

                    // Remove '(' and ')'
                    if (content.StartsWith(STR_KEY_LEFT_BRACKET) && content.EndsWith(STR_KEY_RIGHT_BRACKET) && 3 <= content.Length)
                    {
                        str = content.Substring(1, content.Trim().Length - 2);
                    }

                    // Remove space
                    string strTmp = System.Text.RegularExpressions.Regex.Replace(str, @"\b\s+\b", "");

                    Char[] checkList = {CHAR_KEY_OPERATION_PLUS, CHAR_KEY_OPERATION_MINU, CHAR_KEY_OPERATION_TIME, CHAR_KEY_OPERATION_DIVISION};

                    string[] strElements = strTmp.Split(checkList);
                    int num = INVALID_NUM;

                    foreach (string element in strElements)
                    {
                        num = getNumByName(userFile, element);
                        ud.DefineNum = num;
                    }
                }
            }
        }

        private UserFile getFileByTag(string strTag)
        {
            UserFile userFile = null;

            if (null == m_DefineFileGroup || 0 >= m_DefineFileGroup.Count)
            {
                return null;
            }

            foreach (UserFile uf in m_DefineFileGroup)
            {
                if (0 == uf.GetTag().CompareTo(strTag))
                {
                    userFile = uf;
                }
            }

            return userFile;
        }

        private int getNumByName(UserFile userFile, string strName)
        {
            int iVal = INVALID_NUM;

            if (null == userFile || Common.IsStrEmpty(strName))
            {
                return INVALID_NUM;
            }

            foreach (UserDefine ud in userFile.GetDataList())
            {
                if (0 == ud.DefineName.CompareTo(strName))
                {
                    return ud.DefineNum;
                }
            }

            return iVal;
        }
    }
}
