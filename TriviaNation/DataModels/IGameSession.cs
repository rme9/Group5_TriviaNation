using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaNation.DataModels
{
    interface IGameSession
    {
        List<Object> Students { get; }

        String Name { get; }

        IQuestionBank QuestionBank { get; }


    }
}
