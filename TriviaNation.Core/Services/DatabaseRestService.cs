using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaNation.Core.Drivers;
using TriviaNation.Core.Models;

namespace TriviaNation.Services
{
	public class DatabaseRestService //: IDatabaseService, IRestService
	{
		private DynamoDBDriver _Driver;

		public DatabaseRestService()
		{
			_Driver = new DynamoDBDriver();
		}

		public bool Insert(object newObject, string ownersId)
		{
			throw new NotImplementedException();
		}

		public IList GetAllByOwner(Type objectType, string ownersId)
		{
			if (objectType == typeof(IUser))
			{
				return _Driver.GetAllUsersByInstructor(ownersId);
			}
			else if (objectType == typeof(IQuestionBank))
			{

			}
			else if (objectType == typeof(IGameSession))
			{

			}

			return new List<object>();
		}

		public object GetOneByKey(Type objectType, string key)
		{
			throw new NotImplementedException();
		}
	}
}
