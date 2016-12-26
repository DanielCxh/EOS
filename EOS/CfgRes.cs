using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EOS
{
    class FontJson
    {
        [JsonIgnore]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "file_name")]
        public string FileName { get; set; }

        [JsonProperty(PropertyName = "width")]
        public string Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public string Height { get; set; }
    }

    class ColorJson
    {
        [JsonIgnore]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "format")]
        public string Format { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string[] Value { get; set; }
    }

    /// <summary>
    /// Resource file base info
    /// </summary>
    class CfgRes
    {
        public const string RES_SUFFIX = ".res";

        public const string RES_TYPE_FONT = "font";
        public const string RES_TYPE_COLOR = "color";
        public const string RES_TYPE_STRING = "string";

        public enum ResType
        {
            UNKNOW = 0,
            FONT = 1,
            COLOR = 2,
            STRING = 3
        }

        public CfgRes()
        {
 
        }

        static public string GetResTypeByEnum(ResType eType)
        {
            string strType = "";

            if (ResType.FONT == eType)
            {
                strType = RES_TYPE_FONT;
            }
            else if (ResType.COLOR == eType)
            {
                strType = RES_TYPE_COLOR;
            }
            else if (ResType.STRING == eType)
            {
                strType = RES_TYPE_STRING;
            }

            return strType;
        }

        static public ResType GetResNodeType(TreeNode node)
        {
            ResType eResType = ResType.UNKNOW;

            if (IsFontResNode(node))
            {
                eResType = ResType.FONT;
            }
            else if (IsColorResNode(node))
            {
                eResType = ResType.COLOR;
            }
            else if (IsStringResNode(node))
            {
                eResType = ResType.STRING;
            }

            return eResType;
        }

        static public bool IsResNode(TreeNode node)
        {
            bool bRst = false ;

            if (null == node || null == node.Parent)
            {
                return bRst;
            }

            if (node.Parent.FullPath.EndsWith(RES_SUFFIX))
            {
                if (node.FullPath.EndsWith(RES_TYPE_FONT)
                    || node.FullPath.EndsWith(RES_TYPE_COLOR)
                    || node.FullPath.EndsWith(RES_TYPE_STRING))
                {
                    bRst = true;
                }
            }

            return bRst;
        }

        static public bool IsFontResNode(TreeNode node)
        {
            bool bRst = false;

            if (null == node)
            {
                return false;
            }

            if (true == IsResNode(node) && node.FullPath.EndsWith(RES_TYPE_FONT))
            {
                bRst = true;
            }

            return bRst;
        }

        static public bool IsColorResNode(TreeNode node)
        {
            bool bRst = false;

            if (null == node)
            {
                return false;
            }

            if (true == IsResNode(node) && node.FullPath.EndsWith(RES_TYPE_COLOR))
            {
                bRst = true;
            }

            return bRst;
        }

        public static bool IsStringResNode(TreeNode node)
        {
            bool bRst = false;

            if (null == node)
            {
                return false;
            }

            if (true == IsResNode(node) && node.FullPath.EndsWith(RES_TYPE_STRING))
            {
                bRst = true;
            }

            return bRst;
        }
    }
}
