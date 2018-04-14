using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using TriviaNation.Core.Drivers;
using TriviaNation.Core.Models;
using TriviaNation.Core.Util.CustomExceptions;
using Table = Amazon.DynamoDBv2.DocumentModel.Table;

namespace TriviaNation.Drivers
{
	public class DynamoDBDriver : IDisposable
	{
		private readonly BasicAWSCredentials _awsCredentials;

		private AmazonDynamoDBClient _Client;

		public DynamoDBDriver()
		{
			_Client = new AmazonDynamoDBClient(_awsCredentials, RegionEndpoint.USEast1);
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
		
		#region IDisposable Implementation
		public void Dispose()
		{
			_Client?.Dispose();
		}

		#endregion
	}
}
