using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EOS
{
    class BitMapImgJson
    {
        [JsonProperty(PropertyName = "file_name")]
        public string FileName { get; set; }

        [JsonProperty(PropertyName = "width")]
        public string Width { get; set; }

        [JsonProperty(PropertyName = "height")]
        public string Height { get; set; }

        [JsonProperty(PropertyName = "format")]
        public string Format { get; set; }

        [JsonProperty(PropertyName = "compress")]
        public bool Compress { get; set; }

        [JsonProperty(PropertyName = "static_compiled")]
        public bool StaticCompiled { get; set; }

        [JsonProperty(PropertyName = "storage")]
        public string Storage { get; set; }

        [JsonProperty(PropertyName = "outline_image")]
        public string OutlineImage { get; set; }

        [JsonProperty(PropertyName = "outline_color")]
        public string OutlineColor { get; set; }

        [JsonProperty(PropertyName = "outline_width")]
        public string OutlineWidth { get; set; }
    }

    class BitmapImg
    {
        private string m_itemName;
        private BitMapImgJson jsonInfo = null;

        public BitmapImg()
        {
 
        }

        public string getItemName()
        {
            return m_itemName;
        }

        public void getItemName(string strItemName)
        {
            m_itemName = strItemName;
        }
    }
}
