using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookAdminApp.Helpers
{
    public class CommentResponse
    {
        public string commenttext { get; set; }
        public int id { get; set; }
        public int useId { get; set; }
        public int recId { get; set; }
        public int firstcommentid { get; set; }
        public string userNick { get; set; }
    }
}
