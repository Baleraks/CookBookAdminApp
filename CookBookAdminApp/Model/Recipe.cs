using CookBookAdminApp.Helpers;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;

namespace CookBookAdminApp.Model
{
    public class Recipe
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Likes { get; set; }
        public int Reports { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Ingridients> Ingridients { get; set; }
        public ObservableCollection<Tag> Tags { get; set; }
        public ObservableCollection<Step> Steps { get; set; }
        public List<DateTime> CookedDays { get; set; }
        public string FileId { get; set; }
        public string ImageUrl { get; set; }
        public float Calories { get; set; }
        public Microsoft.Maui.Graphics.IImage Image { get; set; }
        public bool IsCart { get; set; } = false;
        public bool IsLoad { get; set; } = false;
        public bool IsUpdated { get; set; } = false;
        public string OnLineHash { get; set; }


        public Recipe()
        {
            Ingridients = new ObservableCollection<Ingridients>();
            Tags = new ObservableCollection<Tag>();
            Steps = new ObservableCollection<Step>();
            CookedDays = new List<DateTime>();
            OnLineHash = "";
            FileId = "";
            ImageUrl = "";
            Calories = 0;
            UserId = Preferences.Default.Get(Constants.Texts.PreferencesUserIdKey, -1);
            SetFileId();
        }

        public Recipe(Recipe recipe)
        {
            Id = recipe.Id;
            Name = recipe.Name;
            Tags = recipe.Tags;
            FileId = recipe.FileId;
            Steps = new ObservableCollection<Step>();
            for (int i = 0; i < recipe.Steps.Count; i++)
            {
                Steps.Add(new Step(recipe.Steps[i]));
            }
            ImageUrl = recipe.ImageUrl;
            Ingridients = recipe.Ingridients;
            CookedDays = recipe.CookedDays;
            IsCart = recipe.IsCart;
            Calories = recipe.Calories;
            OnLineHash = recipe.OnLineHash;
            UserId = recipe.UserId;
        }

        public void SetFileId()
        {
            FileId = Guid.NewGuid().ToString();
            IsCart = false;
        }

        public void SetCalories()
        {
            Calories = 0;
            foreach (var item in Ingridients)
            {
                Calories += item.Calories;
            }
        }

        public void AddToCart()
        {
            IsCart = true;
        }

        public void AddDate()
        {
            CookedDays.Add(DateTime.Now);
        }

        public void RemoveFromCart()
        {
            IsCart = false;
        }

        public void SetHash(RecipeResponse recipeResponse)
        {
            if (recipeResponse == null)
                return;
            string data = recipeResponse.recipename;
            data += recipeResponse.recipeimagepath;
            foreach (var ingridient in recipeResponse.recipetoingridients)
            {
                data += ingridient.ing.ingridienname;
                foreach (var qauntity in ingridient.ing.ingridienttoqauntities)
                {
                    data += qauntity.qau.type;
                }
            }
            foreach (var step in recipeResponse.steps)
            {
                data += step.stepimagepath;
                data += step.steptext;
            }
            foreach (var tag in recipeResponse.recipetotags)
            {
                data += tag.tag.tagname;
            }
            var source = ASCIIEncoding.ASCII.GetBytes(data);
            source = new MD5CryptoServiceProvider().ComputeHash(source);
            OnLineHash = Convert.ToBase64String(source);
        }
    }
}
