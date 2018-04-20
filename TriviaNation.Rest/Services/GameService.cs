using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2.Model;
using TriviaNation.Core.Models;
using TriviaNation.Drivers;

namespace TriviaNation.Rest.Services
{
	public class GameService
	{
		private DynamoDBDriver _Driver;

		#region Table Name Strings

		private readonly string _UserTableName = "se2_user";

		private readonly string _QuestionBankTableName = "se2_questionbank";

		private readonly string _GameSessionTableName = "se2_gamesession";

		private readonly string _TerritoryTableName = "se2_territory";

		#endregion

		public GameService()
		{
			_Driver = new DynamoDBDriver();
		}


		public bool InsertTerritories(List<Territories> territories, string sessionId)
		{
			if (territories == null)
			{
				return false;
			}

			var territoryList = new Dictionary<string, AttributeValue>();

			foreach (var terr in territories)
			{
				var dictionary = new Dictionary<string, AttributeValue>
				{
					["name"] = new AttributeValue { S = terr.Name },
					["controller_name"] = new AttributeValue { S = terr.ControllerName },
					["control_status"] = new AttributeValue{BOOL = terr.ControlStatus},

				};
				territoryList.Add("territories", new AttributeValue { M = dictionary });
			}

			// Build the attribute dictionary
			var attributes =
				new Dictionary<string, AttributeValue>
				{
					["session_id"] = new AttributeValue { S = sessionId},
					["territories"] = new AttributeValue { M = territoryList },
				};

			return _Driver.Insert(_TerritoryTableName, attributes).Result;
		}

		public List<GameSession> GetGameSessionsByStudent(string studentId)
		{
			List<GameSession> sessions;
			IUser student;
			using (AdminService asrv = new AdminService())
			{
				 student = asrv.GetUserByEmail(studentId);

				 sessions = asrv.GetGameSessionsByInstructor((student as StudentUser).InstructorId);
			}

			sessions.RemoveAll(x => !x.Students.Any(y => y.Email.Equals(studentId)));

			return sessions;
		}

		public List<Territories> GetTerritoriesByGameSession(string gameSessionId)
		{
			if (string.IsNullOrWhiteSpace(gameSessionId))
			{
				return null;
			}

			var filter = "session_id = :sid";

			var searchValues = new Dictionary<string, AttributeValue>
			{
				{":sid", new AttributeValue {S = gameSessionId}}
			};

			var resp = _Driver.Scan(_TerritoryTableName, searchValues, filter).Result;

			var territories= new List<Territories>();

			#region out variables

			AttributeValue name;
			AttributeValue contName;
			AttributeValue contStatus;
			AttributeValue territoryList;
			AttributeValue sessionId;

			#endregion

			foreach (var item in resp)
			{
				item.TryGetValue("session_id", out sessionId);
				item.TryGetValue("territories", out territoryList);


				if (territoryList != null && territoryList.IsMSet)
				{
					foreach (var keyPair in territoryList.M)
					{
						keyPair.Value.M.TryGetValue("name", out name);
						keyPair.Value.M.TryGetValue("controller_name", out contName);
						keyPair.Value.M.TryGetValue("control_status", out contStatus);

						territories.Add(new Territories(name?.S, contStatus?.BOOL ?? false, contName?.S));
					}
				}
			}

			return territories;
		}
	}
}
