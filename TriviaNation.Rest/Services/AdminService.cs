using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2.Model;
using TriviaNation.Core.Models;
using TriviaNation.Drivers;

namespace TriviaNation.Rest.Services
{
	public class AdminService : IDisposable
	{
		private DynamoDBDriver _Driver;
		
		#region Table Name Strings

		private readonly string _UserTableName = "se2_user";

		private readonly string _QuestionBankTableName = "se2_questionbank";

		private readonly string _GameSessionTableName = "se2_gamesession";

		#endregion

		public AdminService()
		{
			_Driver = new DynamoDBDriver();
		}

		public bool InsertUser(IUser user)
		{
			if (user == null)
			{
				return false;
			}
			
			// Build the attribute dictionary
			var attributes =
				new Dictionary<string, AttributeValue>
				{
					["email"] = new AttributeValue { S = user.Email },
					["name"] = new AttributeValue { S = user.Name }
				};

			if (user is StudentUser suser)
			{
				attributes.Add("isadmin", new AttributeValue { BOOL = false });
				attributes.Add("instructor_id",
					new AttributeValue { S = suser.InstructorId });
			}
			else if (user is AdminUser)
			{
				attributes.Add("isadmin", new AttributeValue { BOOL = true });
			}

			return _Driver.Insert(_UserTableName, attributes).Result;
		}

		public bool InsertQuestionBank(IQuestionBank newQuestionBank, string instructorEmail)
		{
			if (newQuestionBank == null || string.IsNullOrWhiteSpace(instructorEmail))
			{
				throw new ArgumentNullException();
			}

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

			return _Driver.Insert(_QuestionBankTableName, attributes).Result;
		}

		public bool InsertGameSession(IGameSession newGameSession, string instructorsEmail)
		{
			if (newGameSession == null || instructorsEmail == null)
			{
				return false;
			}


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

			return _Driver.Insert(_GameSessionTableName, attributes).Result;
		}

		public bool DeleteUser(IUser userId)
		{
			var attributes =
				new Dictionary<string, AttributeValue>()
				{
					{"email", new AttributeValue {S = userId.Email}},
					{"name",  new AttributeValue{S = userId.Name}}
				};

			return _Driver.Delete(_UserTableName, attributes).Result;
		}

		public List<StudentUser> GetAllUsersByInstructor(string instructorsEmail)
		{
			var searchValues = new Dictionary<string, AttributeValue>
			{
				{":instr", new AttributeValue {S = instructorsEmail}}
			};

			var filter =  "instructor_id = :instr";

			var resp = _Driver.Scan(_UserTableName, searchValues, filter).Result;

			var users = new List<StudentUser>();

			foreach (var item in resp)
			{
				AttributeValue name;
				AttributeValue email;
				item.TryGetValue("name", out name);
				item.TryGetValue("email", out email);

				users.Add(new StudentUser(name?.S, email?.S)
				{
					InstructorId = instructorsEmail
				});
			}

			return users;
		}

