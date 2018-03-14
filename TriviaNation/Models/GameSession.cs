using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaNation.Models
{
    public class GameSession
    {
        //TODO this should be a list of Students
        public List<Object> Students { get; private set; }

        public string Name { get; }

        public IQuestionBank QuestionBank { get; set; }

        public GameSession(string name)
        {

        }
    }
}
