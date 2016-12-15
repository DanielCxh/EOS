using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EOS
{
    class ColorData
    {
        // For multi color date user a tag distinguish
        // In this case user node full path
        string m_strTag = "";

        // Save multi color date
        ArrayList m_data = null;

        public ColorData()
        {
            m_data = new ArrayList();
        }

        public ColorData(string strTag, ArrayList data)
        {
            m_strTag = strTag;
            m_data = data;
        }

        public void SetTag(string strTag)
        {
            m_strTag = strTag;
        }

        public string GetTag()
        {
            return m_strTag;
        }

        public void AddData(ColorJson cj)
        {
            m_data.Add(cj);
        }

        public ArrayList GetData()
        {
            return m_data;
        }
    }

    /// <summary>
    /// This class used to store resouece date such as font, color and string.
    /// Only one ResData exist during program is running.
    /// </summary>
    class ResData
    {
        /* Save all color date */
        static ArrayList m_colorDataGroup = new ArrayList();

        /* Save all font date */
        ArrayList m_fontDataGroup;

        /* Save all sring date */
        ArrayList m_stringDataGroup;

        ArrayList m_fontArray;
        ArrayList m_colorArray;
        ArrayList m_stringArray;

        private static ResData m_instance = null;
        private static readonly object lockHelper = new object();

        private ResData()
        {
           // m_colorDataGroup = new ArrayList();
            m_fontDataGroup = new ArrayList();
            m_stringDataGroup = new ArrayList();

            m_fontArray = new ArrayList();
            m_colorArray = new ArrayList();
            m_stringArray = new ArrayList();
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

        /* need check */
        public void AddColor(ColorJson color)
        {
            m_colorArray.Add(color);
        }

        public ArrayList GetColorList()
        {
            return m_colorArray;
        }

        public void AddFont(FontJson font)
        {
            m_fontArray.Add(font);
        }

        public ArrayList GetFontList()
        {
            return m_fontArray;
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

                ColorData data = new ColorData();

                data.SetTag(strResFilePath);

                foreach (string str in arrList)
                {
                    string strVal = Common.GetJsonValueByKey(detailContent, str);

                    ColorJson job = null;

                    try
                    {
                        job = (ColorJson)JsonConvert.DeserializeObject(strVal, typeof(ColorJson));
                        job.Title = str;

                        data.AddData(job);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[E]" + e.Message);
                    }   
                }

                /*new check if contani*/
                m_colorDataGroup.Add(data);
            }

            foreach (ColorData c in m_colorDataGroup)
            {
                ArrayList array = c.GetData();

                foreach (ColorJson cj in array)
                {
                    Console.WriteLine(cj.Title +":"+ cj.Format +":" + cj.Value[0]);
                }
            }

        }

        static public ColorData GetResColorDataByTag(string strTag)
        {
            if (0 == strTag.CompareTo(""))
            {
                return null;
            }

            foreach(ColorData cd in m_colorDataGroup)
            {
                if (0 == cd.GetTag().CompareTo(strTag))
                {
                    return cd;
                }
            }

            return null;
        }
    }
}
