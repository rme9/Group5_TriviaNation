using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TriviaNation.Core.Models;

namespace TriviaNation.Core.Drivers
{
	public class LoginDriver : IDisposable
	{

		public readonly string _BaseRequestURL = @"https://nxumf3nld2.execute-api.us-east-1.amazonaws.com/Prod/login/";

		public HttpClient _Client;


		public LoginDriver()
		{
			_Client = new HttpClient();
		}

		public async Task<IUser> Login(string email, string password, string userType)
		{
			IUser user = null;
            string useremail;

			var request = _BaseRequestURL + email + "/" + password;

			var response = await _Client.GetAsync(request);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
                

				if (userType.Equals("Student"))
				{
					user = JsonConvert.DeserializeObject<StudentUser>(content);
				}
				else
				{
					user = JsonConvert.DeserializeObject<AdminUser>(content);
				}

				user.Password = null;
			}

			return user ?? new StudentUser("", "");
		}

		public void Dispose()
		{
		}
	}
}
