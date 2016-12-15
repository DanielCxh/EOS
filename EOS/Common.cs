﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace EOS
{
    class Common
    {
        /// <summary>
        /// Get the content of the file by full file path.
        /// </summary>
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

            return strInfo;
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
    }
}