using System.ComponentModel;

namespace CookBookAdminApp.Model
{
    internal class commentComponent : INotifyPropertyChanged
    {
        public string NewCommentText { get; set; }
        public string CommentText { get; set; }
        public int UserId { get; set; }
        public int FirstCommentId { get; set; }
        public string UserNick { get; set; }

        public commentComponent(string commentText, string userNick, int userId, int firstCommentId)
        {
            CommentText = commentText;
            UserNick = userNick;
            UserId = userId;
            FirstCommentId = firstCommentId;
        }

        public virtual void AddComponent(commentComponent Item) 
        {
        }

        public virtual void RemoveComponent(commentComponent Item)
        {

        }

        public virtual commentComponent GetComponent(int index = 0)
        {
            return this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
