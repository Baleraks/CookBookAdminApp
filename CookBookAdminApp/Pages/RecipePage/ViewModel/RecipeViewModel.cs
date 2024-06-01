using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CookBookAdminApp.Helpers;
using CookBookAdminApp.Model;
using CookBookAdminApp.Services;

namespace CookBookAdminApp
{
    internal class RecipeViewModel : INotifyPropertyChanged
    {
        public ICommand GoToCommentsCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public Recipe recipe { get; set; }
        public ObservableCollection<Step> Steps { get; set; }
        public ObservableCollection<Ingridients> Ingredients { get; set; }
        public ObservableCollection<Tag> Tags { get; set; }
        public Microsoft.Maui.Graphics.IImage Image { get; set; }
        public string Name { get; set; }
        public string Calories { get; set; }
        public bool IsBusy { get; set; }

        private IApiService _service;
        private Page _page;

        public RecipeViewModel(string id, Page page)
        {
            _service = ApiService.Instance;
            GoToCommentsCommand = new Command(async () => await GoToComments());
            DeleteCommand = new Command(async () => await Delete());
            _id = int.Parse(id);
            _page = page;
        }

        private async Task Delete()
        {
            var taskRecipeRespons = _service.GetRecipeByIdAsync(Convert.ToInt32(_id));
            IsBusy = true;
            var recipeRespons = await taskRecipeRespons;
            await Shell.Current.GoToAsync("..");
        }

        private async Task GoToComments()
        {
            await Shell.Current.GoToAsync($"{nameof(CommentsPage)}?ItemId={_id.ToString()}");
        }


        private bool _isInitialized;
        private readonly int _id;

        public async Task InitAsync()
        {
            if (_isInitialized) { return; }

            await Update();

            _isInitialized = true;
        }

        public async Task Update()
        {

            var taskRecipeRespons = _service.GetRecipeByIdAsync(Convert.ToInt32(_id));
            IsBusy = true;
            var recipeRespons = await taskRecipeRespons;
            var recipe = await Converter.RecipeResponseToRecipe(recipeRespons.Value);
            recipe.SetHash(recipeRespons.Value);

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Name = recipe.Name;
                Ingredients = recipe.Ingridients;
                Tags = recipe.Tags;
                Steps = recipe.Steps;
                Image = recipe.Image;
                Calories = recipe.Calories.ToString();
            });

            this.recipe = recipe;
            IsBusy = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
