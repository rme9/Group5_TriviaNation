using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace TriviaNation.Drivers
{
	public class DynamoDBDriver : IDisposable
	{
		private AmazonDynamoDBClient _Client;

		public DynamoDBDriver()
		{
			_Client = new AmazonDynamoDBClient(RegionEndpoint.USEast1);
		}

		public async Task<bool> Insert(string tableName, Dictionary<string, AttributeValue> item)
		{
			try
			{
				// Create PutItem request
				PutItemRequest request = new PutItemRequest
				{
					TableName = tableName,
					Item = item
				};

				// Issue PutItem request
				await _Client.PutItemAsync(request).ConfigureAwait(false);

				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return false;
			}
		}

		public async Task<List<Dictionary<string, AttributeValue>>> Scan(string tableName, Dictionary<string, AttributeValue> searchAttributes, string filterExpression)
		{
			try
			{
				var request = new ScanRequest
				{
					TableName = tableName,
					ExpressionAttributeValues = searchAttributes,
					FilterExpression = filterExpression

				};

				var resp = await _Client.ScanAsync(request).ConfigureAwait(false);

				return resp.Items;
			}
			catch (Exception ex)
			{
				return new List<Dictionary<string, AttributeValue>>();
			}
		}

		public async Task<bool> Delete(string tableName, Dictionary<string, AttributeValue> itemAttr)
		{
			try
			{
				var request = new DeleteItemRequest
				{
					TableName = tableName,
					Key = itemAttr,
				};

				var response = await _Client.DeleteItemAsync(request);

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		#region IDisposable Implementation
		public void Dispose()
		{
			_Client?.Dispose();
		}

		#endregion
	}
}
