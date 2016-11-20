using Refit;
using System.Threading.Tasks;

namespace TraktApp.Data
{
    [Headers("Accept: application/json")]
    public interface IFanartREST
    {
        [Get("/v3/movies/{imdb_id}?api_key={api_key}")]
        Task<FanartMovieImages> GetMovieImagesList(string imdb_id, string api_key);
    }
}
