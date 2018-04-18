using System.Collections.Generic;

namespace TriviaNation.Core.Models
{
	public interface IGameSession
	{
		string Name { get; set; }
		List<IUser> Students { get; set; }
		IQuestionBank QuestionBank { get; set; }
		string UniqueId { get; }
	}
}
