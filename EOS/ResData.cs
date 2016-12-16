using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EOS
{
    class ResFile
    {
        // Class that contain one resource file content

        string m_strTag = "";

        ArrayList m_fontData = null;
        ArrayList m_colorData = null;
        ArrayList m_stringData = null;

        public ResFile()
        {
            m_colorData = new ArrayList();
            m_fontData = new ArrayList();
            m_stringData = new ArrayList();
        }

        public ResFile(string strTag
            , ArrayList fontData = null
            , ArrayList colorData = null
            , ArrayList stringData = null)
        {
            m_strTag = strTag;
            m_fontData = fontData;
            m_colorData = colorData;
            m_stringData = stringData;
        }

        public void SetTag(string strTag)
        {
            m_strTag = strTag;
        }

        public string GetTag()
        {
            return m_strTag;
        }

        public void AddColorData(ColorJson cj)
        {
            if (null != cj)
            {
                m_colorData.Add(cj);
            }
        }

        public void AddFontData(FontJson fj)
        {
            if (null != fj)
            {
                m_fontData.Add(fj);
            }
        }

        public ArrayList GetColorData()
        {
            return m_colorData;
        }
    }

    /// <summary>
    /// This class used to store resouece date such as font, color and string.
    /// Only one ResData exist during program is running.
    /// </summary>
    class ResData
    {
        /* Save all resource date */
        static ArrayList m_ResFileGroup = new ArrayList();

        private static ResData m_instance = null;
        private static readonly object lockHelper = new object();

        private ResData()
        {

        }

        public static ResData GetInstance()
        {
            if (m_instance == null)
            {
                lock (lockHelper)
                {
                    if (m_instance == null)
                    {
                        m_instance = new ResData();
                    }
                }
            }
            return m_instance;
        }

        // Read resource file, must make sure file path point to a .res file
        static public void SyncResFile(string strResFilePath, CfgRes.ResType eType)
        {
            if (null == strResFilePath || 0 == strResFilePath.CompareTo(""))
            {
                return;
            }

            string content = Common.GetFileContent(strResFilePath);

            // Get detail contenct according resource type.
            string strKey = CfgRes.GetResTypeByEnum(eType);

            if (0 != strKey.CompareTo(""))
            {
                Console.WriteLine(strResFilePath);
                string detailContent = Common.GetJsonValueByKey(content, strKey);

                ArrayList arrList = Common.GetJsonProperties(detailContent);

                // Check ResFile exist or not
                ResFile data = GetResFileByTag(strResFilePath);

                if (null == data)
                {
                    data = new ResFile();
                    data.SetTag(strResFilePath);
                }

                foreach (string str in arrList)
                {
                    string strVal = Common.GetJsonValueByKey(detailContent, str);

                    ColorJson job = null;

                    try
                    {
                        job = (ColorJson)JsonConvert.DeserializeObject(strVal, typeof(ColorJson));
                        job.Title = str;

                        data.AddColorData(job);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[E]" + e.Message);
                    }   
                }

                // if no add, else get ResFile and set
                if (null == GetResFileByTag(strResFilePath))
                {
                    m_ResFileGroup.Add(data);
                }
            }
        }

        static public ResFile GetResFileByTag(string strTag)
        {
            if (0 == strTag.CompareTo("") || null == m_ResFileGroup)
            {
                return null;
            }

            foreach (ResFile cd in m_ResFileGroup)
            {
                if (0 == cd.GetTag().CompareTo(strTag))
                {
                    return cd;
                }
            }

            return null;
        }

        private void clearAllResData()
        {
            if (null != m_ResFileGroup)
            {
                m_ResFileGroup.Clear();
            }
        }
    }
}
