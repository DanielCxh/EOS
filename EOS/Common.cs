﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;


namespace EOS
{
    class Common
    {
        const string STR_LOWERCASE_TRUE = "true";
        const string STR_LOWERCASE_FALSE = "false";

        /* ----------------------------------------------------------------------
         * Key worlds
         * 
         * 
         * ---------------------------------------------------------------------- */
        const string STR_KEYWORLD_TAKE_FOCUS = "take_focus";
        const string STR_KEYWORLD_ID = "id";

        public static KeywordColor[] HeightLightKeys = new KeywordColor[] { new KeywordColor("file_name", Color.Blue),
                                                                            new KeywordColor("width", Color.Blue),
                                                                            new KeywordColor("height", Color.Blue),
                                                                            new KeywordColor("format", Color.Blue),
                                                                            new KeywordColor("compress", Color.Blue),

                                                                             new KeywordColor("null", Color.Red)
        };


        public Common()
        {
            
        }
        
        /// <summary>
        ///  Get the content of the file by full file path.
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string GetFileContent(string strPath)
        {
            StreamReader sr = new StreamReader(strPath, System.Text.Encoding.Default);

            if (null == sr)
            {
                return "";
            }

            string line;

            string strInfo = "";

            while ((line = sr.ReadLine()) != null)
            {
                strInfo = strInfo + line.Trim();
            }

            sr.Close();

            return strInfo;
        }

        public static ArrayList GetFileContentLines(string strPath)
        {
            ArrayList lines;

            StreamReader sr = new StreamReader(strPath, System.Text.Encoding.Default);

            if (null == sr)
            {
                return null;
            }

            lines = new ArrayList();

            string strLine = "";

            while ((strLine = sr.ReadLine()) != null)
            {
                lines.Add(strLine);
            }

            sr.Close();

            return lines;
        }

        public static string GetJsonValueByKey(string strJson, string key)
        {
            string strVal = "";

            JObject o = JObject.Parse(strJson);

            IEnumerable<JProperty> properties = o.Properties();

            foreach (JProperty item in properties)
            {
                if (0 == item.Name.ToString().CompareTo(key))
                {
                    strVal = item.Value.ToString();
                    break;
                }
            }

            return strVal;
        }

        public static ArrayList GetJsonProperties(string strJson)
        {
            ArrayList arrList = new ArrayList();

            JObject jo = JObject.Parse(strJson);

            IEnumerable<JProperty> properties = jo.Properties();

            foreach (JProperty item in properties)
            {
                arrList.Add(item.Name.ToString());
            }

            return arrList;
        }

        public static bool IsStrEmpty(string str)
        {
            bool bRst = false;

            if (null == str || 0 == str.CompareTo("") || 0 == str.Length)
            {
                bRst = true;
            }

            return bRst;
        }

        public static bool StrSameVal()
        {
            bool bRst = false;
            return bRst;
        }

        public static bool StrIsTrue(string str)
        {
            bool bRst = false;

            if (IsStrEmpty(str))
            {
                return false;
            }

            if (0 == str.ToLower().CompareTo(STR_LOWERCASE_TRUE))
            {
                bRst = true;
            }

            return bRst;
        }

        public static bool StrIsFalse(string str)
        {
            bool bRst = false;

            if (IsStrEmpty(str))
            {
                return false;
            }

            if (0 == str.ToLower().CompareTo(STR_LOWERCASE_FALSE))
            {
                bRst = true;
            }

            return bRst;
        }
    }
}
