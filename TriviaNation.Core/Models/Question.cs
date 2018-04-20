using System.Collections.Generic;

namespace TriviaNation.Core.Models
{
	public class Question : IQuestion
	{
		public string Body { get; set; }
		public string CorrectAnswer { get; set; }
		public List<string> AlternateAnswers { get; set; }

	}
}
