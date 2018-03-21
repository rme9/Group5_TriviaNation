using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaNation.Models
{
	public class QuestionBank : IQuestionBank
	{
		public string Name { get; set; }
		public List<IQuestion> Questions { get; set; }
		public string UniqueId { get; }

		public QuestionBank()
		{
			UniqueId = Guid.NewGuid().ToString("N");
		}

		public QuestionBank(string uniqueId)
		{
			UniqueId = uniqueId;
		}
	}
}
