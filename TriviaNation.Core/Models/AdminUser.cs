namespace TriviaNation.Core.Models
{
	public class AdminUser : IUser
	{
		public string Name { get; set; }
		public string Email { get; set; }
        public string Password { get; set; }

		public string Password { get; set; }

		public AdminUser(string name, string email)
		{
			Name = name;
			Email = email;
		}
	}
}
