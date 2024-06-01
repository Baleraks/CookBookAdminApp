using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookAdminApp.Helpers
{
    internal class LoginResponse
    {
        public string jwttoken { get; set; }
        public string refreshtoken { get; set; }
        public int id { get; set; }
    }
}
