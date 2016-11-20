using Refit;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TraktApp.Data
{
    static class FanartREST
    {
        const string baseurl = "http://webservice.fanart.tv";
        const string api_key = "fanart_api_key_here";

        public static async Task<FanartMovieImages> GetMovieImagesList(string imdb_id)
        {
            try
            {
                var client = RestService.For<IFanartREST>(baseurl);
                var response = await client.GetMovieImagesList(imdb_id, api_key);
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
