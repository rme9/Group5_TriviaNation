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
    /// Interaction logic for ModifyModule.xaml
    /// </summary>
    /// 

        //handles the logic of the modify button on the main window of the project
    public partial class ModifyModule : UserControl
    {
        private static ModifyModule _instance;

        public static ModifyModule Instance
        {
            get
            {
                
                if (_instance == null)
                {
                    _instance = new ModifyModule();
                }
                return _instance;

            }
        }



        public ModifyModule()
        {
            InitializeComponent();
        }
    }
}
