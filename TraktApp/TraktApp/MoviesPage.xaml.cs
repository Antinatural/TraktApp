using TraktApp.Data;
using Xamarin.Forms;

namespace TraktApp
{
    public partial class MoviesPage : ContentPage
    {
        public MoviesPage()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            var data = await DataManager.GetPopularMovies();
            BindingContext = data;
        }
    }
}
