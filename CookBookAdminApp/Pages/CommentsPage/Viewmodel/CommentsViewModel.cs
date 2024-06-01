using CookBookAdminApp.Helpers;
using CookBookAdminApp.Model;
using CookBookAdminApp.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace CookBookAdminApp
{
    internal class CommentsViewModel : INotifyPropertyChanged
    {
        public bool IsBusy { get; set; }
        public ObservableCollection<CommentComposite> Comments { get; } = new();
        public bool IsDataLoading { get; set; }
        public bool IsRefreshing { get; set; }

        public ICommand RefreshCommand { get; }

        private readonly IApiService _service;
        private readonly int _recipeId;
        private bool _isInitialized;
        private Page _page;

        public CommentsViewModel(string RecipeId, Page page)
        {
            _service = ApiService.Instance;
            _recipeId = int.Parse(RecipeId);
            _isInitialized = false;
            RefreshCommand = new Command(async () => await RefreshAsync());
            _page = page;
        }

        public async Task RefreshAsync()
        {
            if (!_isInitialized)
            {
                return;
            }
            var getRequestTask = _service.GetCommentsByRecIdAsync(_recipeId);
            IsBusy = true;
            var response = await getRequestTask;
            if (!await Validate(response))
            {
                IsBusy = false;
                return;
            }
            var value = Converter.CommentResponseToCommentComponent(response.Value);
            foreach (var comment in value)
            {
                await MainThread.InvokeOnMainThreadAsync(() => Comments.Add(comment));
            }
            IsBusy = false;
        }

        public async Task InitAsync()
        {
            if (_isInitialized)
            {
                return;
            }
            var getRequestTask = _service.GetCommentsByRecIdAsync(_recipeId);
            IsBusy = true;
            var response = await getRequestTask;
            if (!await Validate(response))
            {
                IsBusy = false;
                return;
            }
            var value = Converter.CommentResponseToCommentComponent(response.Value);
            foreach (var comment in value)
            {
                await MainThread.InvokeOnMainThreadAsync(() => Comments.Add(comment));
            }
            IsBusy = false;
            _isInitialized = true;
        }

        private async Task<bool> Validate<T>(ResponseValue<T> recipesApi)
        {
            if (recipesApi.Status == RestSharp.ResponseStatus.Error && recipesApi.Exception.Contains("Value cannot be null. (Parameter 'json')"))
            {
                await _page.DisplayAlert(Constants.Texts.PopUpTitle, Constants.Texts.ToastConnection, Constants.Texts.PopUpCancel);
                IsBusy = false;
                _isInitialized = true;
                IsDataLoading = false;
                return false;
            }
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
