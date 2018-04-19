using System.Collections.Generic;

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
