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
using MongoDB.Driver.Core.Operations;
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

		#region Users

		/// <summary>
		/// Inserts a new user in the user table of the database. This method can be used for both
		/// StudentUsers and AdminUsers.
		/// </summary>
		/// <param name="newUser">A new user to insert in the database.</param>
		public void InsertUser(IUser newUser)
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
					attributes.Add("instructor_id",
						new AttributeValue {S = "rme9@students.uwf.edu"}); // TODO this should be the email of the admin that added it
				}
				else if (newUser is AdminUser)
				{
					attributes.Add("isadmin", new AttributeValue {BOOL = true});
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

		/// <summary>
		/// Retrieves all the StudentUsers who were created by a specified AdminUser. This method is intended to 
		/// be used to pull a single instructor's students.
		/// </summary>
		/// <param name="instructorsEmail">The email saved in the account of the AdminUser whose Students should be pulled.</param>
		/// <returns></returns>
		public List<IUser> GetAllUsersByInstructor(string instructorsEmail)
		{
			try
			{
				var request = new ScanRequest
				{
					TableName = "se2_user",
					ExpressionAttributeValues = new Dictionary<string, AttributeValue>
					{
						{":instr", new AttributeValue {S = instructorsEmail}}
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

		#endregion

		#region QuestionBanks

		public void InsertQuestionBank(IQuestionBank newQuestionBank)
		{
			try
			{
				// Turn the list of questions into a dictionary
				var questions = new Dictionary<string, AttributeValue>();

				foreach (var q in newQuestionBank.Questions)
				{
					var dictionary = new Dictionary<string, AttributeValue>
					{
						["correct_answer"] = new AttributeValue {S = q.CorrectAnswer},
						["alternate_answers"] = new AttributeValue {SS = q.AlternateAnswers}
					};
					questions.Add(q.Body, new AttributeValue {M = dictionary});
				}

				// Build the attribute dictionary
				var attributes =
					new Dictionary<string, AttributeValue>
					{
						["name"] = new AttributeValue {S = newQuestionBank.Name},
						["questions"] = new AttributeValue {M = questions},
						["unique_id"] = new AttributeValue { S = newQuestionBank.UniqueId },
						["instructor_id"] = new AttributeValue { S = "rme9@students.uwf.edu"} // TODO add a way to get the current logged in user
					};

				// Create PutItem request
				PutItemRequest request = new PutItemRequest
				{
					TableName = "se2_questionbank",
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

		public List<IQuestionBank> GetQuestionBanksByInstructor(string instructorsEmail)
		{
			try
			{
				var request = new ScanRequest
				{
					TableName = "se2_questionbank",
					ExpressionAttributeValues = new Dictionary<string, AttributeValue>
					{
						{":instr", new AttributeValue {S = instructorsEmail}}
					},
					FilterExpression = "instructor_id = :instr"
				};

				var resp = _Client.Scan(request);

				var questionbanks = new List<IQuestionBank>();

				foreach (var item in resp.Items)
				{
					item.TryGetValue("name", out var name);
					item.TryGetValue("unique_id", out var unid);

					var didGetQuestions = item.TryGetValue("questions", out var qs);

					var questionList = new List<IQuestion>();

					if(didGetQuestions && qs.IsMSet)
					{
						foreach (var thing in qs.M)
						{
							thing.Value.M.TryGetValue("correct_answer", out var corrAns);
							thing.Value.M.TryGetValue("alternate_answers", out var altAns);

							questionList.Add( new Question
							{
								Body = thing.Key,
								AlternateAnswers = altAns?.SS,
								CorrectAnswer = corrAns?.S
							});
						}
					}

					questionbanks.Add(new QuestionBank(unid?.S)
					{
						Name = name?.S,
						Questions = questionList
					});
				}

				return questionbanks;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return new List<IQuestionBank>();
			}
		}


		#endregion

	}
}
