using System;
using System.Windows.Forms;
using System.IO;

using System.Collections;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;

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
        private volatile static ProjMgt m_instance = null;
        private static readonly object lockHelper = new object();

        private string crtProject = "";

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
                string strFile = openFileDialog.FileName;

                if (0 != strFile.CompareTo(""))
                {
                    if (0 == strFile.CompareTo(crtProject))
                    {
                        Console.WriteLine(strFile + "has opened");
                    }
                    else
                    {
                        crtProject = strFile;
                        readProjFile(strFile);
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

        /// <summary>
        /// Get  file information by file path.
        /// </summary>
        private string getFileInfo(string strPath)
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

        private void readProjFile(string strPath)
        {
            string json = getFileInfo(strPath);

            Console.WriteLine(json);

            ProjectJosn job = null;

            try
            {
                job = (ProjectJosn)JsonConvert.DeserializeObject(json, typeof(ProjectJosn));
            }
            catch (Exception e)
            {
                Console.WriteLine("e:" + e.Message);
                return;
            }

            strProjectName   = job.ProjectInfo.ProjectName;
            strProjectResLoc = job.ProjectInfo.ProjectLoc;

            iScreenWidth = Int16.Parse(job.ProjectCfg.screenWidth);
            iScreenHeight = Int16.Parse(job.ProjectCfg.screenHeight);

            readProjectRes(strProjectResLoc);
        }

        private string getJsonStringByKey(string strJson, string key)
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

            return  strVal;
        }

        private ArrayList getJsonProperties(string strJson)
        {
            ArrayList arrList = new ArrayList();

            JObject o = JObject.Parse(strJson);

            IEnumerable<JProperty> properties = o.Properties();

            foreach (JProperty item in properties)
            {
                arrList.Add(item.Name.ToString());
            }

            return arrList;
        }

        public void SetProjectTree(TreeView tv)
        {
            m_projectTree = tv;
        }

        private void readProjectRes(string strPath)
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

                decodeProject(newNode, NextFile.FullName);
            }
        }

        private void decodeProject(TreeNode parentNode, string filePath)
        {
            if (0 == filePath.CompareTo(""))
            {
                return;
            }

            if (false == isValidResourceFile(filePath))
            {
                return;
            }

            string file = getFileInfo(filePath);

            if (0 == file.CompareTo(""))
            {
                return;
            }

            // Decode file according json
            ArrayList arrList = getJsonProperties(file);

            if (true == isValidResourceFile(filePath))
            {
                foreach (string str in arrList)
                {
                    TreeNode newNode = new TreeNode(str);
                    parentNode.Nodes.Add(newNode);

                    if (parentNode.FullPath.EndsWith(".wgt"))
                    {
                        decodeWgtSkeleton(newNode, file, str);
                    }
                }
            }
  
        }

        private void decodeWgtSkeleton(TreeNode parentNode, string jsonInfo, string key)
        {
            if (0 == jsonInfo.CompareTo("") || 0 == key.CompareTo(""))
            {
                return;
            }

            string strInfo = getJsonStringByKey(jsonInfo, key);

            ArrayList arrList = getJsonProperties(strInfo);

            //if (true == isValidResourceFile(filePath))
            //{
                foreach (string str in arrList)
                {
                    TreeNode newNode = new TreeNode(str);
                    parentNode.Nodes.Add(newNode);
                }
            //}
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
        private bool isValidResourceFile(string filePath)
        {
            bool bRst = false;

            if (filePath.EndsWith(".wgt") || filePath.EndsWith(".tree") || filePath.EndsWith(".res"))
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
    }
}
