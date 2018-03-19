using System.Collections.Generic;

namespace TriviaNation.Models
{
    public class GameSession
    {
        public List<IUser> Students { get; private set; }

        public string Name { get; }

        public IQuestionBank QuestionBank { get; set; }

        public GameSession(string name)
        {

        }

	    public GameSession(string name, List<IUser> students)
	    {

	    }
    }
}
