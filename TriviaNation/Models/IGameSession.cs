using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaNation.Models
{
	public interface IGameSession
	{
		string Name { get; set; }
		List<IUser> Students { get; set; }
		IQuestionBank QuestionBank { get; set; }
		string UniqueId { get; }
	}
}
