using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using TraktApp.Data;

namespace TraktApp.Data
{
    static class TraktREST
    {
        const string baseurl = "https://api.trakt.tv";
        const string client_id = "trakt_client_id_here";

        public static async Task<List<TraktMovie>> GetPopularMovies(int page, int limit)
        {
            try
            {
                var client = RestService.For<ITraktREST>(baseurl);
                var response = await client.GetPopularMovies(client_id, page, limit);
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }

        public static async Task<List<TraktSearchResult>> GetFilteredMovies(string filter, int page, int limit)
        {
            try
            {
                var client = RestService.For<ITraktREST>(baseurl);
                var response = await client.GetFilteredMovies(client_id, filter, page, limit);
                return response;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
    }
}
