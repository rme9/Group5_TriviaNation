using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaNation.Core.Models;

namespace TriviaNation.Core.Drivers
{
	public interface IWebServiceDriver
	{
		Task<bool> InsertUser(IUser newUser);

		Task<bool> InsertQuestionBank(IQuestionBank newQuestionBank, string instructorEmail);

		Task<bool >InsertGameSession(IGameSession newGameSession, string instructorsEmail);

		Task<List<StudentUser>> GetAllUsersByInstructor(string instructorsEmail);

		Task<List<IQuestionBank>> GetQuestionBanksByInstructor(string instructorsEmail);

		Task<List<IGameSession>> GetGameSessionsByInstructor(string instructorsEmail);

		Task<IUser> GetUserByEmail(string email);

		Task<IQuestionBank> GetQuestionBankById(string uniqueId);

		Task<IGameSession> GetGameSessionById(string uniqueId);
	}
}
