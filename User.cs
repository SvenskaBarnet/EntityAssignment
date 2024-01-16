namespace EntityAssignment;

public class User
{
	public int UserId {get; set;}
	public string? Username {get; set;}
	public string? Password {get; set;}
	
	public List<Post> Posts { get; set; } = new();
	public List<int> PostId { get; set; } = new();
}
