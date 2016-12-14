using System;
using System.Windows.Forms;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EOS
{
    class ProjectJosn
    {
        [JsonProperty(PropertyName = "project_info")]
        public ProjectInfoJson projectInfo { get; set; }

        [JsonProperty(PropertyName = "project_cfg")]
        public ProjectCfgJson projectCfg { get; set; }
    }

    class ProjectInfoJson
    {
        [JsonProperty(PropertyName = "project_name")]
        public string projectName { get; set; }

        [JsonProperty(PropertyName = "project_res")]
        public string projectLoc { get; set; }
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

        private void readProjFile(string strPath)
        {
            StreamReader sr = new StreamReader(strPath, System.Text.Encoding.Default);
            string line;

            string json = "";

            while ((line = sr.ReadLine()) != null)
            {
                json = json + line.Trim();
            }

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

            strProjectName   = job.projectInfo.projectName;
            strProjectResLoc = job.projectInfo.projectLoc;

            iScreenWidth = Int16.Parse(job.projectCfg.screenWidth);
            iScreenHeight = Int16.Parse(job.projectCfg.screenHeight);

            readProjectRes(strProjectResLoc);
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

            scanFolder(m_projectTree.Nodes, strPath);
        }

        private void scanFolder(TreeNodeCollection node, string strFolderPath)
        {
            DirectoryInfo TheFolder = new DirectoryInfo(strFolderPath);

            foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
            {
                TreeNode newNode = new TreeNode(NextFolder.Name);
                
                node.Add(newNode);
                scanFolder(newNode.Nodes, NextFolder.FullName);
            }

            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                node.Add(new TreeNode(NextFile.Name));
            }
        }
    }
}
