using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Windows.Forms;

namespace EOS
{
    class TreeNodeJson
    {
        [JsonIgnore]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "take_focus")]
        public string TakeFocus { get; set; }

        [JsonProperty(PropertyName = "popup")]
        public string Popup { get; set; }

        [JsonProperty(PropertyName = "modal")]
        public string Modal { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string ID { get; set; }

        [JsonProperty(PropertyName = "static_compiled")]
        public string StaticCompiled { get; set; }

        [JsonProperty(PropertyName = "loc_x")]
        public string X { get; set; }

        [JsonProperty(PropertyName = "loc_y")]
        public string Y { get; set; }

        [JsonProperty(PropertyName = "width")]
        public string Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public string Height { get; set; }

        [JsonProperty(PropertyName = "resource")]
        public string[] Resource { get; set; }

        [JsonProperty(PropertyName = "init_timer")]
        public string InitTimer { get; set; }

        [JsonProperty(PropertyName = "handle")]
        public object Handle { get; set; }

        [JsonProperty(PropertyName = "children")]
        public string[] Children { get; set; }
    }

    class HandleJson
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "callback")]
        public string[] Callback { get; set; }
    }

    class CfgTree
    {
        public const string TREE_SUFFIX = ".tree";

        public CfgTree()
        {
 
        }

        public static bool IsTreeFileNode(TreeNode node)
        {
            bool bRst = false;

            if (null == node)
            {
                return false;
            }

            if (node.FullPath.EndsWith(TREE_SUFFIX))
            {
                bRst = true;
            }

            return bRst;
        }

        public static bool IsTreeItemNode(TreeNode node)
        {
            bool bRst = false;

            if (null == node || null == node.Parent)
            {
                return false;
            }

            if (node.Parent.FullPath.EndsWith(TREE_SUFFIX))
            {
                bRst = true;
            }

            return bRst;
        }

        public static TreeNodeJson GetTreeNodeContent(TreeNode node)
        {
            TreeNodeJson tnj = null;

            if (null == node || Common.IsStrEmpty(node.Text.ToString()))
            {
                return null;
            }

            tnj = TreeData.GetTreeNode(node.Text);

            return tnj;
        }
    }
}
