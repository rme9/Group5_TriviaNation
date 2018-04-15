using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TriviaNation.Core.Drivers;
using TriviaNation.Core.Models;

namespace TriviaNation.Test
{
	[TestClass]
	public class DynamoDBTest
	{
		private DynamoDBDriver _driver;

		#region TestParameters

		private AdminUser _NewAdmin => new AdminUser("Rebecca Elliff", "rme9@students.uwf.edu");

		private StudentUser _NewUser => new StudentUser("Ron Weasley", "RWeasley@email.com") { InstructorId = _NewAdmin.Email };

		private List<IQuestion> _Questions;

		private QuestionBank _NewQuestionBank;

		private GameSession _NewGameSession;

		#endregion

		#region Setup

		[TestInitialize]
		public void testSetup()
		{
			_driver = new DynamoDBDriver();

			CreateQuestions();

			CreateQuestionBank();

			CreateGameSession();
		}

		public void CreateQuestions()
		{
			_Questions = new List<IQuestion>
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
				Students = new List<IUser>
				{
					_NewUser
				},
				Name = "TestGame1",
				QuestionBank = _NewQuestionBank
			};

		}

		#endregion

		#region User

		[TestMethod]
		public void TestInsertUser()
		{
			var newStu = _driver.InsertUser(_NewUser, _NewAdmin.Email);

			Assert.IsTrue(newStu);
		}

		[TestMethod]
		public void TestGetAllUsersByInstructor()
		{
			var users = _driver.GetAllUsersByInstructor(_NewAdmin.Email);

			Assert.IsNotNull(users.Where(x => x.Name.Equals("Harry Potter")));
			Assert.IsNotNull(users.Where(x => x.Name.Equals("Ron Weasley")));
		}

		[TestMethod]
		public void TestGetUserByEmail()
		{
			var student = _driver.GetUserByEmail(_NewUser.Email);

			Assert.IsTrue(_NewUser.Name.Equals(student.Name));
			Assert.IsTrue(_NewUser.Email.Equals(student.Email));
		}

		#endregion

		#region QuestionBanks

		[TestMethod]
		public void TestInsertQuestionBank()
		{
			var res = _driver.InsertQuestionBank(_NewQuestionBank, _NewAdmin.Email);

			Assert.IsTrue(res);
		}

		[TestMethod]
		public void TestGetAllQuestionBanksByInstructor()
		{
			var qbs = _driver.GetQuestionBanksByInstructor(_NewAdmin.Email);

			Assert.IsNotNull(qbs.Where(x=> x.Name.Equals(_NewQuestionBank.Name)));
			Assert.IsNotNull(qbs.FirstOrDefault()?.Questions);
		}

		[TestMethod]
		public void TestGetQuestionBankById()
		{
			var qbs = _driver.GetQuestionBankById(_NewQuestionBank.UniqueId);

			Assert.IsTrue(qbs.Name.Equals(_NewQuestionBank.Name));
		}

		#endregion

		#region GameSessions

		[TestMethod]
		public void TestInsertGameSession()
		{
			var res = _driver.InsertGameSession(_NewGameSession, _NewAdmin.Email);

			Assert.IsTrue(res);
		}

		[TestMethod]
		public void TestGetGameSessionsByInstructor()
		{
			var games = _driver.GetGameSessionsByInstructor(_NewAdmin.Email);

			Assert.IsNotNull(games.Where(x => x.Name.Equals(_NewGameSession.Name)));

			Assert.IsNotNull(games.FirstOrDefault()?.QuestionBank);
			Assert.IsNotNull(games.FirstOrDefault()?.Students);
			Assert.IsNotNull(games.FirstOrDefault()?.UniqueId);
		}

		[TestMethod]
		public void TestGetGameSessionsById()
		{
			var game = _driver.GetGameSessionById(_NewGameSession.UniqueId);

			Assert.IsNotNull(game);

			Assert.IsTrue(game.Name.Equals(_NewGameSession.Name));

			Assert.IsNotNull(game.QuestionBank);
			Assert.IsNotNull(game.Students);
			Assert.IsNotNull(game.UniqueId);
		}

		#endregion

	}
}
