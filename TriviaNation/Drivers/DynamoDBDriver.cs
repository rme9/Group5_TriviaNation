using System;
using System.Collections.Generic;
using System.Linq;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using TriviaNation.Models;
using TriviaNation.Util.CustomExceptions;
using Table = Amazon.DynamoDBv2.DocumentModel.Table;

namespace TriviaNation.Drivers
{
	public class DynamoDBDriver : IDisposable
	{
		private readonly BasicAWSCredentials _awsCredentials;

		private AmazonDynamoDBClient _Client;

		#region Table Name Strings

		private readonly string _UserTableName = "se2_user";

		private readonly string _QuestionBankTableName = "se2_questionbank";

		private readonly string _GameSessionTableName = "se2_gamesession";

		#endregion

		public DynamoDBDriver()
		{
			_awsCredentials = new BasicAWSCredentials("accesskey", "secretkey");

			_Client = new AmazonDynamoDBClient(_awsCredentials, RegionEndpoint.USEast1);
		}

		#region Users

		/// <summary>
		/// Inserts a new user in the user table of the database. This method can be used for both
		/// StudentUsers and AdminUsers.
		/// </summary>
		/// <param name="newUser">A new user to insert in the database.</param>
		/// <param name="instructorsEmail"></param>
		public bool InsertUser(IUser newUser, string instructorsEmail)
		{
			if (newUser == null || string.IsNullOrWhiteSpace(instructorsEmail))
			{
				throw new ArgumentNullException();
			}

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
						new AttributeValue {S = instructorsEmail});
				}
				else if (newUser is AdminUser)
				{
					attributes.Add("isadmin", new AttributeValue {BOOL = true});
				}

				// Create PutItem request
				PutItemRequest request = new PutItemRequest
				{
					TableName = _UserTableName,
					Item = attributes
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

		/// <summary>
		/// Retrieves all the StudentUsers who were created by a specified AdminUser. This method is intended to 
		/// be used to pull a single instructor's students.
		/// </summary>
		/// <param name="instructorsEmail">The email saved in the account of the AdminUser whose Students should be pulled.</param>
		/// <returns></returns>
		public List<StudentUser> GetAllUsersByInstructor(string instructorsEmail)
		{
			if (string.IsNullOrWhiteSpace(instructorsEmail))
			{
				throw new ArgumentNullException(nameof(instructorsEmail));
			}

			try
			{
				var request = new ScanRequest
				{
					TableName = _UserTableName,
					ExpressionAttributeValues = new Dictionary<string, AttributeValue>
					{
						{":instr", new AttributeValue {S = instructorsEmail}}
					},
					FilterExpression = "instructor_id = :instr"
				};

				var resp = _Client.Scan(request);

				var users = new List<StudentUser>();

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

				return new List<StudentUser>();
			}
		}

		public IUser GetUserByEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				throw new ArgumentNullException(nameof(email));
			}

