using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBookAdminApp.Helpers;
using RestSharp;

namespace CookBookAdminApp.Services
{
    internal class Api : IApi
    {
        #region Instance

        public static IApi Instance => _instance ??= new Api();

        private static IApi? _instance;

        #endregion Instance

        Api() { }

        public async Task<ResponseValue<string>> RegisterationAsync(string Nick, string Password)
        {
            ResponseValue<string> res = new();
            var regUrl = Constants.Texts.ServerUrl + "api/Register";
            CancellationTokenSource TokenSource = new CancellationTokenSource(5000);
            try
            {
                var client = ClientFactory.CreateClient(regUrl);
                var request = new RestRequest(regUrl, Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(new { Nick = Nick, HashPassword = Password, Isadmin = true });
                var response = await client.ExecuteAsync(request, TokenSource.Token);
                client.Dispose();
                res = await RequestHandler.HandlerAsync<string>(res, response);
            }
            catch (Exception e)
            {
                res.Exception = e.Message;
                res.Status = ResponseStatus.Error;
            }
            return res;
        }

        public async Task<ResponseValue<LoginResponse>> LoginAsync(string Nick, string Password)
        {
            ResponseValue<LoginResponse> res = new();
            var logUrl = Constants.Texts.ServerUrl + "api/Login";
            CancellationTokenSource TokenSource = new CancellationTokenSource(5000);
            try
            {
                var client = ClientFactory.CreateClient(logUrl);
                var request = new RestRequest(logUrl, Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(new { Nick = Nick, HashPassword = Password });
                var response = await client.ExecuteAsync(request, TokenSource.Token);
                client.Dispose();
                res = await RequestHandler.HandlerAsync<LoginResponse>(res, response);
            }
            catch (Exception e)
            {
                res.Exception = e.Message;
                res.Status = ResponseStatus.Error;
            }
            return res;
        }

        public async Task<ResponseValue<ShortRecipeResponse[]>> GetRecipesAsync(int offset, int count, string[] tags, string name)
        {
            ResponseValue<ShortRecipeResponse[]> res = new();
            var logUrl = Constants.Texts.ServerUrl + "api/GetRecipesByReports";
            CancellationTokenSource TokenSource = new CancellationTokenSource(5000);
            try
            {
                var client = ClientFactory.CreateClient(logUrl);
                var request = new RestRequest(logUrl, Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(new
                {
                    offset = offset,
                    count = count,
                    tags = tags,
                    recipeName = name
                });
                var response = await client.ExecuteAsync(request, TokenSource.Token);
                client.Dispose();
                res = await RequestHandler.HandlerAsync<ShortRecipeResponse[]>(res, response);
            }
            catch (Exception e)
            {
                res.Exception = e.Message;
                res.Status = ResponseStatus.Error;
            }
            return res;
        }

        public async Task<ResponseValue<RecipeResponse>> GetRecipeByIdAsync(int id)
        {
            ResponseValue<RecipeResponse> res = new();
            var logUrl = Constants.Texts.ServerUrl + $"api/GetRecipe/{id}";
            CancellationTokenSource TokenSource = new CancellationTokenSource(5000);
            try
            {
                var client = ClientFactory.CreateClient(logUrl);
                var request = new RestRequest(logUrl, Method.Get);
                var response = await client.ExecuteAsync(request, TokenSource.Token);
                client.Dispose();
                res = await RequestHandler.HandlerAsync<RecipeResponse>(res, response);
                TokenSource.Dispose();
            }
            catch (Exception e)
            {
                res.Exception = e.Message;
                res.Status = ResponseStatus.Error;
            }
            return res;
        }

        public async Task<ResponseValue<LoginResponse>> RefreshAsync()
        {
            ResponseValue<LoginResponse> res = new();
            var logUrl = Constants.Texts.ServerUrl + "api/RefreshToken";
            CancellationTokenSource TokenSource = new CancellationTokenSource(5000);
            try
            {
                var client = ClientFactory.CreateClient(logUrl);
                var request = new RestRequest(logUrl, Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(new
                {
                    jwttoken = Preferences.Default.Get(Constants.Texts.PreferencesAccessTokenenKey, ""),
                    refreshtoken = Preferences.Default.Get(Constants.Texts.PreferencesRefreshTokenKey, "")
                });
                var response = await client.ExecuteAsync(request, TokenSource.Token);
                client.Dispose();
                res = await RequestHandler.HandlerAsync<LoginResponse>(res, response);
            }
            catch (Exception e)
            {
                res.Exception = e.Message;
                res.Status = ResponseStatus.Error;
            }
            return res;
        }

        public async Task<ResponseValue<bool>> DeleteRecipeAsync(int recipeId)
        {
            ResponseValue<bool> res = new();
            var logUrl = Constants.Texts.ServerUrl + $"api/DeleteRecipe";
            CancellationTokenSource TokenSource = new CancellationTokenSource(5000);
            try
            {
                var client = ClientFactory.CreateClient(logUrl);
                var request = new RestRequest(logUrl, Method.Delete);
                request.AddHeader("Authorization", "Bearer " + Preferences.Default.Get(Constants.Texts.PreferencesAccessTokenenKey, ""));
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(new
                {
                    Id = recipeId,
                    UserId = Preferences.Default.Get(Constants.Texts.PreferencesUserIdKey, -1)
                });
                var response = await client.ExecuteAsync(request, TokenSource.Token);
                client.Dispose();
                res = await RequestHandler.HandlerAsync<bool>(res, response);
                TokenSource.Dispose();
            }
            catch (Exception e)
            {
                res.Exception = e.Message;
                res.Status = ResponseStatus.Error;
            }
            return res;
        }

        public async Task<ResponseValue<CommentResponse[]>> GetCommentsByRecIdAsync(int recipeId)
        {
            ResponseValue<CommentResponse[]> res = new();
            var logUrl = Constants.Texts.ServerUrl + $"api/GetComment/{recipeId}";
            CancellationTokenSource TokenSource = new CancellationTokenSource(5000);
            try
            {
                var client = ClientFactory.CreateClient(logUrl);
                var request = new RestRequest(logUrl, Method.Get);
                var response = await client.ExecuteAsync(request, TokenSource.Token);
                client.Dispose();
                res = await RequestHandler.HandlerAsync<CommentResponse[]>(res, response);
                TokenSource.Dispose();
            }
            catch (Exception e)
            {
                res.Exception = e.Message;
                res.Status = ResponseStatus.Error;
            }
            return res;
        }

        public async Task<ResponseValue<bool>> DeleteCommentAsync(int commentId)
        {
            ResponseValue<bool> res = new();
            var logUrl = Constants.Texts.ServerUrl + $"api/DeleteComment";
            CancellationTokenSource TokenSource = new CancellationTokenSource(5000);
            try
            {
                var client = ClientFactory.CreateClient(logUrl);
                var request = new RestRequest(logUrl, Method.Delete);
                request.AddHeader("Authorization", 
                    "Bearer " + Preferences.Default.Get(Constants.Texts.PreferencesAccessTokenenKey, ""));
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(new
                {
                    Id = commentId,
                    UserId = Preferences.Default.Get(Constants.Texts.PreferencesUserIdKey, -1)
                });
                var response = await client.ExecuteAsync(request, TokenSource.Token);
                client.Dispose();
                res = await RequestHandler.HandlerAsync(res, response);
                TokenSource.Dispose();
            }
            catch (Exception e)
            {
                res.Exception = e.Message;
                res.Status = ResponseStatus.Error;
            }
            return res;
        }

        public async Task<ResponseValue<byte[]>> GetImageBytesAsync(string imageUrl)
        {
            var res = new ResponseValue<byte[]>();
            try
            {
                var client = ClientFactory.CreateClient(imageUrl);
                var imageBytes = await client.DownloadDataAsync(new RestRequest(imageUrl));
                client.Dispose();
                res.Value = imageBytes;
                res.Status = ResponseStatus.Completed;
            }
            catch (Exception e)
            {
                res.Exception = e.Message;
                res.Status = ResponseStatus.Error;
            }
            return res;
        }
    }
}
