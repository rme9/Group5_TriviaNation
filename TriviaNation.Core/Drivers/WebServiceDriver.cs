﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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

		public async Task<bool> InsertUser(IUser newUser)
		{
			var response = new HttpResponseMessage();

			if (newUser is StudentUser suser)
			{
				var content = JsonConvert.SerializeObject(suser);

				response = await _Client.PostAsync(_BaseRequestURL + "InsertStudent", new StringContent(content, Encoding.UTF8, "application/json"));
			}
			else if (newUser is AdminUser auser)
			{
				var content = JsonConvert.SerializeObject(auser);

				response = await _Client.PostAsync(_BaseRequestURL + "InsertAdmin", new StringContent(content, Encoding.UTF8, "application/json"));
			}

			if (response.IsSuccessStatusCode)
			{
				var didPost = await response.Content.ReadAsStringAsync();

				if (JsonConvert.DeserializeObject<bool>(didPost))
				{
					return true;
				}
			}

			return false;
		}

		public async Task<bool> InsertQuestionBank(IQuestionBank questionBank, string instructorsEmail)
		{
			var content = JsonConvert.SerializeObject(questionBank);

			var response = await _Client.PostAsync(_BaseRequestURL + "InsertQuestionBank/" + instructorsEmail, new StringContent(content, Encoding.UTF8, "application/json"));

			if (response.IsSuccessStatusCode)
			{
				var didPost = await response.Content.ReadAsStringAsync();

				if (JsonConvert.DeserializeObject<bool>(didPost))
				{
					return true;
				}
			}

			return false;
		}

		public async Task<bool> InsertGameSession(IGameSession gameSession, string instructorsEmail)
		{
			var content = JsonConvert.SerializeObject(gameSession);

			var response = await _Client.PostAsync(_BaseRequestURL + "InsertGameSession/" + instructorsEmail, new StringContent(content, Encoding.UTF8, "application/json"));

			if (response.IsSuccessStatusCode)
			{
				var didPost = await response.Content.ReadAsStringAsync();

				if (JsonConvert.DeserializeObject<bool>(didPost))
				{
					return true;
				}
			}

			return false;
		}

		public async Task<List<StudentUser>> GetAllUsersByInstructor(string instructorsEmail)
		{
			var students = new List<StudentUser>();

			var response = await _Client.GetAsync(_BaseRequestURL + "GetAllUsersByInstructor/" + instructorsEmail);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();

				students = JsonConvert.DeserializeObject<List<StudentUser>>(content);
			}

			return students;
		}

		public async Task<List<IQuestionBank>> GetQuestionBanksByInstructor(string instructorsEmail)
		{
			var questionBanks = new List<IQuestionBank>();

			var response = await _Client.GetAsync(_BaseRequestURL + "GetQuestionBanksByInstructor/" + instructorsEmail);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();

				questionBanks = JsonConvert.DeserializeObject<List<IQuestionBank>>(content);
			}

			return questionBanks;
		}

		public async Task<List<IGameSession>> GetGameSessionsByInstructor(string instructorEmail)
		{
			var gameSessions = new List<IGameSession>();

			var response = await _Client.GetAsync(_BaseRequestURL + "GetGameSessionsByInstructor/" + instructorEmail);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();

				gameSessions = JsonConvert.DeserializeObject<List<IGameSession>>(content);
			}

			return gameSessions;
		}

		public async Task<IUser> GetUserByEmail(string email)
		{
			IUser user = null;

			var response = await _Client.GetAsync(_BaseRequestURL + "GetUserByEmail/" + email);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();

				user = JsonConvert.DeserializeObject<IUser>(content);
			}

			return user ?? new StudentUser("", "");
		}

		public async Task<IQuestionBank> GetQuestionBankById(string uniqueId)
		{
			IQuestionBank questionBank = null;

			var response = await _Client.GetAsync(_BaseRequestURL + "GetQuestionBankById/" + uniqueId);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();

				questionBank = JsonConvert.DeserializeObject<IQuestionBank>(content);
			}

			return questionBank ?? new QuestionBank(null);
		}

		public async Task<IGameSession> GetGameSessionById(string uniqueId)
		{
			IGameSession gameSession = null;

			var response = await _Client.GetAsync(_BaseRequestURL + "GetGameSessionById/" + uniqueId);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();

				gameSession = JsonConvert.DeserializeObject<IGameSession>(content);
			}

			return gameSession;
		}

		public void Dispose()
		{
			_Client.Dispose();
		}
	}
}