		public List<QuestionBank> GetQuestionBanksByInstructor(string instructorsEmail)
		{
			if (string.IsNullOrWhiteSpace(instructorsEmail))
			{
				return null;
			}

			var filter = "instructor_id = :instr";

			var searchValues = new Dictionary<string, AttributeValue>
			{
				{":instr", new AttributeValue {S = instructorsEmail}}
			};

			var resp = _Driver.Scan(_QuestionBankTableName, searchValues, filter).Result;

			var questionbanks = new List<QuestionBank>();

			#region out variables

			AttributeValue name;
			AttributeValue unid;
			AttributeValue qs;
			AttributeValue corrAns;
			AttributeValue altAns;

			#endregion

			foreach (var item in resp)
			{
				item.TryGetValue("name", out name);
				item.TryGetValue("unique_id", out unid);

				var didGetQuestions = item.TryGetValue("questions", out qs);

				var questionList = new List<Question>();

				if (didGetQuestions && qs.IsMSet)
				{
					foreach (var keyPair in qs.M)
					{
						keyPair.Value.M.TryGetValue("correct_answer", out corrAns);
						keyPair.Value.M.TryGetValue("alternate_answers", out altAns);

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

		public List<GameSession> GetGameSessionsByInstructor(string instructorsEmail)
		{
			if (string.IsNullOrWhiteSpace(instructorsEmail))
			{
				return new List<GameSession>();
			}

			var filter = "instructor_id = :instr";

			var searchValues = new Dictionary<string, AttributeValue>
			{
				{":instr", new AttributeValue {S = instructorsEmail}}
			};

			var resp = _Driver.Scan(_GameSessionTableName, searchValues, filter).Result;

			var gameSessions = new List<GameSession>();

			#region out variables

			AttributeValue name;
			AttributeValue unid;
			AttributeValue qbId;
			AttributeValue studentsEmails;

			#endregion

			foreach (var item in resp)
			{
				if (item.TryGetValue("name", out name) &&
				    item.TryGetValue("unique_id", out unid) &&
				    item.TryGetValue("question_bank_id", out qbId) &&
				    item.TryGetValue("student_ids", out studentsEmails))
				{
					// pull all the students from the user table
					var studentList = new List<StudentUser>();
					foreach (var stid in studentsEmails.SS)
					{
						try
						{
							IUser user = GetUserByEmail(stid);
							studentList.Add(user as StudentUser);
						}
						catch (Exception ex)
						{
							// continue trying to get the students
						}
					}

					//pull the questionbank from the questionbank table
					QuestionBank questionB = GetQuestionBankById(qbId.S);

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

		public IUser GetUserByEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				return new StudentUser("", "");
			}

			var filter = "email = :em";

			var searchValues = new Dictionary<string, AttributeValue>
			{
				{":em", new AttributeValue {S = email}}
			};

			var response = _Driver.Scan(_UserTableName, searchValues, filter).Result;

			var result = response.FirstOrDefault();

			AttributeValue userIsAdmin;
			AttributeValue dbEmail;
			AttributeValue name;
			AttributeValue password;

			// if we can't retrieve one of the values, or the email doesn't match
			// what we searched for, throw an error
			if (result == null ||
			    !result.TryGetValue("isadmin", out userIsAdmin) ||
			    !result.TryGetValue("email", out dbEmail) ||
			    !result.TryGetValue("name", out name) ||
				!result.TryGetValue("password", out password) ||
			    !email.Equals(dbEmail.S))
			{
				return new StudentUser(null, null);
			}

			if (userIsAdmin.BOOL)
			{
				return new AdminUser(name.S, email) {Password = password.S};
			}

			AttributeValue instrid;

			// If the student doesn't have an instructor, throw an error
			if (!result.TryGetValue("instructor_id", out instrid))
			{
				return new StudentUser("", "");
			}

			return new StudentUser(name.S, email)
			{
				InstructorId = instrid.S,
				Password = password.S
			};
		}

		public QuestionBank GetQuestionBankById(string uniqueId)
		{
			if (string.IsNullOrWhiteSpace(uniqueId))
			{
				throw new ArgumentNullException(nameof(uniqueId));
			}

			var filter = "unique_id = :uid";

			var searchValues = new Dictionary<string, AttributeValue>
			{
				{":uid", new AttributeValue {S = uniqueId}}
			};

			var response = _Driver.Scan(_QuestionBankTableName, searchValues, filter).Result;

			var result = response.FirstOrDefault();

			#region out variables

			AttributeValue name;
			AttributeValue qs;
			AttributeValue corrAns;
			AttributeValue altAns;

			#endregion

			if (result == null || !result.TryGetValue("name", out name))
			{
				return new QuestionBank(null);
			}

			var questionList = new List<Question>();

			if (result.TryGetValue("questions", out qs) && qs.IsMSet)
			{
				foreach (var keyPair in qs.M)
				{
					keyPair.Value.M.TryGetValue("correct_answer", out corrAns);
					keyPair.Value.M.TryGetValue("alternate_answers", out altAns);

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

		public IGameSession GetGameSessionById(string uniqueId)
		{
			if (string.IsNullOrWhiteSpace(uniqueId))
			{
				throw new ArgumentNullException(nameof(uniqueId));
			}

			var filter = "unique_id = :uid";

			var searchValues = new Dictionary<string, AttributeValue>
			{
				{":uid", new AttributeValue {S = uniqueId}}
			};

			var resp = _Driver.Scan(_GameSessionTableName, searchValues, filter).Result.FirstOrDefault();

			#region

			AttributeValue name;
			AttributeValue unid;
			AttributeValue qbId;
			AttributeValue studentsEmails;

			#endregion

			if (resp == null ||
			    !resp.TryGetValue("name", out name) ||
			    !resp.TryGetValue("unique_id", out unid) ||
			    !resp.TryGetValue("question_bank_id", out qbId) ||
			    !resp.TryGetValue("student_ids", out studentsEmails))
			{
				return new GameSession(null);
			}

			// pull all the students from the user table
			var studentList = new List<StudentUser>();
			foreach (var stid in studentsEmails.SS)
			{
				try
				{
					var suser = GetUserByEmail(stid) as StudentUser;
					studentList.Add(suser);
				}
				catch (Exception ex)
				{
					// ignore the exception, continue retrieving students
				}
			}

			//pull the questionbank from the questionbank table
			QuestionBank questionB = GetQuestionBankById(qbId.S);

			return new GameSession(unid?.S)
			{
				Name = name?.S,
				Students = studentList,
				QuestionBank = questionB
			};
		}

		public void Dispose()
		{
			_Driver.Dispose();
		}
	}
}