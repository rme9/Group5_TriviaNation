using System;
using System.Collections.Generic;

namespace TriviaNation.Models
{
	public class GameSession : IGameSession
	{
		public List<IUser> Students { get; set; }

		public string Name { get; set; }

		public string UniqueId { get; set; }

		public IQuestionBank QuestionBank { get; set; }

		public GameSession()
		{
			UniqueId = Guid.NewGuid().ToString("N");
		}

		public GameSession(string uniqueId)
		{
			UniqueId = uniqueId;
		}
		
	}
}