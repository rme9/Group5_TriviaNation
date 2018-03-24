using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TriviaNation.Drivers;
using TriviaNation.Models;

namespace TriviaNation.Test
{
	[TestClass]
	public class DynamoDBTest
	{
		private DynamoDBDriver _driver;

		[TestInitialize]
		public void testSetup()
		{
			_driver = new DynamoDBDriver();
		}

		#region User

		[TestMethod]
		public void TestInsertUser()
		{
			var newStu = new StudentUser("Ron Weasley", "RWeasley@email.com") {InstructorId = "rme9@students.uwf.edu"};
			_driver.InsertUser(newStu, "rme9@students.uwf.edu");
		}

		[TestMethod]
		public void TestGetAllUsersByInstructor()
		{
			var users = _driver.GetAllUsersByInstructor("rme9@students.uwf.edu");

			Assert.IsNotNull(users.Where(x => x.Name.Equals("Harry Potter")));
			Assert.IsNotNull(users.Where(x => x.Name.Equals("Ron Weasley")));
		}

		[TestMethod]
		public void TestGetUserByEmail()
		{
			var student = _driver.GetUserByEmail("hpotter@email.com");

			Assert.IsTrue("Harry Potter".Equals(student.Name));
			Assert.IsTrue("hpotter@email.com".Equals(student.Email));
		}

		#endregion

		#region QuestionBanks

		[TestMethod]
		public void TestInsertQuestionBank()
		{
			var questions = new List<IQuestion>
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

			var qb = new QuestionBank
			{
				Questions = questions,
				Name = "US_History_1"
			};

			_driver.InsertQuestionBank(qb, "rme9@students.uwf.edu");
		}

		[TestMethod]
		public void TestGetAllQuestionBanksByInstructor()
		{
			var qbs = _driver.GetQuestionBanksByInstructor("rme9@students.uwf.edu");


			Assert.IsNotNull(qbs.Where(x=> x.Name.Equals("US_History_1")));
		}

		[TestMethod]
		public void TestGetQuestionBankById()
		{
			var qbs = _driver.GetQuestionBankById("08a9df568ced4fa393e4a78d7da94649");

			Assert.IsTrue(qbs.Name.Equals("US_History_1"));
		}

		#endregion

		#region GameSessions

		[TestMethod]
		public void TestInsertGameSession()
		{
			var gs = new GameSession()
			{
				Students = new List<IUser>
				{
					new StudentUser("Harry Potter", "hpotter@email.com") {InstructorId = "rme9@students.uwf.edu"}
				},
				Name = "TestGame1",
				QuestionBank = new QuestionBank("d187d42797634e5f9277faf5fd78bcf4")
			};

			_driver.InsertGameSession(gs, "rme9@students.uwf.edu");
		}

		[TestMethod]
		public void TestGetGameSessionsByInstructor()
		{
			var games = _driver.GetGameSessionsByInstructor("rme9@students.uwf.edu");

			Assert.IsNotNull(games.Where(x => x.Name.Equals("TestGame1")));

			Assert.IsNotNull(games.FirstOrDefault()?.QuestionBank);
			Assert.IsNotNull(games.FirstOrDefault()?.Students);
			Assert.IsNotNull(games.FirstOrDefault()?.UniqueId);
		}

		[TestMethod]
		public void TestGetGameSessionsById()
		{
			var game = _driver.GetGameSessionById("29a9d5c256a24cc8b05a32862609d8e7");

			Assert.IsNotNull(game.Name.Equals("TestGame1"));

			Assert.IsNotNull(game.QuestionBank);
			Assert.IsNotNull(game.Students);
			Assert.IsNotNull(game.UniqueId);
		}

		#endregion

	}
}
