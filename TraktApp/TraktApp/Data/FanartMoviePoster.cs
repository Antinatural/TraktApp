using Newtonsoft.Json;

namespace TraktApp.Data
{
    public class FanartMoviePoster
    {
        [JsonProperty]
        public string id { get; set; }

        [JsonProperty]
        public string lang { get; set; }

        [JsonProperty]
        public string likes { get; set; }

        [JsonProperty]
        public string url { get; set; }
    }
}
