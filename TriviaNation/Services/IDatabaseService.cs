using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaNation.Models;

namespace TriviaNation.Services
{
	public interface IDatabaseService
	{
		bool InsertUser(IUser newUser, string instructorsEmail);

		bool InsertQuestionBank(IQuestionBank newQuestionBank, string instructorEmail);

		bool InsertGameSession(IGameSession newGameSession, string instructorsEmail);

		List<StudentUser> GetAllUsersByInstructor(string instructorsEmail);

		List<IQuestionBank> GetQuestionBanksByInstructor(string instructorsEmail);

		List<IGameSession> GetGameSessionsByInstructor(string instructorsEmail);

		IUser GetUserByEmail(string email);

		IQuestionBank GetQuestionBankById(string uniqueId);

		IGameSession GetGameSessionById(string uniqueId);
	}
}
