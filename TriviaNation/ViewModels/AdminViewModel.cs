using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TriviaNation.Models;
using TriviaNation.Util;

namespace TriviaNation.ViewModels
{
	// Defines the data context of an AdminView
	public class AdminViewModel : IViewModel
	{
		public string LoggedInUserName
		{
			get { return Application.Current.Properties["LoggedInUserName"] as string; }
			set { }
		}

		public AdminViewModel()
		{
			
		}

	}
}
