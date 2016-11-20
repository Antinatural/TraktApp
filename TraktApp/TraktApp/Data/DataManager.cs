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
