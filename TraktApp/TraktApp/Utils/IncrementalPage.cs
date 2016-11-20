using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public bool IsLoading { get; protected set; }

        protected IncrementalPage()
        {
            RowsBeforeTheEndToLoad = 10;
            LastSearch = string.Empty;
            SearchCount = 0;
            AllItems = new ObservableCollection<T>();

            SearchBar = new SearchBar { Placeholder = "Search for..." };
            SearchBar.TextChanged += (sender, args) => LoadSearch(SearchBar.Text);

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
            ListView.ItemAppearing += (sender, e) => {
                if (!IsLoading)
                {
                    var foundIndex = AllItems.IndexOf(e.Item as T);
                    if (foundIndex == AllItems.Count - RowsBeforeTheEndToLoad)
                        LoadNextPage();
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

        public void LoadSearch(string text)
        {
            SearchCount++;
            LastSearch = text;
            CurrentPage = 1;
            AllItems.Clear();
            IsLoading = true;
            DoLoad();
        }

        public void LoadFirstPage()
        {
            LastSearch = string.Empty;
            CurrentPage = 1;
            AllItems.Clear();
            IsLoading = true;
            DoLoad();
        }

        public void LoadNextPage()
        {
            CurrentPage++;
            DoLoad();
        }

        private void DoLoad()
        {
            int LastSearchCount = SearchCount;
            IsBusy = true;
            
            IEnumerable<T> result = default(IEnumerable<T>);
            var data = LoadPage();
            data.Subscribe(rx =>
            {
                result = rx;
                if (result!=null && LastSearchCount == SearchCount)
                    foreach (T item in result)
                        AllItems.Add(item);
                IsBusy = false;
                IsLoading = false;
            });
        }

        protected abstract IObservable<IEnumerable<T>> LoadPage();
    }
}
