using System;
using System.Collections.Generic;
using System.Linq;
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
	public class DynamoDBDriver : IDatabaseDriver, IDisposable
	{
		private readonly BasicAWSCredentials _awsCredentials;

		private AmazonDynamoDBClient _Client;

		public DynamoDBDriver()
		{
			_awsCredentials = new BasicAWSCredentials("AKIAJVY3C5ER7URGFMVQ", "XMlT3UlWOCzpeX9jEfQIx+fGuL24mqJAdQVf7lsS");

			_Client = new AmazonDynamoDBClient(_awsCredentials, RegionEndpoint.USEast1);
		}

		public bool Insert(string tableName, Dictionary<string, AttributeValue> item)
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
				_Client.PutItem(request);

				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return false;
			}
		}

		public List<Dictionary<string, AttributeValue>> Scan(string tableName, Dictionary<string, AttributeValue> searchAttributes)
		{
			try
			{
				var request = new ScanRequest
				{
					TableName = tableName,
					ExpressionAttributeValues = searchAttributes
				};

				var resp = _Client.Scan(request);

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
