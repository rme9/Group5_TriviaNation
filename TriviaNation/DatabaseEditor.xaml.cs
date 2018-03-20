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
using System.Windows.Shapes;
using TriviaNation.ViewModels;

namespace TriviaNation
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Add_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new AddQuestionBankViewModel();
        }

        private void Delete_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new DeleteQuestionBankViewModel();
        }

        private void Modify_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ModifyQuestionBankViewModel();
        }
    }
}
