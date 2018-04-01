using System;
using System.Windows;
using System.Windows.Controls;
using TriviaNation.ViewModels;

namespace TriviaNation.UI.Views
{
	/// <summary>
	/// Interaction logic for GameCreationView.xaml
	/// </summary>
	public partial class GameCreationView : Window
	{
		public GameCreationView(GameCreationViewModel vm)
		{
			InitializeComponent();
			DataContext = vm;

			vm.CloseView += Vm_CloseView; ;
		}

		private void Vm_CloseView(object sender, object e)
		{
			this.Close();
		}

	}
}
