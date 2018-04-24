using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaNation.Core.Models
{
	public class QuestionBank : IQuestionBank
	{
		public string Name { get; set; }
		public List<IQuestion> Questions { get; set; }
		public string UniqueId { set; get; }

		public QuestionBank()
		{
			UniqueId = Guid.NewGuid().ToString("N");
            Name = "Default";
            Questions = new List<IQuestion>();

		}

		public QuestionBank(string uniqueId)
		{
			UniqueId = uniqueId;
		}
	}
}
