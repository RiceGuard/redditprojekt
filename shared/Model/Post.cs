using System;
namespace shared.Model
{
    public class Post
    {
        public long PostId { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public User User { get; set; }

        public int Vote { get; set; }

        public string Text { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();


        public Post(string title = "Ikke angivet", DateTime date = new DateTime(), int vote = 0, User user = null, string text = "")
        {
            Title = title;
            Date = date;
            Vote = vote;
            User = user;
            Text = text;
        }

        public Post()
        {
        }
    }
}

