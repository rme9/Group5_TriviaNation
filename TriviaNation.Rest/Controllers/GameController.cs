using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TriviaNation.Core.Models;
using TriviaNation.Rest.Services;

namespace TriviaNation.Rest.Controllers
{
	[Produces("application/json")]
	[Route("game/[action]")]
    public class GameController : Controller
    {
		private GameService _Service;

		public GameController()
		{
			_Service = new GameService();
		}

		[HttpGet("{str}")]
		public string Echo(string str)
		{
			return str;
		}

	    [HttpPost("{gameSessionId}")]
	    public bool InsertTerritories([FromBody] List<Territories> territories, string gameSessionId) => _Service.InsertTerritories(territories, gameSessionId);

		[HttpGet("{studentId}")]
		public List<GameSession> GetGameSessionsByStudent(string studentId) => _Service.GetGameSessionsByStudent(studentId);

	    [HttpGet("{gameSessionId}")]
	    public List<Territories> GetTerritoriesByGameSession(string gameSessionId) => _Service.GetTerritoriesByGameSession(gameSessionId);

    }
}