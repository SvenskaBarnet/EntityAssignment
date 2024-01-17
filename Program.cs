using EntityAssignment;
using var db = new BloggingContext();

foreach (Blog b in db.Blogs)
{
    db.Remove(b);
}
db.SaveChanges();

foreach (User u in db.Users)
{
    db.Remove(u);
}
db.SaveChanges();

string[] blogs = File.ReadAllLines("blogs.csv");
string[] users = File.ReadAllLines("users.csv");
string[] posts = File.ReadAllLines("posts.csv");

foreach (string blog in blogs)
{
    string[] blogInfo = blog.Split(',');
    int blogId = ParseId(blogInfo[0]);
    string url = blogInfo[1];
    string name = blogInfo[2];
    if (db.Blogs.Find(blogId) == null)
    {
        db.Add(new Blog
        {
            BlogId = blogId,
            Url = url,
            Name = name
        });
    }
}
db.SaveChanges();

foreach (string user in users)
{
    string[] userInfo = user.Split(',');
    int userId = ParseId(userInfo[0]);
    string username = userInfo[1];
    string password = userInfo[2];
    if (db.Users.Find(userId) == null)
    {
        db.Add(new User
        {
            UserId = userId,
            Username = username,
            Password = password
        });
    }
}
db.SaveChanges();

foreach (string post in posts)
{
    string[] postInfo = post.Split(',');
    int postId = ParseId(postInfo[0]);
    string title = postInfo[1];
    string content = postInfo[2];
    DateTime publishedOn = ParseDate(postInfo[3]);
    int userId = ParseId(postInfo[4]);
    int blogId = ParseId(postInfo[5]);
    db.Add(new Post
    {
        PostId = postId,
        Title = title,
        Content = content,
        PublishedOn = publishedOn,
        UserId = userId,
        BlogId = blogId
    });
}
db.SaveChanges();
Console.WriteLine("\n╔═══════════════╗\n║ DATABASE TREE ║\n╚═══════════════╝");
foreach (Blog b in db.Blogs)
{
    Console.WriteLine($"{BoxString(b.Name)}");
    Console.WriteLine($"BlogId: {b.BlogId}, Url: {b.Url} Name: {b.Name}");
    Console.WriteLine($"{string.Format("{0,3} {1,-7}", "", "Posts: ")}");

    foreach (Post p in b.Posts)
    {
        Console.WriteLine($"{string.Format("{0,7} {1, -20}", "", $"PostId: {p.PostId}")}");
        Console.WriteLine($"{string.Format("{0,7} {1, -20}", "", $"Titel: {p.Title}")}");
        Console.WriteLine($"{string.Format("{0,7} {1, -20}", "", $"Content: {p.Content}")}");
        Console.WriteLine($"{string.Format("{0,7} {1, -20}", "", $"Published on: {p.PublishedOn.ToShortDateString()}\n")}");
        Console.WriteLine($"{string.Format("{0,7} {1,7} {2, -20}", "", "", $"Author details:")}");
        Console.WriteLine($"{string.Format("{0,7} {1,7} {2, -20}", "", "", $"Username: {p.User?.Username}")}");
        Console.WriteLine($"{string.Format("{0,7} {1,7} {2, -20}", "", "", $"Password: {p.User?.Password}\n")}");
    }
}


static int ParseId(string csvId)
{
    int entityId = 0;
    if (int.TryParse(csvId, out int id))
    {
        entityId = id;
    }
    else
    {
        Console.WriteLine("Couldn't parse id: {0}", csvId);
        Console.WriteLine("Shutting down");
        Environment.Exit(1337);
    }
    if (entityId == 0)
    {
        Console.WriteLine("Id can't be 0. Shutting down.");
        Thread.Sleep(1337);
        Environment.Exit(1337);
    }
    return entityId;
}

static DateTime ParseDate(string csvDate)
{
    DateTime date = new DateTime();
    String pattern = "dd/MM/yyyy";
    try
    {
        date = DateTime.ParseExact(csvDate, pattern, null);
    }
    catch (FormatException)
    {
        Console.WriteLine("'{0}' is not in the correct format", csvDate);
        Thread.Sleep(1337);
        Environment.Exit(300);
    }
    return date;
}

static string BoxString(string? username)
{
    string boxLength = "";
    for (int i = 0; i < username?.Length + 2; i++)
    {
        boxLength += "─";
    }
    string user = $"┌{boxLength}┐ \n│ {username} │\n└{boxLength}┘";
    return user;
}
