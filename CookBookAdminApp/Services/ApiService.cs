using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBookAdminApp.Helpers;

namespace CookBookAdminApp.Services
{
    internal class ApiService : IApiService
    {
        #region Instance

        public static IApiService Instance => _instance ??= new ApiService();

        private static IApiService? _instance;

        #endregion Instance

        private static IApi _api;

        private ApiService(){}

        static ApiService()
        {
            _api = Api.Instance;
        }

        public async Task<ResponseValue<RecipeResponse>> GetRecipeByIdAsync(int id) => await _api.GetRecipeByIdAsync(id);

        public async Task<ResponseValue<ShortRecipeResponse[]>> GetRecipesAsync(int offset, int count, string[] tags,
            string name) => await _api.GetRecipesAsync(offset, count, tags, name);

        public async Task<ResponseValue<LoginResponse>> LoginAsync(string Nick, string Password)
            => await _api.LoginAsync(Nick, Password);

        public async Task<ResponseValue<string>> RegisterationAsync(string Nick, string Password)
            => await _api.RegisterationAsync(Nick, Password);

        public Task<ResponseValue<LoginResponse>> RefreshAsync() => _api.RefreshAsync();

        public async Task<ResponseValue<byte[]>> GetImageBytesAsync(string imageUrl) =>
            await _api.GetImageBytesAsync(imageUrl);

        public async Task<ResponseValue<bool>> DeleteRecipeAsync(int recipeId) => await _api.DeleteRecipeAsync(recipeId);

        public async Task<ResponseValue<CommentResponse[]>> GetCommentsByRecIdAsync(int recipeId) =>
            await _api.GetCommentsByRecIdAsync(recipeId);
    }
}
