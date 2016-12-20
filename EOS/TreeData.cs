﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EOS
{
    class TreeFile
    {
        // The tag is tree file path.
        public string m_strTag = "";
        public ArrayList m_nodeList = null;

        public TreeFile()
        {
 
        }

        public TreeFile(string strTag, ArrayList array)
        {
            m_strTag = strTag;

            if (null != array && array.Count > 0)
            {
                m_nodeList = array;
            }
        }

        public string GetTag()
        {
            return m_strTag;
        }

        public void SetTag(string strTag)
        {
            m_strTag = strTag;
        }

        public void AddTreeNode(TreeNodeJson tnj)
        {
            if (null == tnj)
            {
                return;
            }

            if (null == m_nodeList)
            {
                m_nodeList = new ArrayList();
            }

            m_nodeList.Add(tnj);
        }

        public TreeNodeJson GetTreeNodeByKey(string strKey)
        {
            TreeNodeJson treeNode = null;

            if (Common.IsStrEmpty(strKey) || null == m_nodeList)
            {
                return null;
            }

            foreach (TreeNodeJson tnj in m_nodeList)
            {
                if (0 == tnj.Title.CompareTo(strKey))
                {
                    treeNode = tnj;
                    break;
                }
            }

            return treeNode;
        }
    }

    class NodeResData
    {
        public string Title { get; set; }
        public string ResFile { get; set; }
        public string Key { get; set; }

        public NodeResData()
        {
 
        }

        public NodeResData(string strTitle, string strResFile, string strKey)
        {
            Title = strTitle;
            ResFile = strResFile;
            Key = strKey;
        }
    }

    class TreeData
    {
        /* Save all tree file */
        public static ArrayList m_TreeFileGroup = new ArrayList();

        /* Save all tree node resource information */
        public static ArrayList m_TreeNodeResDataGroup = new ArrayList();

        private static TreeData m_instance = null;
        private static readonly object lockHelper = new object();

        private TreeData()
        {

        }

        public static TreeData GetInstance()
        {
            if (m_instance == null)
            {
                lock (lockHelper)
                {
                    if (m_instance == null)
                    {
                        m_instance = new TreeData();
                    }
                }
            }
            return m_instance;
        }

        public static void AddTreeFile(TreeFile tf)
        {
            if (null == tf)
            {
                return;
            }

            if (null == m_TreeFileGroup)
            {
                m_TreeFileGroup = new ArrayList();
            }

            if (null == GetTreeFileByTag(tf.GetTag()))
            {
                m_TreeFileGroup.Add(tf);
            }
        }

        public static bool SyncTreeFile(string strTreeFilePath)
        {
            bool bRst = false;

            if (Common.IsStrEmpty(strTreeFilePath))
            {
                LogMgt.Debug("SyncTreeFile", "Tree file path is empty.");
                return false;
            }

            string content = Common.GetFileContent(strTreeFilePath);

            if (Common.IsStrEmpty(content))
            {
                LogMgt.Debug("SyncTreeFile", "Tree file content is empty.");
                return false;
            }

            ArrayList itemList = Common.GetJsonProperties(content);

            TreeFile tf = GetTreeFileByTag(strTreeFilePath);

            if (null == tf)
            {
                tf = new TreeFile();
                tf.SetTag(strTreeFilePath);
            }

            foreach (string key in itemList)
            {
                string strJsonVal = Common.GetJsonValueByKey(content, key);

                TreeNodeJson job = null;

                try
                {
                    job = (TreeNodeJson)JsonConvert.DeserializeObject(strJsonVal, typeof(TreeNodeJson));
                    job.Title = key;

                    tf.AddTreeNode(job);

                    // Add resource date
                    m_TreeNodeResDataGroup.Add(AnalyzeTreeNodeRes(key, job.Resource));
                    
                    bRst = true;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    bRst = false;
                }
            }

            AddTreeFile(tf);

            return bRst;
        }

        public static TreeFile GetTreeFileByTag(string strTag)
        {
            TreeFile treeFile = null;

            if (null == m_TreeFileGroup)
            {
                return null;
            }

            foreach (TreeFile tf in m_TreeFileGroup)
            {
                if (0 == tf.GetTag().CompareTo(strTag))
                {
                    treeFile = tf;
                    break;
                }
            }

            return treeFile;
        }

        public static NodeResData AnalyzeTreeNodeRes(string strItemName, string[] strRes)
        {
            NodeResData nrd = null;

            string str = strRes[0];

            if (Common.IsStrEmpty(str))
            {
                return null;
            }

            string[] strContent = str.Split('@');

            if (strContent.Length != 2)
            {
                return null;
            }

            nrd = new NodeResData();

            nrd.Key = strContent[0];
            nrd.ResFile = strContent[1];
            nrd.Title = strItemName;

            return nrd;
        }

        public static NodeResData GetNodeResByTitle(string strTitle)
        {
            NodeResData nodeRes = null;

            if (Common.IsStrEmpty(strTitle))
            {
                return null;
            }

            if (null == m_TreeNodeResDataGroup)
            {
                return null;
            }

            foreach (NodeResData nrd in m_TreeNodeResDataGroup)
            {
                if (0 == nrd.Title.CompareTo(strTitle))
                {
                    nodeRes = nrd;
                    break;
                }
            }

            return nodeRes;
        }
    }
}
