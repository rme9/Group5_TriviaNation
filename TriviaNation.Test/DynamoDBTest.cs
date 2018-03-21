using System;
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

		[TestMethod]
		public void TestMethod1()
		{
			var newStu = new StudentUser("Harry Potter", "hpotter@email.com");
			newStu.InstructorId = "rme9@students.uwf.edu";
			_driver.InsertUser(newStu);
		}

		[TestMethod]
		public void TestGetAllUsers()
		{
			_driver.GetAllUsers("rme9@students.uwf.edu");
		}
	}
}
