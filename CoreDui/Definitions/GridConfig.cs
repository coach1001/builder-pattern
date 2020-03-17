using System.Collections.Generic;
using CoreDui.Enums;
using Newtonsoft.Json;

namespace CoreDui.Definitions
{
    public class GridConfig
    {        
        public string AreaName { get; set; } = null;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, SpanConfig> SpanConfig { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, TrackConfig> TrackConfig { get; set; }
    }

    public class SpanConfig
    {
        public int Columns { get; set; } = 1;
        public int Rows { get; set; } = 1;
    }

    public class TrackConfig
    {
        public string Columns { get; set; } = "1fr";
        public string Rows { get; set; } = "1fr";
    }

}
