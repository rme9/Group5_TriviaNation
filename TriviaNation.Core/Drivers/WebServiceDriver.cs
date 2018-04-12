﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TriviaNation.Core.Models;

namespace TriviaNation.Core.Drivers
{
	public class WebServiceDriver : IDisposable
	{
		public readonly string _BaseRequestURL = @"https://nxumf3nld2.execute-api.us-east-1.amazonaws.com/Prod/admin/";

		public HttpClient _Client;


		public WebServiceDriver()
		{
			_Client = new HttpClient();
		}

		public async Task<List<StudentUser>> GetAllUsersByInstructor(string instructorsEmail)
		{
			var students = new List<StudentUser>();

			var response = await _Client.GetAsync(_BaseRequestURL + instructorsEmail);

			if (response.IsSuccessStatusCode)
			{
				students = await response.Content.ReadAsAsync<List<StudentUser>>();
			}

			return students;
		}

		public void Dispose()
		{
			_Client.Dispose();
		}
	}
}
