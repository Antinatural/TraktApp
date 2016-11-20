using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TraktApp.Data
{
    static class DataManager
    {
        public static async Task<IEnumerable<TraktMovie>> GetPopularMovies()
        {
            try
            {
                IEnumerable<TraktMovie> result = await TraktREST.GetPopularMovies();
                foreach (TraktMovie tm in result)
                    tm.Images = await GetMovieImagesList(tm.Ids.Imdb);
                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public static async Task<FanartMovieImages> GetMovieImagesList(string imdb_id)
        {
            try
            {
                FanartMovieImages result = await FanartREST.GetMovieImagesList(imdb_id);
                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

    }
}
