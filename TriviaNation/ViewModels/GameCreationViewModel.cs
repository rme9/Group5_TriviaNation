using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Mvvm;
using TriviaNation.Models;

namespace TriviaNation.ViewModels
{
	// Defines the data context of a GameCreationView
	public class GameCreationViewModel : IViewModel
	{
		public string Name { get; set; }

		public IQuestionBank SelectedQuestionBank { get; set; }

		public ObservableCollection<IQuestionBank> AvailableQuestionBanks { get; set; }

		public ObservableCollection<IUser> SelectedStudents { get; set; }

		public ObservableCollection<IUser> AvailableStudents { get; set; }

		public GameCreationViewModel()
		{
		}
	}
}
