using CookBookAdminApp.Helpers;
using CookBookAdminApp.Model;
using CookBookAdminApp.Services;
using RestSharp;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace CookBookAdminApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ICommand SelectRecipeCommand { get; }
        public ICommand RemainingItemseachedCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand SearchCommand { get; }


        public ObservableCollection<Recipe> RecipesList { get; set; }
        public bool IsBusy { get; set; }
        public bool IsRefreshing { get; set; } = false;

        private List<Tag> _tags = new List<Tag>();
        private readonly IApiService _service;
        private string _text;
        private Page _page;

        public MainViewModel(Page page)
        {
            RecipesList = new();
            _service = ApiService.Instance;
            _text = "";
            _page = page;
            SelectRecipeCommand = new Command<Recipe>(async (r) => await SelectRecipeAsync(r));
            RemainingItemseachedCommand = new Command(async () => await UpdateList());
            RefreshCommand = new Command(async () => await Refresh());
            SearchCommand = new Command<string>(async (text) => await Search(text));
        }

        public bool IsDataLoading { get; private set; } = false;
        public bool IsEnd { get; private set; } = false;

        public async Task UpdateList()
        {
            if (!_isInitialized || IsDataLoading || IsEnd) { return; }

            IsDataLoading = true;

            var tagsName = new List<string>();
            foreach (var tag in _tags)
            {
                tagsName.Add(tag._Tag);
            }
            var taskRecipesApi =
                _service.GetRecipesAsync(_from, _to, tagsName.ToArray(), _text);
            IsBusy = true;
            var recipesApi = await taskRecipesApi;
            if (!await Validate(recipesApi)) { return; }
            _from += _to;
            foreach (var item in recipesApi.Value)
            {
                var recipe = await Converter.SortRecipeResponseToRecipe(item);
                await MainThread.InvokeOnMainThreadAsync(() => RecipesList.Add(recipe));
            }
            IsDataLoading = false;
            IsBusy = false;
        }

        private async Task Refresh()
        {
            IsEnd = false;
            _from = 0;
            await MainThread.InvokeOnMainThreadAsync(() => RecipesList.Clear());
            await UpdateList();
            IsBusy = false;
            IsRefreshing = false;
        }

        private bool _isInitialized;
        private int _from = 0;
        private const int _to = 10;

        public async Task InitAsync()
        {
            if (_isInitialized) { return; }

            var tagsName = new List<string>();
            foreach (var tag in _tags)
            {
                tagsName.Add(tag._Tag);
            }
            var taskRecipesApi
                = _service.GetRecipesAsync(_from, _to, tagsName.ToArray(), _text);
            IsBusy = true;
            var recipesApi = await taskRecipesApi;
            if (!await Validate(recipesApi))
            {
                return;
            }
            _from += _to;
            foreach (var item in recipesApi.Value)
            {
                var recipe = await Converter.SortRecipeResponseToRecipe(item);
                await MainThread.InvokeOnMainThreadAsync(() => RecipesList.Add(recipe));
            }

            IsBusy = false;
            _isInitialized = true;
        }

        private async Task<bool> Validate(ResponseValue<ShortRecipeResponse[]> recipesApi)
        {
            if (recipesApi.Status == RestSharp.ResponseStatus.Error &&
                recipesApi.Exception.Contains("Value cannot be null. (Parameter 'json')"))
            {
                await _page.DisplayAlert(Constants.Texts.PopUpTitle, Constants.Texts.ToastConnection, Constants.Texts.PopUpCancel);
                IsBusy = false;
                _isInitialized = true;
                IsDataLoading = false;
                return false;
            }
            else if (recipesApi.Status == ResponseStatus.Error &&
                     recipesApi.Exception.Contains("Offset is out of range"))
            {
                IsEnd = true;
                IsBusy = false;
                _isInitialized = true;
                IsDataLoading = false;
                return false;
            }
            return true;
        }

        private async Task Search(string text)
        {
            _text = text;
            await Refresh();
        }

        private async Task SelectRecipeAsync(Recipe recipe)
        {
            await Shell.Current.GoToAsync($"RecipePage?ItemId={recipe.Id.ToString()}");
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
