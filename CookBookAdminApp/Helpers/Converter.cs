using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBookAdminApp.Model;
using CookBookAdminApp.Services;
using Microsoft.Maui.Graphics.Platform;

namespace CookBookAdminApp.Helpers
{
    internal class Converter
    {
        private static IApiService _service;

        static Converter()
        {
            _service = ApiService.Instance;
        }

        public static async Task<Recipe> SortRecipeResponseToRecipe(ShortRecipeResponse shortRecipe)
        {
            string imageUrl = Constants.Texts.ServerUrl.Remove(Constants.Texts.ServerUrl.Count() - 1)
                              + shortRecipe.recipeimagepath.Replace('\\', '/');
            var recipe = new Recipe();
            try
            {
                var stream = await _service.GetImageBytesAsync(imageUrl);
                recipe = new Recipe()
                {
                    Calories = (float)shortRecipe.recipecalories,
                    ImageUrl = imageUrl,
                    Name = shortRecipe.recipename,
                    Image = PlatformImage.FromStream(new MemoryStream(stream.Value)),
                    Id = shortRecipe.id,
                    Likes = shortRecipe.likes,
                    Reports = shortRecipe.reportsnum
                };
            }
            catch (Exception ex)
            {
                Debug.Print($"\n\nSortRecipeResponseToRecipe: {ex.Message}\n\n.");
            }

            return recipe;
        }

        public static async Task<Recipe> RecipeResponseToRecipe(RecipeResponse recipeResponse)
        {
            string imageUrl = Constants.Texts.ServerUrl.Remove(Constants.Texts.ServerUrl.Count() - 1)
                              + recipeResponse.recipeimagepath.Replace('\\', '/');
            var imageStreamResponse = await _service.GetImageBytesAsync(imageUrl);
            var recipe = new Recipe()
            {
                Calories = (float)recipeResponse.recipecalories,
                ImageUrl = imageUrl,
                Name = recipeResponse.recipename,
                Image = PlatformImage.FromStream(new MemoryStream(imageStreamResponse.Value)),
                Id = recipeResponse.id
            };
            foreach (var stepItem in recipeResponse.steps)
            {
                imageUrl = Constants.Texts.ServerUrl.Remove(Constants.Texts.ServerUrl.Length - 1)
                           + stepItem.stepimagepath.Replace('\\', '/');
                var stream = await _service.GetImageBytesAsync(imageUrl);
                var step = new Step()
                {
                    ImageUrl = imageUrl,
                    Image = ImageSource.FromStream(() => new MemoryStream(stream.Value)),
                    CookingProcess = stepItem.steptext
                };
                recipe.Steps.Add(step);
            }
            foreach (var ingridientItem in recipeResponse.recipetoingridients)
            {
                var ingridient = new Ingridients()
                {
                    Ingridient = ingridientItem.ing.ingridienname,
                    Calories = ingridientItem.ing.ingridientcalories,
                    Qauntittys = ingridientItem.ing.ingridienttoqauntities[0].qau.type
                };
                recipe.Ingridients.Add(ingridient);
            }
            foreach (var tagItem in recipeResponse.recipetotags)
            {
                var tag = new Tag()
                {
                    _Tag = tagItem.tag.tagname
                };
                recipe.Tags.Add(tag);
            }
            recipe.SetHash(recipeResponse);
            return recipe;
        }

        public static List<CommentComposite> CommentResponseToCommentComponent(CommentResponse[] comments)
        {
            var components = new List<CommentComposite>();
            var firsts = comments.Where(x => x.firstcommentid == x.id);
            foreach (var comment in firsts)
            {
                var component = new CommentComposite(comment.commenttext, comment.userNick, 
                    comment.useId, comment.firstcommentid,
                    comment.id);
                var childs = comments.Where(x => x.firstcommentid == comment.id && x.id != comment.id);
                foreach (var child in childs)
                {
                    component.AddComponent(new CommentLeaf(child.commenttext, child.userNick, 
                        child.useId, child.firstcommentid,
                        child.id));
                }
                components.Add(component);
            }
            return components;
        }
    }
}
