namespace TriviaNation.Core.Models
{
	public interface IUser
	{
		string Name { get; set; }
		string Email { get; set; }
		string Password { get; set; }
	}
}
