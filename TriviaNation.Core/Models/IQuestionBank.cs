using System.Collections.Generic;

namespace TriviaNation.Core.Models
{
	public interface IQuestionBank
	{
		string Name { get; set; }

		List<Question> Questions { get; set; }

		string UniqueId { get; }
	}
}
