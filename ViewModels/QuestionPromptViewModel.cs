using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaNation.Drivers;
using TriviaNation.Models;

namespace TriviaNation.ViewModels
{
    public class QuestionPromptViewModel : IViewModel
    {
        public QuestionPromptViewModel()
        {
        }

        public string getQuestionBody()
        {
            DynamoDBDriver data = new DynamoDBDriver();
            IQuestionBank sessionQuestions = data.GetQuestionBankById("d187d42797634e5f9277faf5fd78bcf4");
            IQuestion activeQuestion = sessionQuestions.Questions.FirstOrDefault();

            return activeQuestion.Body;
        }

        public string getQuestionAnswer()
        {
            DynamoDBDriver data = new DynamoDBDriver();
            IQuestionBank sessionQuestions = data.GetQuestionBankById("d187d42797634e5f9277faf5fd78bcf4");
            IQuestion activeQuestion = sessionQuestions.Questions.FirstOrDefault();

            return activeQuestion.CorrectAnswer;
        }

        public string getQuestionOption(int answerOption)
        {
            DynamoDBDriver data = new DynamoDBDriver();
            IQuestionBank sessionQuestions = data.GetQuestionBankById("d187d42797634e5f9277faf5fd78bcf4");
            IQuestion activeQuestion = sessionQuestions.Questions.FirstOrDefault();

            return activeQuestion.AlternateAnswers.ElementAt(answerOption);
        }
    }
}
