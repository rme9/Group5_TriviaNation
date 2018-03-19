using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TriviaNation.Control_Modules;

namespace TriviaNation
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        DeleteModule deleteModule = new DeleteModule();
        ModifyModule modifyModule = new ModifyModule();


        public MainWindow()
		{
			InitializeComponent();
		}

        //Add button action
        

            //when Delete button is clicked, checks t osee if the delete control has been loaded and is not loads the module.
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (deleteModule.IsEnabled == false)
            {
               
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
