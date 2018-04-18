using Microsoft.VisualStudio.TestTools.UnitTesting;
using TriviaNation.Core.Drivers;
using TriviaNation.Core.Models;

namespace TriviaNation.Test
{
	[TestClass]
	public class LoginTest
	{
		private string _UserName = "rme9@students.uwf.edu";
		private string _Password = "password";
		private string _UserType = "admin";

		[TestMethod]
		public void Login()
		{
			using (var driver = new LoginDriver())
			{
				var user = driver.Login(_UserName, _Password, _UserType).Result;

				Assert.IsNotNull(user);
				Assert.IsTrue((user as AdminUser) != null);
				Assert.AreEqual(_UserName, user.Email);
			}
		}
	}
}
