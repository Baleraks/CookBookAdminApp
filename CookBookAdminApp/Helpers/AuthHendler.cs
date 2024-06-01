using CookBookAdminApp.Services;

namespace CookBookAdminApp.Helpers
{
    public static class AuthHendler
    {
        private static IApiService _service;

        static AuthHendler()
        {
            _service = ApiService.Instance;
        }

        public static async Task<bool> RefreshAsync()
        {
            bool result = false;
            var response = await _service.RefreshAsync();
            if (response.Status == RestSharp.ResponseStatus.Completed)
            {
                Preferences.Default.Set(Constants.Texts.PreferencesAccessTokenenKey, response.Value.jwttoken);
                Preferences.Default.Set(Constants.Texts.PreferencesRefreshTokenKey, response.Value.refreshtoken);
                result = true;
            }
            else
            {
                await Shell.Current.GoToAsync("LogInPage");
            }

            return result;
        }
    }
}
