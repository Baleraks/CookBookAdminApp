using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBookAdminApp.Helpers;

namespace CookBookAdminApp.Services
{
    internal interface IApiService
    {
        public Task<ResponseValue<RecipeResponse>> GetRecipeByIdAsync(int id);

        public Task<ResponseValue<ShortRecipeResponse[]>> GetRecipesAsync(int offset, int count, string[] tags,
            string name);

        public Task<ResponseValue<LoginResponse>> LoginAsync(string Nick, string Password);

        public Task<ResponseValue<string>> RegisterationAsync(string Nick, string Password);

        public Task<ResponseValue<LoginResponse>> RefreshAsync();

        public Task<ResponseValue<byte[]>> GetImageBytesAsync(string imageUrl);

        public Task<ResponseValue<bool>> DeleteRecipeAsync(int recipeId);

        public Task<ResponseValue<CommentResponse[]>> GetCommentsByRecIdAsync(int recipeId);
    }
}
