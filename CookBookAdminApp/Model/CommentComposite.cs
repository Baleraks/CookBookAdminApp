using System.Collections.ObjectModel;

namespace CookBookAdminApp.Model
{
    internal class CommentComposite : commentComponent
    {
        public ObservableCollection<commentComponent> Comments { get; set; } = new();

        public CommentComposite(string commentText, string userNick, int userId, int firstCommentId) 
            :base(commentText, userNick, userId, firstCommentId) 
        {
        }

        public override void AddComponent(commentComponent Item)
        {
            Comments.Add(Item);
        }

        public override void RemoveComponent(commentComponent Item)
        {
            Comments.Remove(Item);
        }

        public override commentComponent GetComponent(int index)
        {
            return Comments[index];
        }
    }
}
