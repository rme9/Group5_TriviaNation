using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using TriviaNation.Core.Drivers;
using TriviaNation.Core.Models;
using TriviaNation.Properties;
using TriviaNation.Util;
using TriviaNation.Annotations;

namespace TriviaNation.ViewModels
{
    public class AddQuestionToDataseViewModel : ViewModel
    {

        private Question question;
        private string _body;
        private string _correctAnswer;
        private string _altAnswer1;
        private string _altAnswer2;
        private string _altAnswer3;
        


        public AddQuestionToDataseViewModel()
        {
            question = new Question();
            
        }

        public string Body
        {
            get { return _body; }
            set
            {
                if (_body != value)
                {
                    _body = value;
                    OnPropertyChanged(nameof(Body));
                }
            }
        }
        #region Answers
        public string CorrectAnswer
        {
            get { return _correctAnswer; }
            set
            {
                if (_correctAnswer != value)
                {
                    _correctAnswer = value;
                    OnPropertyChanged(nameof(CorrectAnswer));
                }
            }
        }

        public string AlternateAnswer1
        {
            get { return _altAnswer1; }
            set
            {
                if (_altAnswer1 != value)
                {
                    _altAnswer1 = value;
                    OnPropertyChanged(nameof(AlternateAnswer1));
                }
            }
        }

        public string AlternateAnswer2
        {
            get { return _altAnswer2; }
            set
            {
                if (_altAnswer2 != value)
                {
                    _altAnswer2 = value;
                    OnPropertyChanged(nameof(AlternateAnswer2));
                }
            }
        }

        public string AlternateAnswer3
        {
            get { return _altAnswer3; }
            set
            {
                if (_altAnswer3 != value)
                {
                    _altAnswer3 = value;
                    OnPropertyChanged(nameof(AlternateAnswer3));
                }
            }
        }

        #endregion

        public void addAltAnswer(string _altAnswer)
        {
            question.AlternateAnswers.Add(_altAnswer);
        }

        public void addBody(string body)
        {
            if (body != null)
            {
                question.Body = body;
            }
        }

        public bool CanExecuteLoginCommand(object ob)
        {
            return question != null;
        }

        public void ExecuteSaveQuestionCommand()
        {
            
            addAltAnswer(CorrectAnswer);
            addAltAnswer(AlternateAnswer1);
            addAltAnswer(AlternateAnswer2);
            addAltAnswer(AlternateAnswer3);
        }

        private ICommand _AddQuestionCommand;

       







    }
}
