using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using TriviaNation.Models;

using Table = Amazon.DynamoDBv2.DocumentModel.Table;

namespace TriviaNation.Drivers
{
	public class DynamoDBDriver
	{
		private readonly BasicAWSCredentials _awsCredentials;

		private AmazonDynamoDBClient _Client;

		public DynamoDBDriver()
		{
			_awsCredentials = new BasicAWSCredentials("AKIAIX3UY7VRKCCXOAAA", "EZXpCk5qjO7JpGyY6n7dGLUPMl3AKeXyFTL49zOV");

			_Client = new AmazonDynamoDBClient(_awsCredentials, RegionEndpoint.USEast1);
		}

		public async void InsertUser(IUser newUser)
		{
			try
			{
				// Build the attribute dictionary
				var attributes =
					new Dictionary<string, AttributeValue>
					{
						["email"] = new AttributeValue {S = newUser.Email},
						["name"] = new AttributeValue {S = newUser.Name},
					};

				if (newUser is StudentUser)
				{
					attributes.Add("isadmin", new AttributeValue {BOOL = false});
					attributes.Add("instructor_id", new AttributeValue { S = "rme9@students.uwf.edu" }); // TODO this should be the email of the admin that added it
				}
				else if (newUser is AdminUser)
				{
					attributes.Add("isadmin", new AttributeValue { BOOL = true });
				}

				// Create PutItem request
				PutItemRequest request = new PutItemRequest
				{
					TableName = "se2_user",
					Item = attributes
				};

				// Issue PutItem request
				var resp = _Client.PutItem(request);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public List<IUser> GetAllUsers(string instructorsEmail)
		{
			try
			{
				var request = new ScanRequest
				{
					TableName = "se2_user",
					ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
						{":instr", new AttributeValue { S =  "rme9@students.uwf.edu" }}
					},
					FilterExpression = "instructor_id = :instr"
				};

				var resp = _Client.Scan(request);

				var users = new List<IUser>();

				foreach (var item in resp.Items)
				{
					item.TryGetValue("name", out var name);
					item.TryGetValue("email", out var email);

					users.Add(new StudentUser(name?.S, email?.S)
					{
						InstructorId = instructorsEmail
					});
				}

				return users;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return new List<IUser>();
			}

		}

	}
}
