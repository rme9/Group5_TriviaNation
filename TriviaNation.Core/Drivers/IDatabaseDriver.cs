using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;

namespace TriviaNation.Core.Drivers
{
	public interface IDatabaseDriver
	{
		bool Insert(string tableName, Dictionary<string, AttributeValue> item);

		List<Dictionary<string, AttributeValue>> Scan(string tableName, Dictionary<string, AttributeValue> searchAttributes);
	}
}
