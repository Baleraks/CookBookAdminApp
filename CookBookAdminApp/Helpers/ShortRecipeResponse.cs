using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookAdminApp.Helpers
{
    public class ShortRecipeResponse
    {
        public string recipename { get; set; }
        public int reportsnum { get; set; }
        public int likes { get; set; }
        public string recipeimagepath { get; set; }
        public int cokingtime { get; set; }
        public int id { get; set; }
        public int useId { get; set; }
        public double recipecalories { get; set; }
    }
}
