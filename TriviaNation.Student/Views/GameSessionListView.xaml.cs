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
using TriviaNation.Student.ViewModels;
using Application = System.Windows.Application;

namespace TriviaNation.Student.Views
{
    /// <summary>
    /// Interaction logic for GameSessionListView.xaml
    /// </summary>
    public partial class GameSessionListView : Window
    {
        public GameSessionListView(GameSessionListViewModel model)
        {
            InitializeComponent();
            //DataContext = model;

            CurrentName.Content = model.StudentName;
            List<string> a= new List<string>() { "1", "2", "3" };
            foreach(string x in a)
                AvailableSessions.Items.Add(x);
            this.Show();
            this.Closing += GameSessionListView_Closing;
        }

        private void GameSessionListView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Application.Current.Properties["LoggedInUserId"] == null)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
