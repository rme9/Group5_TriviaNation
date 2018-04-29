using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaNation.Core.Models
{
	public class StudentUser : IUser
	{
		public string Email { get; set; }

		public string Name { get; set; }

		public string InstructorId { get; set; }

		public string Password { get; set; }

		public StudentUser(string name, string email)
		{
			Email = email;
			Name = name;
		}
	}
}
