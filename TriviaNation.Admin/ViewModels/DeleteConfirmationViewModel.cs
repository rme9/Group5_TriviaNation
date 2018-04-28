using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TriviaNation.Core.Drivers;
using TriviaNation.Core.Models;
using TriviaNation.Util;

namespace TriviaNation.ViewModels
{
	public class DeleteConfirmationViewModel : ViewModel
	{
		private string _WarningMessage;

		public string WarningMessage
		{
			get { return _WarningMessage; }
			set
			{
				if (!value.Equals(_WarningMessage))
				{
					_WarningMessage = value;
					OnPropertyChanged(nameof(WarningMessage));
				}
			}
		}

		public DeleteConfirmationViewModel(string itemDescription)
		{
			WarningMessage = "This operation will permanently delete " + itemDescription + ".\n\nDo you wish to continue?";
		}

		public DeleteConfirmationViewModel()
		{
			WarningMessage = "This operation will permanently delete data.\n\nDo you wish to continue?";
		}


		#region DeleteCommand

		public void ExecuteDeleteCommand(object ob)
		{
			CloseView?.Invoke(this, true);
		}

		private RelayCommand _DeleteCommand;

		public RelayCommand DeleteCommand
		{
			get { return _DeleteCommand ?? (_DeleteCommand = new RelayCommand(ExecuteDeleteCommand)); }
			set { _DeleteCommand = value; }
		}

		#endregion

		#region CancelCommand

		public void ExecuteCancelCommand(object ob)
		{
			CloseView?.Invoke(this, false);
		}

		private RelayCommand _CancelCommand;

		public RelayCommand CancelCommand
		{
			get { return _CancelCommand ?? (_CancelCommand = new RelayCommand(ExecuteCancelCommand)); }
			set { _CancelCommand = value; }
		}

		public event EventHandler<object> CloseView;

		#endregion

	}
}
