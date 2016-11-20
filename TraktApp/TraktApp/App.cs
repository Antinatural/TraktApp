using Akavache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace TraktApp
{
    public class App : Application
    {
        public App()
        {
            BlobCache.ApplicationName = "TraktAppCache";
            MainPage = new NavigationPage(new MoviesPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
