using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TriviaNation.Core.Models;
using TriviaNation.Rest.Services;

namespace TriviaNation.Rest
{
	[Route("admin/[action]")]
	public class AdminController : Controller
	{
		private readonly AdminService _WebService;

		public AdminController()
		{
			_WebService = new AdminService();
		}

		public IEnumerable<string> Get()
		{
			return new List<string> { "ASP.NET", "Docker", "Windows Containers" };
		}

		#region webservice requests

		#region Inserts

		[HttpPost]
		public bool InsertStudent([FromBody] StudentUser newUser) => _WebService.InsertUser(newUser);

		[HttpPost]
		public bool InsertAdmin([FromBody] AdminUser newUser) => _WebService.InsertUser(newUser);

		[HttpPost("{instructorsEmail}")]
		public bool InsertQuestionBank([FromBody] IQuestionBank newQuestionBank, string instructorsEmail) => _WebService.InsertQuestionBank(newQuestionBank, instructorsEmail);

		[HttpPost("{instructorsEmail}")]
		public bool InsertGameSession([FromBody] IGameSession newGameSession, string instructorsEmail) => _WebService.InsertGameSession(newGameSession, instructorsEmail);

		#endregion

		#region Gets

		[HttpGet("{instructorEmail}")]
		public List<StudentUser> GetAllUsersByInstructor(string instructorEmail) => _WebService.GetAllUsersByInstructor(instructorEmail);

		[HttpGet("{instructorEmail}")]
		public List<QuestionBank> GetQuestionBanksByInstructor(string instructorEmail) => _WebService.GetQuestionBanksByInstructor(instructorEmail);

		[HttpGet("{instructorEmail}")]
		public List<GameSession> GetGameSessionsByInstructor(string instructorEmail) => _WebService.GetGameSessionsByInstructor(instructorEmail);

		[HttpGet("{email}")]
		public IUser GetUserByEmail(string email) => _WebService.GetUserByEmail(email);

		[HttpGet("{uniqueId}")]
		public IQuestionBank GetQuestionBankById(string uniqueId) => _WebService.GetQuestionBankById(uniqueId);

		[HttpGet("{uniqueId}")]
		public IGameSession GetGameSessionById(string uniqueId) => _WebService.GetGameSessionById(uniqueId);

		#endregion

		#region Deletes

		[HttpDelete]
		public bool DeleteUser([FromBody] StudentUser userId) => _WebService.DeleteUser(userId);

		#endregion


		#endregion
	}
}
