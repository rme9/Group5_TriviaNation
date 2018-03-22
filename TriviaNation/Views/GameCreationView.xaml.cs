using System;
using System.Windows;
using System.Windows.Controls;
using TriviaNation.ViewModels;

namespace TriviaNation.UI.Views
{
	/// <summary>
	/// Interaction logic for GameCreationView.xaml
	/// </summary>
	public partial class GameCreationView : UserControl
	{
		public GameCreationView(GameCreationViewModel vm)
		{
			InitializeComponent();
			DataContext = vm;

			vm.CloseView += OnCloseView;
		}

		public void OnCloseView(object sender, EventArgs eventArgs)
		{
		}
	}
}
