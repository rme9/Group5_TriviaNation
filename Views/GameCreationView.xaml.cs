using System;
using System.Windows;
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

			vm.CloseView += OnCloseView;
		}

		public void OnCloseView(object sender, EventArgs eventArgs)
		{
			this.Close();
		}
	}
}
