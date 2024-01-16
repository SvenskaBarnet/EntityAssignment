namespace EntityAssignment;

public class Post
{
	public int PostId {get; set;}
	public string? Title {get; set;}
	public string? Content {get; set;}
	public DateTime PublishedOn {get; set;}

	public int UserId {get; set;}
	public User? User { get; set; }
	public int BlogId {get; set;}
	public Blog? Blog { get; set; }
}
