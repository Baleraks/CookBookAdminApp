using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookAdminApp.Helpers
{
    public class RecipeResponse
    {
        public string recipename { get; set; }
        public int reportsnum { get; set; }
        public int likes { get; set; }
        public string recipeimagepath { get; set; }
        public int cokingtime { get; set; }
        public int id { get; set; }
        public int useId { get; set; }
        public float recipecalories { get; set; }
        public Recipetoingridient[] recipetoingridients { get; set; }
        public Recipetotag[] recipetotags { get; set; }
        public StepApi[] steps { get; set; }
    }

    public class Recipetoingridient
    {
        public Ing ing { get; set; }
    }

    public class Ing
    {
        public string ingridienname { get; set; }
        public float ingridientcalories { get; set; }
        public Ingridienttoqauntity[] ingridienttoqauntities { get; set; }
    }

    public class Ingridienttoqauntity
    {
        public Qau qau { get; set; }
    }

    public class Qau
    {
        public string type { get; set; }
    }

    public class Recipetotag
    {
        public int id { get; set; }
        public TagApi tag { get; set; }
    }

    public class TagApi
    {
        public string tagname { get; set; }
    }

    public class StepApi
    {
        public string stepimagepath { get; set; }
        public string steptext { get; set; }
    }
}
