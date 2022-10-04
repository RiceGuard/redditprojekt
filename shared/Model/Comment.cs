using System;
namespace shared.Model
{

    public class Comment
    {
        public long CommentId { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }

        public string Text { get; set; }

        public int Vote { get; set; }

        public long PostId { get; set; }



        public Comment(int vote = 0, DateTime date = new DateTime(), User user = null, string text = "", long postid = 0)

        {
            Vote = vote;
            Date = date;
            User = user;
            Text = text;
            PostId = postid;
        }



        public Comment()
        {

        }

    }
}