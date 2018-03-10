using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaNation.UI.Controller
{
    class GameCreationViewController
    {
        protected void OnStudentListChanged(string name)
        {
            PropertyChangedEventHandler handler = StudentListChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler StudentListChanged;
    }
}
