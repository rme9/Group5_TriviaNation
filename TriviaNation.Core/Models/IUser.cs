using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaNation.Core.Models
{
	public interface IUser
	{
		string Name { get; set; }
		string Email { get; set; }
	}
}
