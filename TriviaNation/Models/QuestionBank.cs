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

	    public QuestionBank(string name)
	    {
		    Name = name;
	    }
    }
}
