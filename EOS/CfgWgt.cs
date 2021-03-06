﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Windows.Forms;

namespace EOS
{
    /*---------------------------------------------------------------------------------*/
    /*                                  bitmap_image                                   */
    /*---------------------------------------------------------------------------------*/
    class BitmapImgJson
    {
        [JsonIgnore]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "file_name")]
        public string FileName { get; set; }

        [JsonProperty(PropertyName = "width")]
        public string Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public string Height { get; set; }

        [JsonProperty(PropertyName = "format")]
        public string Format { get; set; }

        [JsonProperty(PropertyName = "compress")]
        public string Compress { get; set; }

        [JsonProperty(PropertyName = "static_compiled")]
        public string StaticCompiled { get; set; }

        [JsonProperty(PropertyName = "storage")]
        public string Storage { get; set; }

        [JsonProperty(PropertyName = "outline_image")]
        public string OutlineImage { get; set; }

        [JsonProperty(PropertyName = "outline_color")]
        public string OutlineColor { get; set; }

        [JsonProperty(PropertyName = "outline_width")]
        public string OutlineWidth { get; set; }
    }

    /*---------------------------------------------------------------------------------*/
    /*                                   solid_image                                   */
    /*---------------------------------------------------------------------------------*/
    class SolidImgJson
    {
        [JsonIgnore]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "fill_color")]
        public string FillColor { get; set; }

        [JsonProperty(PropertyName = "width")]
        public string Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public string Height { get; set; }

        [JsonProperty(PropertyName = "outline_color")]
        public string OutlineColor { get; set; }

        [JsonProperty(PropertyName = "outline_width")]
        public string OutlineWidth { get; set; }
    }

    /*---------------------------------------------------------------------------------*/
    /*                                    text_box                                     */
    /*---------------------------------------------------------------------------------*/
    class TextBoxJson
    {
        [JsonIgnore]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "font")]
        public string Font { get; set; }

        [JsonProperty(PropertyName = "x_alignment")]
        public string AlignmentX { get; set; }

        [JsonProperty(PropertyName = "y_alignment")]
        public string AlignmentY { get; set; }

        [JsonProperty(PropertyName = "indent")]
        public string Indent { get; set; }

        [JsonProperty(PropertyName = "text_color")]
        public string TextColor { get; set; }

        [JsonProperty(PropertyName = "background")]
        public string Background { get; set; }

        [JsonProperty(PropertyName = "keep_last_text")]
        public string KeepLastText { get; set; }

        [JsonProperty(PropertyName = "show_more_indicator")]
        public string ShowMoreIndicator { get; set; }
    }

    /*---------------------------------------------------------------------------------*/
    /*                                    push_button                                  */
    /*---------------------------------------------------------------------------------*/
    class PushButtonJson
    {
        [JsonIgnore]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "state_non_focus")]
        public string StateNonFocus { get; set; }

        [JsonProperty(PropertyName = "state_focus")]
        public string StateFocus { get; set; }

        [JsonProperty(PropertyName = "state_inactive_focus")]
        public string StateInactiveFocus { get; set; }

        [JsonProperty(PropertyName = "state_pushed")]
        public string StatePushed { get; set; }

        [JsonProperty(PropertyName = "state_customer1")]
        public string StateCustomer1 { get; set; }

        [JsonProperty(PropertyName = "state_customer2")]
        public string StateCustomer2 { get; set; }
    }

    /*---------------------------------------------------------------------------------*/
    /*                                     scroll_bar                                  */
    /*---------------------------------------------------------------------------------*/
    class ScrollBarJson
    {
        [JsonIgnore]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "scroll_box")]
        public string ScrollBox { get; set; }

        [JsonProperty(PropertyName = "scroll_offset")]
        public string ScrollOffset { get; set; }

        [JsonProperty(PropertyName = "background")]
        public string Background { get; set; }

        [JsonProperty(PropertyName = "orientation")]
        public string Orientation { get; set; }
    }

    /*---------------------------------------------------------------------------------*/
    /*                                    schedule_bar                                 */
    /*---------------------------------------------------------------------------------*/
    class ScheduleBarJson
    {
        [JsonIgnore]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "foreground")]
        public string Foreground { get; set; }

        [JsonProperty(PropertyName = "background")]
        public string Background { get; set; }

        [JsonProperty(PropertyName = "process_offset")]
        public string ProcestsOffse { get; set; }

        [JsonProperty(PropertyName = "discrate")]
        public string Discrate { get; set; }

        [JsonProperty(PropertyName = "orientation")]
        public string Orientation { get; set; }
    }

    class CfgWgt
    {
        public const string WGT_SUFFIX = ".wgt";

        public const string WGT_NODE_TYPE_BITMAP_IMG = "bitmap_image";
        public const string WGT_NODE_TYPE_SOLID_IMG = "solid_image";
        public const string WGT_NODE_TYPE_TEXT_BOX = "text_box";
        public const string WGT_NODE_TYPE_PUSH_BUTTON = "push_button";
        public const string WGT_NODE_TYPE_SCROLL_BAR = "scroll_bar";
        public const string WGT_NODE_TYPE_SCHEDULE_BAR = "schedule_bar";

        public enum WgtType
        {
            UNKNOW = 0,
            BITMAP_IMG = 1,
            SOLID_IMG = 2,
            TEXT_BOX = 3,
            PUSH_BUTTON = 4,
            SCROLL_BAR = 5,
            SCHEDULE_BAR = 6
        }

        public CfgWgt()
        {
 
        }

        static public WgtType GetWgtNodeType(TreeNode node)
        {
            WgtType eType = WgtType.UNKNOW;

            if (IsBitmapImgWgtNode(node))
            {
                eType = WgtType.BITMAP_IMG;
            }
            else if (IsSolidImgWgtNode(node))
            {
                eType = WgtType.SOLID_IMG;
            }
            else if (IsTextBoxWgtNode(node))
            {
                eType = WgtType.TEXT_BOX;
            }
            else if (IsPushButtonWgtNode(node))
            {
                eType = WgtType.PUSH_BUTTON;
            }
            else if (IsScrollBarWgtNode(node))
            {
                eType = WgtType.SCROLL_BAR;
            }
            else if (IsScheduleBarWgtNode(node))
            {
                eType = WgtType.SCHEDULE_BAR;
            }

            return eType;
        }

        static public WgtType GetWgtNodeTypeByStr(string property)
        {
            WgtType eType = WgtType.UNKNOW;

            if (0 == property.CompareTo(""))
            {
                return WgtType.UNKNOW;
            }

            if (0 == property.CompareTo(WGT_NODE_TYPE_BITMAP_IMG))
            {
                eType = WgtType.BITMAP_IMG;
            }

            return eType;
        }

        static public bool IsWgtFileNode(TreeNode node)
        {
            bool bRst = false;

            if (null == node)
            {
                return false;
            }

            if (node.FullPath.EndsWith(".wgt"))
            {
                bRst = true;
            }

            return bRst;
        }

        public static bool IsWgtNode(TreeNode node)
        {
            bool bRst = false;

            if (null == node || null == node.Parent || null == node.Parent.Parent)
            {
                return false;
            }

            if (node.Parent.Parent.FullPath.EndsWith(WGT_SUFFIX))
            {
                if (node.Parent.FullPath.EndsWith(WGT_NODE_TYPE_BITMAP_IMG)
                    || node.Parent.FullPath.EndsWith(WGT_NODE_TYPE_SOLID_IMG)
                    || node.Parent.FullPath.EndsWith(WGT_NODE_TYPE_TEXT_BOX)
                    || node.Parent.FullPath.EndsWith(WGT_NODE_TYPE_PUSH_BUTTON)
                    || node.Parent.FullPath.EndsWith(WGT_NODE_TYPE_SCROLL_BAR)
                    || node.Parent.FullPath.EndsWith(WGT_NODE_TYPE_SCHEDULE_BAR))
                {
                    bRst = true;
                }
            }

            return bRst;
        }

        static public bool IsBitmapImgWgtNode(TreeNode node)
        {
            bool bRst = false;

            if (true == IsWgtNode(node)
                && node.Parent.FullPath.EndsWith(WGT_NODE_TYPE_BITMAP_IMG))
            {
                bRst = true;
            }

            return bRst;
        }

        static public bool IsSolidImgWgtNode(TreeNode node)
        {
            bool bRst = false;

            if (true == IsWgtNode(node)
               && node.Parent.FullPath.EndsWith(WGT_NODE_TYPE_SOLID_IMG))
            {
                bRst = true;
            }

            return bRst;
        }

        static public bool IsTextBoxWgtNode(TreeNode node)
        {
            bool bRst = false;

            if (true == IsWgtNode(node)
               && node.Parent.FullPath.EndsWith(WGT_NODE_TYPE_TEXT_BOX))
            {
                bRst = true;
            }

            return bRst;
        }

        static public bool IsPushButtonWgtNode(TreeNode node)
        {
            bool bRst = false;

            if (true == IsWgtNode(node)
               && node.Parent.FullPath.EndsWith(WGT_NODE_TYPE_PUSH_BUTTON))
            {
                bRst = true;
            }

            return bRst;
        }

        static public bool IsScrollBarWgtNode(TreeNode node)
        {
            bool bRst = false;

            if (true == IsWgtNode(node)
               && node.Parent.FullPath.EndsWith(WGT_NODE_TYPE_SCROLL_BAR))
            {
                bRst = true;
            }

            return bRst;
        }

        static public bool IsScheduleBarWgtNode(TreeNode node)
        {
            bool bRst = false;

            if (true == IsWgtNode(node)
               && node.Parent.FullPath.EndsWith(WGT_NODE_TYPE_SCHEDULE_BAR))
            {
                bRst = true;
            }

            return bRst;
        }

        public static object GetWgtNodeContent(TreeNode node)
        {
            object obj = null;

            string strFileTag = "";
            string property = "";
            string nodeName = "";

            if (IsBitmapImgWgtNode(node))
            {
                strFileTag = ProjMgt.GetInstance().GetNodeFullPath(node.Parent.Parent);
                property = node.Parent.Text;
                nodeName = node.Text;
            }

            obj = WgtData.GetWgtContent(strFileTag, property, nodeName);

            return obj;
        }
    }
}
