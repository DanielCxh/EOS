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
        public string GetNodeJsonValue(TreeNode node)
        {
            string strVal = "";

            // Resource item
            if (CfgRes.IsResNode(node))
            {
                // Full file content
                string strContent = Common.GetFileContent(GetProjectResLoc() + "\\" + node.Parent.FullPath);

                // Get color related content
                string strJson = Common.GetJsonValueByKey(strContent, CfgRes.RES_TYPE_COLOR);

                ArrayList arrL = Common.GetJsonProperties(strJson);

                foreach (string sj in arrL)
                {
                    string ss = Common.GetJsonValueByKey(strJson, sj);

                    Console.WriteLine("s" + ss);
                    ColorJson job = null;

                    try
                    {
                        job = (ColorJson)JsonConvert.DeserializeObject(ss, typeof(ColorJson));
                        job.Title = sj;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("eee" + e.Message);
                    }

                    ResData.GetInstance().AddColor(job);
                }

                ArrayList c = ResData.GetInstance().GetColorList();

                foreach (ColorJson col in c)
                {
                    Console.WriteLine(col.Title + ":"+ col.Format +":"+ col.Value[0].ToString());
                }

                
                /*
                RColor job = null;

                try
                {
                    job = (RColor)JsonConvert.DeserializeObject(strJson, typeof(RColor));
                }
                catch (Exception e)
                {
                    Console.WriteLine("e:" + e.Message);
                }
                */
                // Font

                // Color

                // String
            }
            // Tree item
           // else if (node.Parent.FullPath.EndsWith(".tree"))
            //{
                // Tree node
            //}
          
            return strVal;
        }

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
                    loadNode(node);
                }
                // Node with sub nodes
                else
                {
                    sacnEachNode(node);
                }
            }

            return n;
        }

        private void loadNode(TreeNode node)
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
                ResData.SyncResFile(strProjectResLoc + "\\" + node.Parent.FullPath, CfgRes.ResType.COLOR);
            }
        }

        /* Load wgt files to RAM */
        private void loadWgtContent()
        {
 
        }

        /* Load tree files to RAM*/
        private void loadTreeContent()
        {
 
        }
    }
}
