
namespace CookBookAdminApp.Model
{
    internal class CommentLeaf : commentComponent
    {
        public CommentLeaf(string commentText, string userNick, int userId, int firstCommentId)
            :base(commentText, userNick, userId, firstCommentId) 
        {

        }
    }
}
