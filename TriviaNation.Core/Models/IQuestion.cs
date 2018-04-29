using System.Collections.Generic;

namespace TriviaNation.Core.Models
{
	public interface IQuestion
	{
		string Body { set; get; }
		string CorrectAnswer { set; get; }
		List<string> AlternateAnswers { get; set; }
	}
}
