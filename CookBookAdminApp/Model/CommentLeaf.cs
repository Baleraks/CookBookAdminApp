
namespace CookBookAdminApp.Model
{
    internal class CommentLeaf : commentComponent
    {
        public CommentLeaf(string commentText, string userNick, int userId, int firstCommentId, int id)
            :base(commentText, userNick, userId, firstCommentId, id) 
        {

        }
    }
}
