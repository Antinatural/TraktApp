using Newtonsoft.Json;
using System.Collections.Generic;

namespace TraktApp.Data
{
    public class FanartMovieImages
    {
        [JsonProperty]
        public List<FanartMoviePoster> movieposter { get; set; }
        [JsonProperty]
        public string name { get; set; }
        [JsonProperty]
        public string tmdb_id { get; set; }

    }
}
