using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TriviaNation.Core.Models;
using TriviaNation.Util;
using TriviaNation.ViewModels;

namespace TriviaNation.Student.ViewModels
{
    public class GameBoardViewModel : IViewModel
    {
        public GameSession Session = null;

        public GameBoardViewModel(GameSession input)
        {
            Session = input;
        }
    }
}