using System;
using System.Reflection.Metadata;

namespace shared.Model
{
    public class Post
    {
        public int PostId { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public User User { get; set; }

        public int Upvote { get; set; }

        public int Downvote { get; set; }

        public string Text { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();


        public Post(User user, string title = "Ikke angivet", DateTime date = new DateTime(), int upvote = 0, int downvote = 0, string text = "")
        {
            Title = title;
            Date = date;
            Upvote = upvote;
            Downvote = downvote;
            User = user;
            Text = text;
        }

        public Post()
        {
      
        }
    }
}

