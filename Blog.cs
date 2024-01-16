namespace EntityAssignment;

public class Blog
{
	public int BlogId {get; set;}
	public string? Url {get; set;}
	public string? Name {get; set;}

	public List<Post> Posts {get;} = new();
	public List<int> PostId { get; set; }
}
