using System;
using System.Collections.Generic;

namespace TriviaNation.Core.Models
{
	public class GameSession : IGameSession
	{
		public List<StudentUser> Students { get; set; }

		public string Name { get; set; }

		public string UniqueId { get; set; }

		public QuestionBank QuestionBank { get; set; }

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