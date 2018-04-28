using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NUnit.Framework;
using TriviaNation.Core.Drivers;
using TriviaNation.Core.Models;

namespace TriviaNation.Test
{
	public class DynamoDBTest
	{
		private WebServiceDriver _Driver;

		#region TestParameters

		private AdminUser _NewAdmin => new AdminUser("Rebecca Elliff", "rme9@students.uwf.edu") { Password = "password"};

		private StudentUser _NewUser => new StudentUser("Ron Weasley", "RWeasley@email.com") { InstructorId = _NewAdmin.Email, Password = "rw" };

		private List<Question> _Questions;

		private QuestionBank _NewQuestionBank;

		private GameSession _NewGameSession;

		#endregion

		#region Setup

		[OneTimeSetUp]
		public void testSetup()
		{
			_Driver = new WebServiceDriver();

			CreateQuestions();

			CreateQuestionBank();

			CreateGameSession();
		}

		public void CreateQuestions()
		{
			_Questions = new List<Question>
			{
				new Question
				{
					Body = "Who was the first president of the United States of America?",
					AlternateAnswers = new List<string> {"John Adams", "John Hancock", "Alexander Hamilton"},
					CorrectAnswer = "George Washington"
				},
				new Question
				{
					Body = "Who of the following is not a founding father of the United States of America?",
					AlternateAnswers = new List<string> {"George Washington", "John Hancock", "Alexander Hamilton"},
					CorrectAnswer = "John Quincy Adams"
				}
			};
		}

		public void CreateQuestionBank()
		{
			_NewQuestionBank = new QuestionBank("08a9df568ced4fa393e4a78d7da94649")
			{
				Questions = _Questions,
				Name = "US_History_1"
			};
		}

		public void CreateGameSession()
		{
			_NewGameSession = new GameSession("29a9d5c256a24cc8b05a32862609d8e7")
			{
				Students = new List<StudentUser>
				{
					_NewUser
				},
				Name = "TestGame1",
				QuestionBank = _NewQuestionBank
			};

		}

		#endregion

		#region User

		[Test]
		public async Task TestInsertUser()
		{
			var newStu = await _Driver.InsertUser(_NewUser);

			Assert.IsTrue(newStu);
		}

		[Test]
		public async Task TestGetAllUsersByInstructor()
		{
			var users = await _Driver.GetAllUsersByInstructor(_NewAdmin.Email);

			Assert.IsNotNull(users.Where(x => x.Name.Equals("Harry Potter")));
			Assert.IsNotNull(users.Where(x => x.Name.Equals("Ron Weasley")));
		}

		[Test]
		public async Task TestGetUserByEmail()
		{
			var student = await _Driver.GetUserByEmail(_NewUser.Email);

			Assert.IsTrue(_NewUser.Name.Equals(student.Name));
			Assert.IsTrue(_NewUser.Email.Equals(student.Email));
		}

		#endregion

		#region QuestionBanks

		[Test]
		public async Task TestInsertQuestionBank()
		{
			var res = await _Driver.InsertQuestionBank(_NewQuestionBank, _NewAdmin.Email);

			Assert.IsTrue(res);
		}

		[Test]
		public async Task TestGetAllQuestionBanksByInstructor()
		{
			var qbs = await _Driver.GetQuestionBanksByInstructor(_NewAdmin.Email);

			Assert.IsNotNull(qbs.Where(x=> x.Name.Equals(_NewQuestionBank.Name)));
			Assert.IsNotNull(qbs.FirstOrDefault()?.Questions);
		}

		[Test]
		public async Task TestGetQuestionBankById()
		{
			var qbs = await _Driver.GetQuestionBankById(_NewQuestionBank.UniqueId);

			Assert.IsTrue(qbs.Name.Equals(_NewQuestionBank.Name));
		}

		#endregion

		#region GameSessions

		[Test]
		public async Task TestInsertGameSession()
		{
			var res = await _Driver.InsertGameSession(_NewGameSession, _NewAdmin.Email);

			Assert.IsTrue(res);
		}

		[Test]
		public async Task TestGetGameSessionsByInstructor()
		{
			var games = await _Driver.GetGameSessionsByInstructor(_NewAdmin.Email);

			Assert.IsNotNull(games.Where(x => x.Name.Equals(_NewGameSession.Name)));

			Assert.IsNotNull(games.FirstOrDefault()?.QuestionBank);
			Assert.IsNotNull(games.FirstOrDefault()?.Students);
			Assert.IsNotNull(games.FirstOrDefault()?.UniqueId);
		}

		[Test]
		public async Task TestGetGameSessionsById()
		{
			var game = await _Driver.GetGameSessionById(_NewGameSession.UniqueId);

			Assert.IsNotNull(game);

			Assert.IsTrue(game.Name?.Equals(_NewGameSession.Name));

			Assert.IsNotNull(game.QuestionBank);
			Assert.IsNotNull(game.Students);
			Assert.IsNotNull(game.UniqueId);
		}

		#endregion
	}
}
