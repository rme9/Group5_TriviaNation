using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaNation.Core.Models
{
	public interface IGameSession
	{
		string Name { get; set; }
		List<StudentUser> Students { get; set; }
		QuestionBank QuestionBank { get; set; }
		string UniqueId { get; }
	}
}
