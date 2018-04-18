using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaNation.Student.Models
{
    class Territories
    {
        private string Name;
        private string ControllerName = null;
        private bool ControlStatus = false;

        public Territories(string name, bool isConstrolled, string controllerName)
        {
            Name = name;
            ControlStatus = isConstrolled;
            ControllerName = controllerName;

        }
    }
}
