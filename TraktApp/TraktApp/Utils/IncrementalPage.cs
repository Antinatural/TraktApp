using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TraktApp.Utils
{
    public abstract class IncrementalPage<T> : ContentPage where T : class
    {
        public SearchBar SearchBar { get; private set; }
        public ListView ListView { get; set; }
        public LoadingView LoadingView { get; private set; }

        public ObservableCollection<T> AllItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int RowsBeforeTheEndToLoad { get; set; }
        public string LastSearch { get; protected set; }
        public int SearchCount { get; protected set; }
        public bool HasMoreData { get; protected set; }
        public bool IsLoading { get; protected set; }

        protected IncrementalPage()
        {
            RowsBeforeTheEndToLoad = 10;
            LastSearch = string.Empty;
            SearchCount = 0;
            AllItems = new ObservableCollection<T>();

            SearchBar = new SearchBar { Placeholder = "Search for..." };
            SearchBar.TextChanged += async (sender, args) => await LoadSearch(SearchBar.Text);

            LoadingView = new LoadingView { IsVisible = false };
            LoadingView.SetBinding(LoadingView.IsShowingProperty,
                new Binding("IsBusy", source: this));

            ListView = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                SeparatorVisibility = SeparatorVisibility.Default,
                HasUnevenRows = true,
                ItemsSource = AllItems
            };
            ListView.ItemAppearing += async (sender, e) => {
                if (HasMoreData && !IsLoading)
                {
                    var foundIndex = AllItems.IndexOf(e.Item as T);
                    if (foundIndex == AllItems.Count - RowsBeforeTheEndToLoad)
                        await LoadNextPage();
                }
            };

            var layout = new RelativeLayout();
            layout.Children.Add(
                SearchBar,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent(p => p.Width),
                Constraint.Constant(50)
            );
            layout.Children.Add(
                ListView,
                Constraint.Constant(0),
                Constraint.Constant(50),
                Constraint.RelativeToParent(p => p.Width),
                Constraint.RelativeToParent(p => p.Height - 50)
            );
            layout.Children.Add(
                LoadingView,
                Constraint.RelativeToParent(p => p.Width - 100),
                Constraint.RelativeToParent(p => p.Height - 100),
                Constraint.Constant(90),
                Constraint.Constant(90)
            );
            Content = layout;
        }

        public async Task LoadSearch(string text)
        {
            SearchCount++;
            LastSearch = text;
            CurrentPage = 1;
            HasMoreData = true;
            AllItems.Clear();
            IsLoading = true;
            await DoLoad();
        }

        public async Task LoadFirstPage()
        {
            LastSearch = string.Empty;
            CurrentPage = 1;
            HasMoreData = true;
            AllItems.Clear();
            IsLoading = true;
            await DoLoad();
        }

        public async Task LoadNextPage()
        {
            CurrentPage++;
            await DoLoad();
        }

        private async Task DoLoad()
        {
            int LastSearchCount = SearchCount;
            IsBusy = true;
            var data = await LoadPage();
            if (LastSearchCount==SearchCount)
                foreach (T item in data)
                    AllItems.Add(item);
            IsBusy = false;
            IsLoading = false;
        }

        protected abstract Task<IEnumerable<T>> LoadPage();
    }
}
