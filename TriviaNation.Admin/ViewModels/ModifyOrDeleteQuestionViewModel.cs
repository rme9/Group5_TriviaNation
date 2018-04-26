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
using TriviaNation.Views;

namespace TriviaNation.ViewModels
{
    class ModifyOrDeleteQuestionViewModel
    {
        
        private IQuestionBank questionBank { get; set; }

        private IQuestionBank _questions;

        AddQuestionToDatabaseView view = new AddQuestionToDatabaseView();

        /*Id that determines what questionbank to show in the view. */
        private string Id { get; set; }

        public IQuestionBank Questions
        {
            get {return _questions; }
            set { _questions = value; }
        }

        public ModifyOrDeleteQuestionViewModel()
        {
            using (var db = new WebServiceDriver())
            {
                _questions = db.GetQuestionBankById(Id).Result;
            }
        }


        #region OpenAddQuestionViewCommand
        public bool CanExecuteOpenAddQuestionViewCommand(object obj)
        {
            return view != null;
        }

        public void ExecuteOpenAddQuestionViewCommand(object obj)
        {
            AddQuestionWindow window = new AddQuestionWindow();
            window.Content = view;
            window.Show();
        }

        private RelayCommand _openAddQuestionViewControlCommand;

        public RelayCommand OpenAddQuestionViewControlCommand
        {
            get { return _openAddQuestionViewControlCommand ?? (_openAddQuestionViewControlCommand = new RelayCommand(ExecuteOpenAddQuestionViewCommand, CanExecuteOpenAddQuestionViewCommand)); }
        }

        #endregion
    }
}
