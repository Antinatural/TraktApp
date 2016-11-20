using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TraktApp.Data;
using TraktApp.Utils;
using Xamarin.Forms;

namespace TraktApp
{
    public partial class MoviesPage : IncrementalPage<TraktMovie>
    {

        public MoviesPage()
        {
            InitializeComponent();
            ListView.ItemTemplate = (DataTemplate)Resources["MovieTemplate"];
            LoadFirstPage();//.IgnoreResult();
        }

        protected override IObservable<IEnumerable<TraktMovie>> LoadPage()
        {
            if (string.IsNullOrEmpty(LastSearch))
                return DataManager.GetPopularMovies(CurrentPage);
            else
                return DataManager.GetFilteredMovies(LastSearch, CurrentPage);
        }
    }
}
