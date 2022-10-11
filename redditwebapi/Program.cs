using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using shared.Model;
using redditwebapi.Data;
using redditwebapi.Service;

Console.WriteLine("Hello");
var builder = WebApplication.CreateBuilder(args);

// Sætter CORS så API'en kan bruges fra andre domæner
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSomeStuff, builder => {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Tilføj DbContext factory som service.
builder.Services.AddDbContext<PostContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));

// Viser flotte fejlbeskeder i browseren hvis der kommer fejl fra databasen
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Tilføj DataService så den kan bruges i endpoints
builder.Services.AddScoped<DataService>();

// Dette kode kan bruges til at fjerne "cykler" i JSON objekterne.
/*
builder.Services.Configure<JsonOptions>(options =>
{
    // Her kan man fjerne fejl der opstår, når man returnerer JSON med objekter,
    // der refererer til hinanden i en cykel.
    // (altså dobbelrettede associeringer)
    options.SerializerOptions.ReferenceHandler = 
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
*/

var app = builder.Build();

// Seed data hvis nødvendigt.

using (var scope = app.Services.CreateScope())
{
    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    dataService.SeedData(); // Fylder data på, hvis databasen er tom. Ellers ikke.
}



app.UseHttpsRedirection();
app.UseCors(AllowSomeStuff);

// Middlware der kører før hver request. Sætter ContentType for alle responses til "JSON".
app.Use(async (context, next) =>
{
    context.Response.ContentType = "application/json; charset=utf-8";
    await next(context);
});


// DataService fås via "Dependency Injection" (DI)
app.MapGet("/", (DataService service) =>
{
    return new { message = "Hello World!" };
});

app.MapGet("/api/posts", (DataService service) =>
{
    return service.GetPosts().Select(p => new
    {
        postId = p.PostId,
        date = p.Date,
        title = p.Title,
        user = p.User,
        downvote = p.Downvote,
        upvote = p.Upvote,
        text = p.Text,
        commentsCount = p.Comments.Count()
    });
});

app.MapGet("/api/posts/{id}", (DataService service, long id) =>
{
    return service.GetPost(id);
});

app.MapPost("/api/posts", (DataService service, NewPostData data) =>
{
    string result = service.CreatePost(data.Title, data.UserId, data.Text);
    return new { message = result };
});

app.MapPost("/api/posts/{postid}/comments", (DataService service, long postid, NewCommentData data) =>
{
    string result = service.CreateComment(data.UserId, data.PostId, data.Text);
    return new { massage = result };
});


app.Run();

record NewPostData(string Title, long UserId, string Text);

record NewCommentData(long UserId, long PostId, string Text);

