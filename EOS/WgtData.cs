using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EOS
{
    class WgtFile
    {
        // wgt file full path.
        public string m_strTag = "";
        public string m_strID = "";

        ArrayList m_bitmapImgList = null;
        ArrayList m_solidImgList = null;
        ArrayList m_textBoxList = null;
        ArrayList m_pushBtnList = null;
        ArrayList m_scrollBarList = null;
        ArrayList m_scheduleBarList = null;

        public WgtFile()
        {
            m_bitmapImgList = new ArrayList();
        }

        public WgtFile(string strTag
            , ArrayList bitmapImgList = null
            , ArrayList solidImgList = null
            , ArrayList textBoxList = null
            , ArrayList pushBtnList = null
            , ArrayList scrollBarList = null
            , ArrayList scheduleBarList = null)
        {
            m_strTag = strTag;

            m_bitmapImgList = bitmapImgList;
            m_solidImgList = solidImgList;
            m_textBoxList = textBoxList;
            m_pushBtnList = pushBtnList;
            m_scrollBarList = scrollBarList;
            m_scheduleBarList = scheduleBarList;
        }

        public void SetTag(string strTag)
        {
            m_strTag = strTag;
        }

        public string GetTag()
        {
            return m_strTag;
        }

        public ArrayList GetBitmapImgList()
        {
            return m_bitmapImgList;
        }

        public ArrayList GetSolidImgList()
        {
            return m_solidImgList;
        }

        public ArrayList GetTextBoxList()
        {
            return m_textBoxList;
        }

        public ArrayList GetPushBtnList()
        {
            return m_pushBtnList;
        }

        public ArrayList GetScrollBarList()
        {
            return m_scrollBarList;
        }

        public ArrayList GetScheduleBarList()
        {
            return m_scheduleBarList;
        }

        public bool AddBitmapImgData(BitmapImgJson bij)
        {
            bool bRst = false;

            if (null != bij && null != m_bitmapImgList)
            {
                m_bitmapImgList.Add(bij);
                bRst = true;
            }

            return bRst;
        }

        public BitmapImgJson GetBitmapImgDataByTitle(string strTitle)
        {
            BitmapImgJson bitmap = null;

            if (Common.IsStrEmpty(strTitle) || null == m_bitmapImgList)
            {
                return null;
            }

            foreach (BitmapImgJson bij in m_bitmapImgList)
            {
                if (0 == bij.Title.CompareTo(strTitle))
                {
                    bitmap = bij;
                    break;
                }
            }

            return bitmap;
        }

        public bool AddPushButtonData(PushButtonJson pbj)
        {
            bool bRst = false;

            if (null == pbj)
            {
                return false;
            }

            if (null == m_pushBtnList)
            {
                m_pushBtnList = new ArrayList();
            }

            m_pushBtnList.Add(pbj);

            return bRst;
        }

    }

    class WgtItemOutline
    {
        public string Path { get; set;}
        public string Title { get; set;}
        public object Object { get; set;}
        public CfgWgt.WgtType Type { get; set;}

        public WgtItemOutline()
        {

        }
    }

    class WgtData
    {
        /* Save all the wgt files content */
        public static ArrayList m_WgtFileGroup = new ArrayList();

        private static WgtData m_instance = null;
        private static readonly object lockHelper = new object();

        private WgtData()
        {

        }

        public static WgtData GetInstance()
        {
            if (m_instance == null)
            {
                lock (lockHelper)
                {
                    if (m_instance == null)
                    {
                        m_instance = new WgtData();
                    }
                }
            }
            return m_instance;
        }

        public static WgtFile GetWgtFileByTag(string strTag)
        {
            WgtFile wgtFile = null;

            if (null == strTag || 0 == strTag.CompareTo(""))
            {
                return null;
            }

            if (0 >= m_WgtFileGroup.Count)
            {
                return null;
            }

            foreach (WgtFile wf in m_WgtFileGroup)
            {
                if (0 == wf.GetTag().CompareTo(strTag))
                {
                    wgtFile = wf;
                }
            }

            return wgtFile;
        }

        public static void AddWgtFile(WgtFile wf)
        {
            if (null == wf)
            {
                return;
            }

            if (null == m_WgtFileGroup)
            {
                m_WgtFileGroup = new ArrayList();
            }

            if (null == GetWgtFileByTag(wf.GetTag()))
            {
                m_WgtFileGroup.Add(wf);
            }
        }

        public static bool SyncWgtFile(string strWgtFilePath)
        {
            bool bRst = false;

            if (Common.IsStrEmpty(strWgtFilePath))
            {
                return false;
            }

            // Get file content
            string content = Common.GetFileContent(strWgtFilePath);

            // Skelton
            ArrayList propertyList = Common.GetJsonProperties(content);

            WgtFile wf = GetWgtFileByTag(strWgtFilePath);

            if (null == wf)
            {
                wf = new WgtFile();
                wf.SetTag(strWgtFilePath);
            }

            foreach (string key in propertyList)
            {
                string strJsonVal = Common.GetJsonValueByKey(content, key);

                ArrayList strName = Common.GetJsonProperties(strJsonVal);

                foreach (string name in strName)
                {
                    string strVal = Common.GetJsonValueByKey(strJsonVal, name);

                    if (0 == key.CompareTo(CfgWgt.WGT_NODE_TYPE_BITMAP_IMG))
                    {
                        BitmapImgJson job = null;

                        try
                        {
                            job = (BitmapImgJson)JsonConvert.DeserializeObject(strVal, typeof(BitmapImgJson));
                            job.Title = name;

                            wf.AddBitmapImgData(job);
                        }
                        catch (Exception e)
                        {
                            bRst = false;
                            Console.WriteLine("[E]" + e.Message);
                        }   
                    }
                    else if (0 == key.CompareTo(CfgWgt.WGT_NODE_TYPE_SOLID_IMG))
                    {

                    }
                    else if (0 == key.CompareTo(CfgWgt.WGT_NODE_TYPE_PUSH_BUTTON))
                    {
                        PushButtonJson job = null;

                        try
                        {
                            job = (PushButtonJson)JsonConvert.DeserializeObject(strVal, typeof(PushButtonJson));
                            job.Title = name;

                            wf.AddPushButtonData(job);
                        }
                        catch (Exception e)
                        {
                            bRst = false;
                            Console.WriteLine("[E]" + e.Message);
                        }   
                    }
                }
            }

            if (null == GetWgtFileByTag(strWgtFilePath))
            {
                AddWgtFile(wf);
            }

            return bRst;
        }

        public static bool SyncWgtFileByType(string strFilePath, CfgWgt.WgtType eType)
        {
            bool bRst = false;



            return bRst;
        }

        public static object GetWgtContent(string strFileTag, string property, string keyName)
        {
            object obj = null;

            WgtFile wf = GetWgtFileByTag(strFileTag);

            if (null != wf)
            {
                CfgWgt.WgtType eType = CfgWgt.GetWgtNodeTypeByStr(property);

                if (CfgWgt.WgtType.BITMAP_IMG == eType)
                {
                    obj = GetBitmapImgByKeyName(wf.GetBitmapImgList(), keyName);
                }
                else if (CfgWgt.WgtType.SOLID_IMG == eType)
                {

                }
                else
                {
 
                }
            }
            return obj;
        }

        public static BitmapImgJson GetBitmapImgByKeyName(ArrayList bitmapImgList, string strKeyName)
        {
            BitmapImgJson bij = null;

            if (null == bitmapImgList || Common.IsStrEmpty(strKeyName))
            {
                return null;
            }

            foreach (BitmapImgJson item in bitmapImgList)
            {
                if (0 == item.Title.CompareTo(strKeyName))
                {
                    bij = item;
                    return bij;
                }
            }

            return bij;
        }

        public static WgtItemOutline GetWgtItem(string strFilePath, string strTitle)
        {
            WgtItemOutline item = null;


            return item;
        }

        public static WgtItemOutline GetWgtItem(string strTitle)
        {
            WgtItemOutline item = null;

            if (Common.IsStrEmpty(strTitle) || null == m_WgtFileGroup || 0 >= m_WgtFileGroup.Count)
            {
                return null;
            }

            foreach (WgtFile wf in m_WgtFileGroup)
            {
                foreach (BitmapImgJson bij in wf.GetBitmapImgList())
                {
                    if (0 == bij.Title.CompareTo(strTitle))
                    {
                        item = new WgtItemOutline();
                        item.Title = strTitle;
                        item.Path = wf.GetTag();
                        item.Object = bij;
                        item.Type = CfgWgt.WgtType.BITMAP_IMG;
                        break;
                    }
                }
            }

            return item;
        }
    }
}
