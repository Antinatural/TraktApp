using Xamarin.Forms;

namespace TraktApp.Utils
{
    public class LoadingView : ContentView
    {
        public static readonly BindableProperty IsShowingProperty =
            BindableProperty.Create<LoadingView, bool>(mv => mv.IsShowing,
                false, BindingMode.OneWay, propertyChanging: OnShowingPropertyChanging);

        public bool IsShowing
        {
            get { return (bool)base.GetValue(IsShowingProperty); }
            set { base.SetValue(IsShowingProperty, value); }
        }

        static void OnShowingPropertyChanging(BindableObject bindable, bool oldValue, bool newValue)
        {
            Device.BeginInvokeOnMainThread(delegate {
                LoadingView mv = (LoadingView)bindable;
                mv.indicator.IsRunning = newValue;
                mv.IsVisible = newValue;
            });
        }

        readonly ActivityIndicator indicator;

        public LoadingView()
        {
            var layout = new StackLayout();
            indicator = new ActivityIndicator
            {
                HeightRequest = 40,
            };
            layout.Children.Add(indicator);
            var frame = new Frame()
            {
                Content = layout,
                BackgroundColor = Color.White,
                OutlineColor = Color.Silver,
                HasShadow = false
            };
            Content = frame;
        }
    }
}
