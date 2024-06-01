using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CookBookAdminApp.Helpers;
using CookBookAdminApp.Services;

namespace CookBookAdminApp
{
    internal class RegistrationViewModel : INotifyPropertyChanged
    {
        private IApiService _service;
        private Page _page;

        public ICommand RegistrationCommand { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string RepPassword { get; set; }
        public bool IsBusy { get; set; }

        readonly char[] UnacceptableSymbols =
        [
            ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', ':', ';', '<', '=', '>',
            '?', '@', '[', '\\', ']', '^', '_', '`', '{', '|', '}', '~'
        ];


        public RegistrationViewModel(Page page)
        {
            _service = ApiService.Instance;
            RegistrationCommand = new Command(Registration);
            _page = page;
        }

        private async void Registration()
        {
            string text = "";
            if (Validation())
            {
                var taskResponse = _service.RegisterationAsync(Login, Password);
                IsBusy = true;
                var response = await taskResponse;
                IsBusy = false;
                if (response.Status == RestSharp.ResponseStatus.Error && String.IsNullOrEmpty(response.Exception))
                {
                    text = Constants.Texts.ToastConnection;
                    await _page.DisplayAlert(Constants.Texts.PopUpTitle, text, Constants.Texts.PopUpCancel);
                    return;
                }
                else if (response.Status == RestSharp.ResponseStatus.Error &&
                         response.Exception.Contains("Invalid Credentials"))
                {
                    text = Constants.Texts.ToastRegLoginValidation;
                    await _page.DisplayAlert(Constants.Texts.PopUpTitle, text, Constants.Texts.PopUpCancel);
                    return;
                }

                text = Constants.Texts.ToastRegSuccess;
                await _page.DisplayAlert(Constants.Texts.PopUpTitle, text, Constants.Texts.PopUpCancel);
                await Shell.Current.GoToAsync("..");
            }
            else if (Password != RepPassword)
            {
                text = Constants.Texts.ToastRegPasswordCompare;
                await _page.DisplayAlert(Constants.Texts.PopUpTitle, text, Constants.Texts.PopUpCancel);
            }
            else if (Password.Trim().Length < 8)
            {
                text = Constants.Texts.ToastRegPasswordValidationCount;
                await _page.DisplayAlert(Constants.Texts.PopUpTitle, text, Constants.Texts.PopUpCancel);
            }
            else if (Login.Length < 5)
            {
                text = Constants.Texts.ToastRegLoginCount;
                await _page.DisplayAlert(Constants.Texts.PopUpTitle, text, Constants.Texts.PopUpCancel);
            }
            else
            {
                text = Constants.Texts.ToastRegPasswordValidation;
                await _page.DisplayAlert(Constants.Texts.PopUpTitle, text, Constants.Texts.PopUpCancel);
            }
        }

        private bool Validation()
        {
            var res = Password == RepPassword && !String.IsNullOrEmpty(Password) && Password.Trim().Length >= 8;
            for (int i = 0; i < UnacceptableSymbols.Length; i++)
            {
                if (!res) break;
                res = !Password.Contains(UnacceptableSymbols[i]);
                if (!res) break;
                res = !Login.Contains(UnacceptableSymbols[i]);
            }

            res = Login.Length >= 5;
            return res;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
