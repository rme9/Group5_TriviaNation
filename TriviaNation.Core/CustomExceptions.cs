using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaNation.Core.Util.CustomExceptions
{
	public class ItemNotFoundException : ApplicationException
	{
		public ItemNotFoundException() : base()
		{

		}

		public ItemNotFoundException(string itemKey) : base($"Could not find item with key {itemKey}")
		{ 	
		}

		public ItemNotFoundException(string itemKey, string tableName) : base($"Could not find item with key \'{itemKey}\' in table \'{tableName}\'")
		{
		}
	}

	public class InvalidDatabaseObjectException : ApplicationException
	{
		public InvalidDatabaseObjectException() : base()
		{

		}

		public InvalidDatabaseObjectException(string itemIdentifier) : base($"Found item \'{itemIdentifier}\' but it was not a valid database entry")
		{

		}
	}


	public class DatabaseException : ApplicationException
	{

	}

}
