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

namespace TriviaNation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource = new BitmapImage(new Uri("D:/Programming/SE2/TriviaNation/TriviaNation/TriviaNation/Resources/background.gif", UriKind.Absolute));
            this.Background = myBrush;
        }

        private void showInformation(object sender, RoutedEventArgs e)
        {
            if (((RadioButton)sender).IsChecked == true)
            {
                InformationPanel.Visibility = Visibility.Visible;
                Information_Info.Visibility = Visibility.Visible;
                Information_Controlled.Visibility = Visibility.Visible;
                AttackButton.Visibility = Visibility.Visible;
                DefendButton.Visibility = Visibility.Visible;

                Information_Info.Content = ((RadioButton)sender).Name;
            }
        }
    }
}
