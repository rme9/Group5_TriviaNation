using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using TriviaNation.Core.Drivers;
using TriviaNation.Core.Models;
using TriviaNation.Properties;
using TriviaNation.Util;

namespace TriviaNation.ViewModels
{
    public class AddQuestionToDataseViewModel : ViewModel
    {

        private List<string> _altAnswers;

        public List<string> AlternateAnswers
        {
            get { return _altAnswers; }
            set
            {
                if (_altAnswers != value)
                {
                    _altAnswers = value;
                    OnPropertyChanged(nameof(AlternateAnswers));
                    
                }
            }
        }







    }
}
