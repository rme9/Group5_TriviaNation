using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TriviaNation.Drivers;
using TriviaNation.Models;
using TriviaNation.Util.CustomExceptions;

namespace TriviaNation
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App() : base()
		{
		}


		/// <summary>
		/// Attempts to find the user in the database.
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="password"></param>
		/// <returns>Null on succes, or an error message if not successful</returns>
		public static string OnLogin(string userId, string password, string userType)
		{
			string loginMessage = null;
			var db = new DynamoDBDriver();

			try
			{
				var user = db.GetUserByEmail(userId);

				if ((user as StudentUser)?.InstructorId == null && userType.Equals("User"))
				{
					throw new Exception();
				}

				if ((user as AdminUser) == null && userType.Equals("Admin"))
				{
					throw new Exception();
				}

				Application.Current.Properties.Add("LoggedInUserId", user.Email);
				Application.Current.Properties.Add("LoggedInUserName", user.Name);
			}
			catch (Exception ex)
			{
				if (ex is ItemNotFoundException)
				{
					return "Invalid Username";
				}
				else
				{
					return "Unable to Login.";
				}
			}

			return loginMessage;
		}

		public static void OnLogout()
		{
			Application.Current.Properties.Remove("LoggedInUserId");
			Application.Current.Properties.Remove("LoggedInUserName");

			//Application.Current.Shutdown();
		}
	}
}
