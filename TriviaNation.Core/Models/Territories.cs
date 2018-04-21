

namespace TriviaNation.Core.Models
{
	public class Territories
	{
		public string Name { get; set; }

		public string ControllerName { get; set; }

		public bool ControlStatus { get; set; }

		public string SessionId { get; set; }

		public Territories(string name, bool isConstrolled, string controllerName)
		{
			Name = name;
			ControlStatus = isConstrolled;
			ControllerName = controllerName;

		}

		public Territories(string name, bool isConstrolled, string controllerName, string sessionId)
		{
			Name = name;
			ControlStatus = isConstrolled;
			ControllerName = controllerName;
			SessionId = sessionId;
		}

		public Territories()
		{

		}
	}
}
