using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TriviaNation.Core.Models;

namespace TriviaNation.Core.Drivers
{
	public class GameDriver : IDisposable
	{
		public readonly string _BaseRequestURL = @"https://nxumf3nld2.execute-api.us-east-1.amazonaws.com/Prod/game/";

		public HttpClient _Client;


		public GameDriver()
		{
			_Client = new HttpClient();
		}

	
		public async Task<bool> InsertTerritories(List<Territories> territories, string gameSessionId)
		{
			var content = JsonConvert.SerializeObject(territories);

			var response = await _Client.PostAsync(_BaseRequestURL + "InsertTerritories/" + gameSessionId, new StringContent(content, Encoding.UTF8, "application/json")).ConfigureAwait(false);

			if (response.IsSuccessStatusCode)
			{
				var didPost = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

				if (JsonConvert.DeserializeObject<bool>(didPost))
				{
					return true;
				}
			}

			return false;
		}

		public async Task<List<GameSession>> GetGameSessionsByStudent(string studentId)
		{
			var sessions = new List<GameSession>();

			var response = await _Client.GetAsync(_BaseRequestURL + "GetGameSessionsByStudent/" + studentId);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();

				sessions = JsonConvert.DeserializeObject<List<GameSession>>(content);
			}

			return sessions;
		}


		public async Task<List<Territories>> GetTerritoriesByGameSession(string gameSessionId)
		{
			var territories = new List<Territories>();

			var response = await _Client.GetAsync(_BaseRequestURL + "GetTerritoriesByGameSession/" + gameSessionId);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();

				territories = JsonConvert.DeserializeObject<List<Territories>>(content);
			}

			return territories;
		}

		public void Dispose()
		{
			_Client?.Dispose();
		}
	}
}
