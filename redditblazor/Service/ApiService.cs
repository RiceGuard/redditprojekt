using System;
using shared.Model;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace redditblazor.Service
{
    public class ApiService
    {
        private readonly HttpClient http;
        private readonly IConfiguration configuration;
        private readonly string baseAPI = "";

        public ApiService(HttpClient http, IConfiguration configuration)
        {
            this.http = http;
            this.configuration = configuration;
            this.baseAPI = configuration["base_api"];
        }

        public async Task<Post[]> GetPosts()
        {
            string url = $"{baseAPI}posts/";
            return await http.GetFromJsonAsync<Post[]>(url);
        }

        public async Task<Post> GetPost(int id)
        {
            string url = $"{baseAPI}posts/{id}/";
            return await http.GetFromJsonAsync<Post>(url);
        }

        public async Task<Comment> CreateComment(string text, int postId, int userId)
        {
            string url = $"{baseAPI}posts/{postId}/comments";

            // Post JSON to API, save the HttpResponseMessage
            HttpResponseMessage msg = await http.PostAsJsonAsync(url, new { text, userId });

            // Get the JSON string from the response
            string json = msg.Content.ReadAsStringAsync().Result;

            // Deserialize the JSON string to a Comment object
            Comment? newComment = JsonSerializer.Deserialize<Comment>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties 
            });

            // Return the new comment 
            return newComment;
        }

    }
}

