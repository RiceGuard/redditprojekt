using System;
using redditwebapi.Data;
using shared.Model;
using redditwebapi.Service;
using Microsoft.EntityFrameworkCore;

namespace redditwebapi.Service
{
    public class DataService
    {
        private readonly HttpClient http;
        private readonly IConfiguration configuration;
        private readonly string baseAPI = "";

        private PostContext db { get; }

        public DataService(PostContext db)
        {
            this.db = db;
        }

        public DataService(HttpClient http, IConfiguration configuration)
        {
            this.http = http;
            this.configuration = configuration;
            this.baseAPI = configuration["base_api"];
        }

        public void SeedData()
        {

            User user = db.Users.FirstOrDefault()!;
            if (user == null)
            {
                db.Users.Add(new User { Username = "Anonymous" });
                db.Users.Add(new User { Username = "Frederik" });
            }

            db.SaveChanges();

            Post post = db.Posts.FirstOrDefault()!;
            if (post == null)
            {
                post = new Post { Date = DateTime.Now, Title = "Random piece of shit", User = new User("Jacob"), Text = "What's the most gatekeep-y opinion you hold?" };
                db.Posts.Add(post);
                db.Posts.Add(new Post { Date = DateTime.Now, Title = "Random piece of shit", User = new User("Oscar"), Text = "North Korea Fires Missile Over Japan in Major" });
                db.SaveChanges();
            }

            Comment comment = db.Comments.FirstOrDefault()!;
            if (comment == null)
            {
                db.Comments.Add(new Comment { Date = DateTime.Now, User = new User("Lars"), Text = "Holy Shit!!!!", PostId = 2 });
                db.SaveChanges();
            }


        }

        //henter en liste af posts
        public List<Post> GetPosts()
        {
            return db.Posts.Include(p => p.User).Include(p => p.Comments).ToList();
        }

        public Post GetPost(long id)
        {
            return db.Posts.Include(p => p.User).Include(p => p.Comments).ToList().FirstOrDefault(p => p.PostId == id);
        }


        public string CreatePost(string title, long userid, string text)
        {
            User user = db.Users.FirstOrDefault(u => u.UserId == userid);
            db.Posts.Add(new Post {Title = title, User = user, Text = text, Date = DateTime.Now });
            db.SaveChanges();
            return "Post created";
        }

        public string CreateComment(long userid, long postid, string text)
        {
            Post post = db.Posts.FirstOrDefault(p => p.PostId == postid);
            User user = db.Users.FirstOrDefault(u => u.UserId == userid);
            db.Comments.Add(new Comment { Date = DateTime.Now, User = user, Text = text, PostId = postid });
            db.SaveChanges();
            return "You have made a comment🔥";
        }

    }
}

