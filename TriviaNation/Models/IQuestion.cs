using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaNation.Models
{
	public interface IQuestion
	{
		string Body { set; get; }
		string CorrectAnswer { set; get; }
		List<string> AlternateAnswers { get; set; }
	}
}
