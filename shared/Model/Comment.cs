using System;
using System.Reflection.Metadata;

namespace shared.Model
{

    public class Comment
    {
        public int CommentId { get; set; }

        public DateTime Date { get; set; }

        public User User { get; set; }

        public string Text { get; set; }

        public int Upvote { get; set; }

        public int Downvote { get; set; }

        public int PostId { get; set; }



        public Comment(int downvote = 0, int upvote = 0, DateTime date = new DateTime(), User user = null, string text = "", int postid = 0)

        {
            Downvote = downvote;
            Upvote = upvote;
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