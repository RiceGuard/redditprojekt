using System;
using redditwebapi.Data;
using shared.Model;
using redditwebapi.Service;
using Microsoft.EntityFrameworkCore;

namespace redditwebapi.Service
{
    public class DataService
    {

        private PostContext db { get; }

        public DataService(PostContext db)
        {
            this.db = db;
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

        public Post GetPost(int id)
        {
            return db.Posts.Include(p => p.User).Include(p => p.Comments).ToList().FirstOrDefault(p => p.PostId == id);
        }


        public string CreatePost(string title, int userid, string text)
        {
            User user = db.Users.FirstOrDefault(u => u.UserId == userid);
            db.Posts.Add(new Post {Title = title, User = user, Text = text, Date = DateTime.Now });
            db.SaveChanges();
            return "Post created";
        }

        public string CreateComment(int userid, int postid, string text)
        {
            Post post = db.Posts.FirstOrDefault(p => p.PostId == postid);
            User user = db.Users.FirstOrDefault(u => u.UserId == userid);
            db.Comments.Add(new Comment { Date = DateTime.Now, User = user, Text = text, PostId = postid });
            db.SaveChanges();
            return "You have made a comment🔥";
        }

        public string AddUpvote(int postid)
        {
            Post post = db.Posts.FirstOrDefault(p => p.PostId == postid);
            post.Upvote++;
            db.SaveChanges();
            return "Så har du lavet en upvote";
        
        }

        public string AddDownvote(int postid)
        {
            Post post = db.Posts.FirstOrDefault(p => p.PostId == postid);
            post.Downvote-=1;
            db.SaveChanges();
            return "Så har du lavet en downvote";

        }

    }

    }


