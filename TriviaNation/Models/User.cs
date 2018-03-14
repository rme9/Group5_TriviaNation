using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaNation.Models
{
	class User : IUser
	{
		public string Email { get; set; }

		public string Name { get; set; }

		public User(string Name, string Email)
		{

		}
	}
}
