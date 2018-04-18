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
        private QuestionBank questionbank;
        private string _body;
        private string _correctAnswer;
        private string _altAnswer1;
        private string _altAnswer2;
        private string _altAnswer3;
        


        public AddQuestionToDataseViewModel()
        {
            question = new Question();
            questionbank = new QuestionBank();
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

        public Question Question
        {
            get { return question; }
            set
            {
                if (question != value)
                {
                    question = value;
                }
            }
        }

        public bool CanExecuteSaveQuestionCommand(object ob)
        {
            return question != null;
        }

        /*Adds the body of the question to the question object*/
        public void addBody(string body)
        {
            if (body != null)
            {
                question.Body = body;
            }
        }

        /*Adds the alternate answer strings to the question object*/
        public void AddAltAnswer(string _altAnswer)
        {
            Question.AlternateAnswers.Add(_altAnswer);
        }

        /* Adds question to questionbank (to be later added to the database) 
         */
        public void AddToQuestionBank(Question question)
        {
            questionbank.Questions.Add(Question);
        }
        
        
        /*Takes the info in the textboxes of the view and save the body and the answers into the 
         * question object.
         * Takes the question object and adds it the database.
        */
        public void ExecuteSaveQuestionCommand(object ob)
        {

            addBody(Body);
            AddAltAnswer(CorrectAnswer);
            AddAltAnswer(AlternateAnswer1);
            AddAltAnswer(AlternateAnswer2);
            AddAltAnswer(AlternateAnswer3);
            AddToQuestionBank(question);
        }

        private RelayCommand _AddQuestionToDatabase;
        public RelayCommand AddQuestionToDatabase
        {
            get { return _AddQuestionToDatabase ?? (_AddQuestionToDatabase = new RelayCommand(ExecuteSaveQuestionCommand, CanExecuteSaveQuestionCommand)); }
        }

        #region CancelSavingQuestionCommand

        public void ExecuteCancelSavingQuestionCommand(object ob)
        {
            CloseView?.Invoke(this, null);
        }

        private RelayCommand _CancelSavingQuestionCommand;

        public RelayCommand CancelSavingQuestionCommand
        {
            get { return _CancelSavingQuestionCommand ?? (_CancelSavingQuestionCommand = new RelayCommand(ExecuteCancelSavingQuestionCommand)); }
            set { _CancelSavingQuestionCommand = value; }
        }

        #endregion

        public event EventHandler<object> CloseView;









    }
}
