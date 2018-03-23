using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaNation.Models;

namespace TriviaNation.ViewModels
{
    public class AddQuestionBankViewModel: IQuestion, IViewModel
    {
        public string Body { get; set; }
        public string CorrectAnswer { get; set; }
        public List<string> AlternateAnswers { get; set; }
    }
}
