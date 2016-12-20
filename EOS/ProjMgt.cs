using System;
using System.Windows.Forms;

using System.Collections;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace EOS
{
    class ProjectJosn
    {
        [JsonProperty(PropertyName = "project_info")]
        public ProjectInfoJson ProjectInfo { get; set; }

        [JsonProperty(PropertyName = "project_cfg")]
        public ProjectCfgJson ProjectCfg { get; set; }
    }

    class ProjectInfoJson
    {
        [JsonProperty(PropertyName = "project_name")]
        public string ProjectName { get; set; }

        [JsonProperty(PropertyName = "project_res")]
        public string ProjectLoc { get; set; }
    }

    class ProjectCfgJson
    {
        [JsonProperty(PropertyName = "screen_width")]
        public string screenWidth { get; set; }

        [JsonProperty(PropertyName = "screen_height")]
        public string screenHeight { get; set; }
    }

    public class ProjMgt
    {
        const string FILE_TYPE_WGT = ".wgt";
        const string FILE_TYPE_TREE = ".tree";
        const string FILE_TYPE_RESOURCE = ".res";

        private static ProjMgt m_instance = null;
        private static readonly object lockHelper = new object();

        private string crtProjectPath = "";

        private string strProjectName;
        private string strProjectResLoc;
        private int iScreenWidth;
        private int iScreenHeight;

        private TreeView m_projectTree = null;

        public enum NodeDetailType
        {
            NT_UNKNOW = 0,
            
            NT_PICTURE_PNG = 10,
            NT_PICTURE_JPG = 11,

            NT_RES_FONT = 100,
            NT_RES_COLOR = 101,
            NES_RES_STRING = 102,

            NT_WGT_BITMAP_IMG = 200,
            NT_WGT_SOLD_IMG = 201,
            NT_WGT_TEXT_BOX = 202,
            NT_WGT_PUSH_BUTTON = 203,
            NT_WGT_SCROLL_BAR = 204,
            NT_WGT_SCHEDULE_BAR = 205,

            NT_TREE_ITEM = 300
        }

        private ProjMgt()
        {
            /* Do nothing */
        }

        public static ProjMgt GetInstance()
        {
            if (m_instance == null)
            {
                lock (lockHelper)
                {
                    if (m_instance == null)
                    {
                        m_instance = new ProjMgt();
                    }
                }
            }
            return m_instance;
        }

        public void ShowFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Please select ";
            openFileDialog.Filter = "Project File(*.eos)|*.eos";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = openFileDialog.FileName;

                if (0 != strFilePath.CompareTo(""))
                {
                    if (0 == strFilePath.CompareTo(crtProjectPath))
                    {
                        Console.WriteLine(strFilePath + "has opened");
                    }
                    else
                    {
                        ProjLoading projLoading = new ProjLoading();

                        projLoading.Show();

                        crtProjectPath = strFilePath;

                        initProjTree(strFilePath);

                        loadProjContent();

                        projLoading.Close();
                    }
                }
            }
        }

        public string GetProjectName()
        {
            return strProjectName;
        }

        public string GetProjectResLoc()
        {
            return strProjectResLoc;
        }

        public int GetScreenWidth()
        {
            return iScreenWidth;
        }

        public int GetScreenHeight()
        {
            return iScreenHeight;
        }

        private bool readProjFile(string strPath)
        {
            bool bRst = false;

            string strJsonContent = Common.GetFileContent(strPath);

            ProjectJosn job = null;

            try
            {
                /* Deserialize project object */
                job = (ProjectJosn)JsonConvert.DeserializeObject(strJsonContent, typeof(ProjectJosn));

                /* Initialize project information */
                strProjectName = job.ProjectInfo.ProjectName;
                strProjectResLoc = job.ProjectInfo.ProjectLoc;

                iScreenWidth = Int16.Parse(job.ProjectCfg.screenWidth);
                iScreenHeight = Int16.Parse(job.ProjectCfg.screenHeight);

                bRst = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("[E]" + e.Message);

                strProjectName = "";
                strProjectResLoc = "";

                iScreenWidth = 0;
                iScreenHeight = 0;

                return false;
            }

            return bRst;
        }

        public void SetProjectTree(TreeView tv)
        {
            m_projectTree = tv;
        }

        private void initProjSkeleton(string strPath)
        {
            if (0 == strPath.CompareTo(""))
            {
                Console.WriteLine("Resource path is empty");
                return;
            }

            scanFolderToAddNode(m_projectTree.Nodes, strPath);
        }

        /// <summary>
        /// Scan the folder and add the node
        /// </summary>
        private void scanFolderToAddNode(TreeNodeCollection node, string strFolderPath)
        {
            DirectoryInfo TheFolder = new DirectoryInfo(strFolderPath);

            foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
            {
                TreeNode newNode = new TreeNode(NextFolder.Name);
                
                node.Add(newNode);
                scanFolderToAddNode(newNode.Nodes, NextFolder.FullName);
            }

            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                TreeNode newNode = new TreeNode(NextFile.Name);
                node.Add(newNode);

                /* Analyze project files */
                analyzeProjFileToAddNode(newNode, NextFile.FullName);
            }
        }

        private void analyzeProjFileToAddNode(TreeNode parentNode, string filePath)
        {
            if (0 == filePath.CompareTo(""))
            {
                return;
            }

            if (false == isValidCfgFile(filePath))
            {
                return;
            }

            string file = Common.GetFileContent(filePath);

            if (0 == file.CompareTo(""))
            {
                return;
            }

            // Decode file according json
            ArrayList arrList = Common.GetJsonProperties(file);

            if (true == isValidCfgFile(filePath))
            {
                foreach (string str in arrList)
                {
                    TreeNode newNode = new TreeNode(str);

                    if (parentNode.FullPath.EndsWith(".wgt"))
                    {
                        initWgtSkeleton(newNode, file, str);
                    }
                    else
                    {
                        newNode.ForeColor = Color.Yellow;
                    }

                    parentNode.Nodes.Add(newNode);
                }
            }
  
        }

        private void initWgtSkeleton(TreeNode parentNode, string jsonInfo, string key)
        {
            if (0 == jsonInfo.CompareTo("") || 0 == key.CompareTo(""))
            {
                return;
            }

            string strInfo = Common.GetJsonValueByKey(jsonInfo, key);

            ArrayList arrList = Common.GetJsonProperties(strInfo);

            foreach (string str in arrList)
            {
                TreeNode newNode = new TreeNode(str);
                newNode.ForeColor = Color.Red;
                parentNode.Nodes.Add(newNode);
            }
        }

        private void decodeWgtContent()
        {

        }

        private void decodeTreeContent()
        {

        }

        private void decodeResContent()
        {

        }


        /// <summary>
        /// Check current file type is valid or not.
        /// </summary>
        private bool isValidCfgFile(string filePath)
        {
            bool bRst = false;

            if (0 == filePath.CompareTo(""))
            {
                return false;
            }

            if (filePath.EndsWith(".wgt")
                || filePath.EndsWith(".tree")
                || filePath.EndsWith(".res"))
            {
                bRst = true;
            }

            return bRst;
        }

        
        /// <summary>
        /// Dynamic key
        /// </summary>
        /// <param name="jObject">json string</param>
        /// <returns>First value of json object</returns>
        /*
        private string getJsonValue(string strJson)
        {
            string strResult;
            JObject jo = JObject.Parse(strJson);
            string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();
            if (values == null)
            {
                strResult = "";
            }
            else
            {
                strResult = values[0];
            }

            return strResult;
        }
        */

        private bool isWgtNode(TreeNode node)
        {
            bool bRst = false;

            if (null == node.Parent || null == node.Parent.Parent)
            {
                return bRst;
            }

            if (node.Parent.Parent.FullPath.EndsWith(".wgt"))
            {
                bRst = true;
            }

            return bRst;
        }

        /* Initialize project tree */
        private void initProjTree(string strPath)
        {
            bool bRst = readProjFile(strPath);

            if (true == bRst && 0 != strProjectResLoc.CompareTo(""))
            {
                initProjSkeleton(strProjectResLoc);
            }
        }

        /* Load all project content */
        private void loadProjContent()
        {
            if (null == m_projectTree)
            {
                return;
            }

            foreach (TreeNode rootNode in m_projectTree.Nodes)
            {
                sacnEachNode(rootNode);
            }
        }

        private TreeNode sacnEachNode(TreeNode rootNode)
        {
            TreeNode n = null;

            foreach (TreeNode node in rootNode.Nodes)
            {
                // Leaf node
                if (node.Nodes.Count <= 0)
                {
                    loadLeafNode(node);
                }
                // File node
                else if (node.Nodes.Count > 0
                    && (CfgWgt.IsWgtFileNode(node) || CfgTree.IsTreeFileNode(node)))
                {
                    loadFileNode(node);
                }
                // Node with sub nodes
                else
                {
                    sacnEachNode(node);
                }
            }

            return n;
        }

        private void loadFileNode(TreeNode node)
        {
            if (null == node)
            {
                return;
            }

            if (true == CfgWgt.IsWgtFileNode(node))
            {
                loadWgtContent(node);
            }
            else if (true == CfgTree.IsTreeFileNode(node))
            {
                loadTreeContent(node);
            }
            else
            {

            }
        }

        private void loadLeafNode(TreeNode node)
        {
            if (CfgRes.IsResNode(node))
            {
               // Console.WriteLine("res::::" + node.FullPath);

                loadResContent(node);
            }
            else if (CfgWgt.IsWgtNode(node))
            {
               // Console.WriteLine("wgt::::" + node.FullPath);
            }
            else
            {
               // Console.WriteLine("ote::::" + node.FullPath);
            }
        }

        /* Load resouece file to RAM */
        private void loadResContent(TreeNode node)
        {
            if (CfgRes.IsColorResNode(node))
            {
                //ColorData cd = new ColorData();
                //ResData.GetInstance().AddColor();
                ResData.SyncResFileByType(strProjectResLoc + "\\" + node.Parent.FullPath, CfgRes.ResType.COLOR);
            }
        }

        /* Load wgt files to RAM */
        private void loadWgtContent(TreeNode node)
        {
            if (null == node)
            {
                return;
            }

            WgtData.SyncWgtFile(GetNodeFullPath(node));
        }

        /* Load tree files to RAM*/
        private void loadTreeContent(TreeNode node)
        {
            if (null == node)
            {
                return;
            }

            TreeData.SyncTreeFile(GetNodeFullPath(node));
        }

        /// <summary>
        /// Get node full path, maybe is not file system address.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public string GetNodeFullPath(TreeNode node)
        {
            return strProjectResLoc + "\\" + node.FullPath;
        }

        public static bool IsPictureNode(TreeNode node)
        {
            bool bRst = false;

            if (node.FullPath.EndsWith(".png") || node.FullPath.EndsWith(".jpg"))
            {
                bRst = true;
            }

            return bRst;
        }

        public static NodeDetailType GetNoteDetailType(TreeNode node)
        {
            if (null == node)
            {
                return NodeDetailType.NT_UNKNOW;
            }

            NodeDetailType nodeType = NodeDetailType.NT_UNKNOW;

            if (CfgRes.IsResNode(node))
            {
 
            }
            else if (CfgWgt.IsWgtNode(node))
            {
                switch (CfgWgt.GetWgtNodeType(node))
                {
                    case CfgWgt.WgtType.BITMAP_IMG:
                        nodeType = NodeDetailType.NT_WGT_BITMAP_IMG;
                        break;
                    default:
                        break;
                }

            }
            else if (CfgTree.IsTreeItemNode(node))
            {
                nodeType = NodeDetailType.NT_TREE_ITEM;
            }
            else if (IsPictureNode(node))
            {
                nodeType = NodeDetailType.NT_PICTURE_PNG;
            }
          

            return nodeType;
        }
    }
}
