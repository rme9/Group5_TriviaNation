using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaNation.Core.Models
{
	public class Question : IQuestion
	{



        public string Body { get; set; }
		public string CorrectAnswer { get; set; }
		public List<string> AlternateAnswers { get; set; }

        public Question()
        {
            this.Body = "";
            this.AlternateAnswers = new List<string>();
            this.CorrectAnswer = "";
        }
	}
}
