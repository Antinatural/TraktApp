using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TraktApp.Data
{
    public class TraktMovie
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "year")]
        public int? Year { get; set; }

        [JsonProperty(PropertyName = "ids")]
        public TraktMovieIds Ids { get; set; }

        [JsonProperty(PropertyName = "images")]
        public FanartMovieImages Images { get; set; }

        [JsonProperty(PropertyName = "overview")]
        public string Overview { get; set; }

        public string TitleAndYear { get { return Title + " (" + Year + ")"; } }
    }
}
