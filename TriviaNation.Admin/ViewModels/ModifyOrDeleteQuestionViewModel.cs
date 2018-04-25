using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TriviaNation.Core.Drivers;
using TriviaNation.Core.Models;
using TriviaNation.UI.Views;
using TriviaNation.Util;

namespace TriviaNation.ViewModels
{
    class ModifyOrDeleteQuestionViewModel
    {
        
        private QuestionBank questionBank { get; set; }

        private List<IQuestion> _questions;

        /*Id that determines what questionbank to show in the view. */
        private string Id { get; set; }

        public ObservableCollection<IQuestion> Questions
        {
            get { return new ObservableCollection<IQuestion>(_questions); }
            set { _questions = value.ToList(); }
        }

        public ModifyOrDeleteQuestionViewModel()
        {
            using (var db = new DynamoDBDriver())
            {
                _questions = db.GetQuestionBankById(Id).Questions;
            }

            questionBank = new QuestionBank();
        }


    }
}
