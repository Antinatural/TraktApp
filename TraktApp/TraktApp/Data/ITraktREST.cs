using Newtonsoft.Json.Linq;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TraktApp.Data
{

    [Headers("Accept: application/json", "trakt-api-version:2")]
    public interface ITraktREST
    {
        [Get("/movies/popular/?page={page}&limit={limit}&extended=full")]
        Task<List<TraktMovie>> GetPopularMovies([Header("trakt-api-key")] string client_id, int page, int limit);
    }
}
