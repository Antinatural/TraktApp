using Newtonsoft.Json;
using System;

namespace TraktApp.Data
{
    public class TraktMovieIds
    {
        [JsonProperty(PropertyName = "trakt")]
        public string Trakt { get; set; }

        [JsonProperty(PropertyName = "slug")]
        public string Slug { get; set; }

        [JsonProperty(PropertyName = "imdb")]
        public string Imdb { get; set; }

        [JsonProperty(PropertyName = "tmdb")]
        public string Tmdb { get; set; }

    }
}
