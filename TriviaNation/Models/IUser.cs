using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaNation.Models
{
	public interface IUser
	{
		string Email { get; set; }

		string Name { get; set; }
	}
}
