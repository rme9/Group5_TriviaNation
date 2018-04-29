using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TriviaNation.Core.Drivers;
using TriviaNation.Core.Models;
using TriviaNation.Core.Util.CustomExceptions;

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
		public static async Task OnLogin(string userId, string password)
		{
			LoginMessage = null;

			using (var db = new LoginDriver())
			{
				try
				{
					var user = await db.Login(userId, password, "Student");
					
					if ((user as StudentUser) == null)
					{
						throw new Exception();
					}

                    if(user.Name == "" || user.Email == "")
                    {
                        throw new Exception();
                    }

					Application.Current.Properties.Add("LoggedInUserId", user.Email);
					Application.Current.Properties.Add("LoggedInUserName", user.Name);
				}
				catch (Exception ex)
				{
					LoginMessage = "Unable to Login.";
				}
			}

		}

		public static string LoginMessage { get; set; }

		public static void OnLogout()
		{
			Application.Current.Properties.Remove("LoggedInUserId");
			Application.Current.Properties.Remove("LoggedInUserName");

			Application.Current.Shutdown();
		}
	}
}
