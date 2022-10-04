using System;
using redditwebapi.Data;
using shared.Model;
using redditwebapi.Service;

namespace redditwebapi.Service
{
    public class DataService
    {
        private PostContext db { get; }

        public DataService(PostContext db)
        {
            this.db = db;
        }
    }

}

