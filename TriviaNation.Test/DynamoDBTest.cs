﻿using System;
using System.Collections.Generic;
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
			var newStu = new StudentUser("Harry Potter", "hpotter@email.com") {InstructorId = "rme9@students.uwf.edu"};
			_driver.InsertUser(newStu);
		}

		[TestMethod]
		public void TestGetAllUsers()
		{
			_driver.GetAllUsersByInstructor("rme9@students.uwf.edu");
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


			_driver.InsertQuestionBank(qb);
		}

		[TestMethod]
		public void TestGetAllQuestionBanksByInstructor()
		{
			var qbs = _driver.GetQuestionBanksByInstructor("rme9@students.uwf.edu");
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
				QuestionBank = new QuestionBank()
			};

			_driver.InsertGameSession(gs);
		}

		#endregion

	}
}
