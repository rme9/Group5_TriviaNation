using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using TriviaNation.Core.Models;
using TriviaNation.Rest.Services;
using TriviaNation.Services;

namespace TriviaNation.Rest
{
	[Route("[controller]/[action]")]
	public class AdminController : Controller
	{
		private readonly DynamoDatabaseService _WebService;

		public AdminController()
		{
			_WebService = new DynamoDatabaseService();
		}

		public IEnumerable<string> Get()
		{
			return new List<string> { "ASP.NET", "Docker", "Windows Containers" };
		}

		#region webservice requests

		[HttpPost]
		public bool InsertStudent([FromBody] StudentUser newUser) => _WebService.InsertUser(newUser);

		[HttpPost]
		public bool InsertAdmin([FromBody] AdminUser newUser) => _WebService.InsertUser(newUser);

		[HttpPost("{instructorsEmail}")]
		public bool InsertQuestionBank([FromBody] IQuestionBank newQuestionBank, string instructorsEmail) => _WebService.InsertQuestionBank(newQuestionBank, instructorsEmail);

		[HttpPost("{instructorsEmail}")]
		public bool InsertGameSession(IGameSession newGameSession, string instructorsEmail) => _WebService.InsertGameSession(newGameSession, instructorsEmail);

		[HttpGet("{instructorEmail}")]
		public List<StudentUser> GetAllUsersByInstructor(string instructorEmail) => _WebService.GetAllUsersByInstructor(instructorEmail);

		[HttpGet("{instructorEmail}")]
		public List<IQuestionBank> GetQuestionBanksByInstructor(string instructorsEmail) => _WebService.GetQuestionBanksByInstructor(instructorsEmail);

		[HttpGet("{instructorEmail}")]
		public List<IGameSession> GetGameSessionsByInstructor(string instructorEmail) => _WebService.GetGameSessionsByInstructor(instructorEmail);

		[HttpGet("{email}")]
		public IUser GetUserByEmail(string email) => _WebService.GetUserByEmail(email);

		[HttpGet("{uniqueId}")]
		public IQuestionBank GetQuestionBankById(string uniqueId) => _WebService.GetQuestionBankById(uniqueId);

		[HttpGet("{uniqueId}")]
		public IGameSession GetGameSessionById(string uniqueId) => _WebService.GetGameSessionById(uniqueId);

		#endregion
	}
}
