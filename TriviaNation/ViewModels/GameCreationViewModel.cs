using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Mvvm;

namespace TriviaNation.ViewModels
{
	// Defines the data context of a GameCreationView
	class GameCreationViewModel : IViewModel
	{
		public GameCreationViewModel()
		{

		}

		#region CancelCommand

		public void ExecuteCancelCommand(object sender)
		{
			var button = sender as Button;
			if (button == null)
				return;

			var win = Window.GetWindow(button);
			if (win == null)
				return;

			win.Close();
		}

		#endregion
	}
}
