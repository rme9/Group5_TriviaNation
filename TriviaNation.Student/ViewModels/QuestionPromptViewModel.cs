using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaNation.Core.Drivers;
using TriviaNation.Core.Models;
using TriviaNation.ViewModels;

namespace TriviaNation.Student.ViewModels
{
    public class QuestionPromptViewModel : IViewModel
    {
        private static GameSession _Session;
        private static QuestionBank _ActiveQuestionBank;
        private static Question _ActiveQuestion;
        public static List<string> _Answers= new List<string>();

        public QuestionPromptViewModel(GameSession input)
        {
            _Session = input;

            _ActiveQuestionBank = input.QuestionBank;
            _ActiveQuestion = GetActiveQuestion();

            _Answers.Add(_ActiveQuestion.CorrectAnswer);
            _Answers.Add(_ActiveQuestion.AlternateAnswers[0]);
            _Answers.Add(_ActiveQuestion.AlternateAnswers[1]);
            _Answers.Add(_ActiveQuestion.AlternateAnswers[2]);
        }

        public string GetQuestionBody()
        {
            return _ActiveQuestion.Body;
        }

        public string GetQuestionAnswer(int reference)
        {
            return (_Answers[reference]);
        }

        public Question GetActiveQuestion()
        {
            Random rand = new Random();
            int questionRef = rand.Next(0, (_ActiveQuestionBank.Questions.Count - 1));

            return _ActiveQuestionBank.Questions[questionRef];
        }

    }
}
