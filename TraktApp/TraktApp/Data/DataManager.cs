using Akavache;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace TraktApp.Data
{
    static class DataManager
    {
        public static IObservable<List<TraktMovie>> GetPopularMovies(int page = 0, int limit = 10)
        {
            try
            {
                List<TraktMovie> result = default(List<TraktMovie>);
                var cache = BlobCache.LocalMachine.GetAndFetchLatest("PopularMovies_" + page,
                    () => GetPopularMoviesWithImages(page, limit), null, null);
                cache.Subscribe(rx => {
                    result = rx;
                });
                return cache;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return default(IObservable<List<TraktMovie>>);
            }
        }

        public static IObservable<List<TraktMovie>> GetFilteredMovies(string filter, int page = 0, int limit = 10)
        {
            try
            {
                List<TraktMovie> result = null;
                var cache = BlobCache.LocalMachine.GetAndFetchLatest("FilteredMoviesBy_" + filter.Trim() + "_" + page,
                    () => GetFilteredMoviesWithImages(filter, page, limit), null, null);
                cache.Subscribe(rx => {
                    result = rx;
                });
                return cache;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return default(IObservable<List<TraktMovie>>);
            }
        }

        private static async Task<FanartMovieImages> GetMovieImagesList(string imdb_id)
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

        private static async Task<List<TraktMovie>> GetPopularMoviesWithImages(int page, int limit)
        {
            var movies = await TraktREST.GetPopularMovies(page, limit);
            foreach (TraktMovie tm in movies)
                if (tm.Images == null)
                    tm.Images = await GetMovieImagesList(tm.Ids.Imdb);
            return movies;
        }

        private static async Task<List<TraktMovie>> GetFilteredMoviesWithImages(string filter, int page, int limit)
        {
            var result = await TraktREST.GetFilteredMovies(filter, page, limit);
            List<TraktMovie> movies = new List<TraktMovie>();
            foreach (TraktSearchResult tsr in result)
            {
                if (tsr.Movie.Images == null)
                    tsr.Movie.Images = await GetMovieImagesList(tsr.Movie.Ids.Imdb);
                movies.Add(tsr.Movie);
            }
            return movies;
        }

    }
}
