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

namespace TriviaNation.Control_Modules
{
    /// <summary>
    /// Interaction logic for DeleteModule.xaml
    /// </summary>
    /// 

        //handles the logic for the delete button on the main window of the program
    public partial class DeleteModule : UserControl
    {
        private static DeleteModule _instance;

        public DeleteModule()
        {
            InitializeComponent();
        }

        public static DeleteModule Instance
        {
            get
            {
                
                if (_instance == null)
                {
                    _instance = new DeleteModule();
                }
                return _instance;
            }
        }
    }
}
