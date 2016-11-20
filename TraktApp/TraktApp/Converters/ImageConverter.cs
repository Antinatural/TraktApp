using System;
using System.Globalization;
using TraktApp.Data;
using Xamarin.Forms;

namespace TraktApp.Converters
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string url = ((FanartMovieImages)value).movieposter[0].url;
                return url.Replace("/fanart/", "/preview/");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
