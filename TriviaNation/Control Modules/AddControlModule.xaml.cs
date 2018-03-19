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
    /// Interaction logic for AddControlModule.xaml
    /// </summary>
    
        //Handles the logic for the add button on the main window of the program
    public partial class AddControlModule : UserControl
    {
        private static AddControlModule _instance;

        public static AddControlModule Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AddControlModule();
                }
                    return _instance;
                
            }
        }

        public AddControlModule()
        {
            InitializeComponent();
        }
    }
}
