using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CookBookAdminApp.Helpers;
using CookBookAdminApp.Services;
using Microsoft.Maui.Controls;

namespace CookBookAdminApp
{
    internal class LoginViewModel: INotifyPropertyChanged
    {

        public string Nick { get; set; }
        public string Password { get; set; }
        public bool IsBusy { get; set; } = false;

        public ICommand LoginCommand { get; set; }
        public ICommand GoToRegister { get; set; }

        private IApiService _service;
        private Page _page;

        public LoginViewModel(Page page)
        {
            Nick = "";
            Password = "";
            _service = ApiService.Instance;
            GoToRegister = new Command(async () => await GoToRegistrationPageAsync());
            LoginCommand = new Command(Login);
            _page = page;
        }

        private async void Login()
        {
            var text = "";
            var loginTask = _service.LoginAsync(Nick, Password);
            IsBusy = true;
            var response = await loginTask;
            if (response.Status == RestSharp.ResponseStatus.Error && response.Exception.Contains("Value cannot be null. (Parameter 'json')"))
            {
                text = Constants.Texts.ToastConnection;
                await _page.DisplayAlert(Constants.Texts.PopUpTitle, text, Constants.Texts.PopUpCancel);
                IsBusy = false;
                return;
            }
            else if (response.Status == RestSharp.ResponseStatus.Error && response.Exception.Contains("Password is incorrect"))
            {
                text = Constants.Texts.ToastLoginValidation;
                await _page.DisplayAlert(Constants.Texts.PopUpTitle, text, Constants.Texts.PopUpCancel);
                IsBusy = false;
                return;
            }
            else if (response.Status == RestSharp.ResponseStatus.Error && response.Exception.Contains("User not found"))
            {
                text = Constants.Texts.ToastLoginValidation;
                await _page.DisplayAlert(Constants.Texts.PopUpTitle, text, Constants.Texts.PopUpCancel);
                IsBusy = false;
                return;
            }
            IsBusy = false;
            Preferences.Default.Set(Constants.Texts.PreferencesUserIdKey, response.Value.id);
            Preferences.Default.Set(Constants.Texts.PreferencesAccessTokenenKey, response.Value.jwttoken);
            Preferences.Default.Set(Constants.Texts.PreferencesRefreshTokenKey, response.Value.refreshtoken);
            await Shell.Current.GoToAsync("MainRout");
        }

        private async Task GoToRegistrationPageAsync()
        {
            await Shell.Current.GoToAsync($"RegistrationPage");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