			try
			{
				var request = new ScanRequest
				{
					TableName = _UserTableName,
					ExpressionAttributeValues = new Dictionary<string, AttributeValue>
					{
						{":em", new AttributeValue {S = email}}
					},
					FilterExpression = "email = :em"
				};

				var response = _Client.Scan(request);

				var result = response.Items.FirstOrDefault();

				// if we can't retrieve one of the values, or the email doesn't match
				// what we searched for, throw an error
				if (result == null ||
				    !result.TryGetValue("isadmin", out var userIsAdmin) ||
				    !result.TryGetValue("email", out var dbEmail) ||
				    !result.TryGetValue("name", out var name) ||
				    !email.Equals(dbEmail.S))
				{
					return new StudentUser(null, null);
				}

				if (userIsAdmin.BOOL)
				{
					return new AdminUser(name.S, email);
				}

				// If the student doesn't have an instructor, throw an error
				if (!result.TryGetValue("instructor_id", out var instrid))
				{
					throw new InvalidDatabaseObjectException(email);
				}

				return new StudentUser(name.S, email)
				{
					InstructorId = instrid.S
				};
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return new StudentUser(null, null);
			}
		}

		#endregion

		#region QuestionBanks

		/// <summary>
		/// Inserts a single new questionbank into the database.
		/// </summary>
		/// <param name="newQuestionBank"></param>
		public bool InsertQuestionBank(IQuestionBank newQuestionBank, string instructorEmail)
		{
			if (newQuestionBank == null || string.IsNullOrWhiteSpace(instructorEmail))
			{
				throw new ArgumentNullException();
			}

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
						["unique_id"] = new AttributeValue {S = newQuestionBank.UniqueId},
						["instructor_id"] = new AttributeValue {S = instructorEmail}
					};

				// Create PutItem request
				PutItemRequest request = new PutItemRequest
				{
					TableName = _QuestionBankTableName,
					Item = attributes
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

		/// <summary>
		/// Retrieves all question banks created by the given user.
		/// </summary>
		/// <param name="instructorsEmail"></param>
		/// <returns></returns>
		public List<IQuestionBank> GetQuestionBanksByInstructor(string instructorsEmail)
		{
			if (string.IsNullOrWhiteSpace(instructorsEmail))
			{
				throw new ArgumentNullException(nameof(instructorsEmail));
			}

			try
			{
				var request = new ScanRequest
				{
					TableName = _QuestionBankTableName,
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

					if (didGetQuestions && qs.IsMSet)
					{
						foreach (var keyPair in qs.M)
						{
							keyPair.Value.M.TryGetValue("correct_answer", out var corrAns);
							keyPair.Value.M.TryGetValue("alternate_answers", out var altAns);

							questionList.Add(new Question
							{
								Body = keyPair.Key,
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

		public IQuestionBank GetQuestionBankById(string uniqueId)
		{
			if (string.IsNullOrWhiteSpace(uniqueId))
			{
				throw new ArgumentNullException(nameof(uniqueId));
			}

			try
			{
				var request = new ScanRequest
				{
					TableName = _QuestionBankTableName,
					ExpressionAttributeValues = new Dictionary<string, AttributeValue>
					{
						{":unid", new AttributeValue {S = uniqueId}}
					},
					FilterExpression = "unique_id = :unid"
				};

				var response = _Client.Scan(request);

				var result = response.Items.FirstOrDefault();

				if (result == null || !result.TryGetValue("name", out var name))
				{
					return new QuestionBank(null);
				}

				var questionList = new List<IQuestion>();

				if (result.TryGetValue("questions", out var qs) && qs.IsMSet)
				{
					foreach (var keyPair in qs.M)
					{
						keyPair.Value.M.TryGetValue("correct_answer", out var corrAns);
						keyPair.Value.M.TryGetValue("alternate_answers", out var altAns);

						questionList.Add(new Question
						{
							Body = keyPair.Key,
							AlternateAnswers = altAns?.SS,
							CorrectAnswer = corrAns?.S
						});
					}
				}

				return new QuestionBank(uniqueId)
				{
					Name = name.S,
					Questions = questionList
				};
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return new QuestionBank(null);
			}
		}

		#endregion

		#region GameSessions

		/// <summary>
		/// Inserts a new game session into the database. Throws an exception on error.
		/// </summary>
		/// <param name="newGameSession"></param>
		/// <param name="instructorsEmail"></param>
		public bool InsertGameSession(IGameSession newGameSession, string instructorsEmail)
		{
			if (newGameSession == null || instructorsEmail == null)
			{
				throw new ArgumentNullException();
			}

			try
			{
				// Create a list of student ids
				var studentsEmails = new List<string>();

				foreach (var s in newGameSession.Students)
				{
					studentsEmails.Add(s.Email);
				}

				// Build the attribute dictionary
				var attributes =
					new Dictionary<string, AttributeValue>
					{
						["name"] = new AttributeValue {S = newGameSession.Name},
						["question_bank_id"] = new AttributeValue {S = newGameSession.QuestionBank.UniqueId},
						["unique_id"] = new AttributeValue {S = newGameSession.UniqueId},
						["instructor_id"] = new AttributeValue {S = instructorsEmail},
						["student_ids"] = new AttributeValue {SS = studentsEmails}
					};

				// Create PutItem request
				PutItemRequest request = new PutItemRequest
				{
					TableName = _GameSessionTableName,
					Item = attributes
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

		public List<IGameSession> GetGameSessionsByInstructor(string instructorsEmail)
		{
			if (string.IsNullOrWhiteSpace(instructorsEmail))
			{
				throw new ArgumentNullException(nameof(instructorsEmail));
			}
			try
			{
				var request = new ScanRequest
				{
					TableName = _GameSessionTableName,
					ExpressionAttributeValues = new Dictionary<string, AttributeValue>
					{
						{":instr", new AttributeValue {S = instructorsEmail}}
					},
					FilterExpression = "instructor_id = :instr"
				};

				var resp = _Client.Scan(request);

				var gameSessions = new List<IGameSession>();

				foreach (var item in resp.Items)
				{
					if (item.TryGetValue("name", out var name) &&
					    item.TryGetValue("unique_id", out var unid) &&
					    item.TryGetValue("question_bank_id", out var qbId) &&
					    item.TryGetValue("student_ids", out var studentsEmails))
					{
						// pull all the students from the user table
						var studentList = new List<IUser>();
						foreach (var stid in studentsEmails.SS)
						{
							try
							{
								studentList.Add(GetUserByEmail(stid));
							}
							catch (ItemNotFoundException ex)
							{
								// continue trying to get the students
							}
						}

						//pull the questionbank from the questionbank table
						var questionB = GetQuestionBankById(qbId.S);
						
						gameSessions.Add(new GameSession(unid?.S)
						{
							Name = name?.S,
							Students = studentList,
							QuestionBank = questionB
						});
					}
				}

				return gameSessions;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return new List<IGameSession>();
			}
		}

		public IGameSession GetGameSessionById(string uniqueId)
		{
			if (string.IsNullOrWhiteSpace(uniqueId))
			{
				throw new ArgumentNullException(nameof(uniqueId));
			}

			try
			{
				var request = new ScanRequest
				{
					TableName = _GameSessionTableName,
					ExpressionAttributeValues = new Dictionary<string, AttributeValue>
					{
						{":unid", new AttributeValue {S = uniqueId}}
					},
					FilterExpression = "unique_id = :unid"
				};

				var resp = _Client.Scan(request).Items.FirstOrDefault();

				if (resp == null || 
				    !resp.TryGetValue("name", out var name) || 
				    !resp.TryGetValue("unique_id", out var unid) ||
				    !resp.TryGetValue("question_bank_id", out var qbId) || 
				    !resp.TryGetValue("student_ids", out var studentsEmails))
				{
					return new GameSession(null);
				}

				// pull all the students from the user table
				var studentList = new List<IUser>();
				foreach (var stid in studentsEmails.SS)
				{
					studentList.Add(GetUserByEmail(stid));
				}

				//pull the questionbank from the questionbank table
				var questionB = GetQuestionBankById(qbId.S);

				return new GameSession(unid?.S)
				{
					Name = name?.S,
					Students = studentList,
					QuestionBank = questionB
				};
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);

				return new GameSession(null);
			}
		}

		#endregion


		#region IDisposable Implementation
		public void Dispose()
		{
			_Client?.Dispose();
		}

		#endregion
	}
}
