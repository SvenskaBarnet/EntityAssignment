using Microsoft.EntityFrameworkCore;
namespace EntityAssignment;


public class BloggingContext : DbContext
{
	public DbSet<User> Users {get; set;}
	public DbSet<Blog> Blogs {get; set;}
	public DbSet<Post> Posts {get; set;}

	public string DbPath {get;}

	public BloggingContext()
	{
		var folder = Environment.CurrentDirectory;
		DbPath = System.IO.Path.Join(folder, "entityassignment.db");
	}

	protected override void OnConfiguring(DbContextOptionsBuilder options)=> options.UseSqlite($"Data Source={DbPath}");
}
