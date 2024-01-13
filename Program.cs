using EntityAssignment;
using var db = new BloggingContext();

foreach(Blog b in db.Blogs)
{
	db.Remove(b);
}
db.SaveChanges();

foreach(User u in db.Users)
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
	db.Add(new Blog
	{
		BlogId = blogId,
		Url = url,
		Name = name 
	});
}
db.SaveChanges();

foreach(string user in users)
{
	string[] userInfo = user.Split(',');
	int userId = ParseId(userInfo[0]);
	string username = userInfo[1];
	string password = userInfo[2];
	db.Add(new User 
	{
		UserId = userId,
		Username = username,
		Password = password 
	});
}
db.SaveChanges();

foreach(string post in posts)
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
Console.WriteLine("\nBlogs:");
foreach(Blog b in db.Blogs)
{
	Console.WriteLine($"\n{b.Name}");
	Console.WriteLine($"BlogId: {b.BlogId}, Url: {b.Url}");
	Console.WriteLine("    Posts: ");
	foreach(Post p in b.Posts)
	{
		Console.WriteLine($"	Title: {p.Title}, Published On: {p.PublishedOn.ToShortDateString()}, Author: {db.Users.Find(p.UserId).Username} ");
		Console.WriteLine($"		Content:\n				{p.Content}");
	}
}

Console.WriteLine("\nUsers:");
foreach(User u in db.Users)
{
	Console.WriteLine($"\n{u.Username}");
	Console.WriteLine($"UserId: {u.UserId}, Username: {u.Username}, Password: {u.Password}");
	Console.WriteLine("    Posts:");
	foreach(Post p in u.Posts)
	{
		Console.WriteLine($"	Title: {p.Title}");
	}
}

static int ParseId(string csvId)
{
	int entityId = 0;
	if(int.TryParse(csvId, out int id))
	{
		entityId = id;
	}
	else
	{
        Console.WriteLine("Couldn't parse id: {0}", id);
    }
	if(entityId == 0)
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
		Console.WriteLine("{0} is not in the correct format", csvDate);
		Thread.Sleep(1337);
	}
	return date;
}
