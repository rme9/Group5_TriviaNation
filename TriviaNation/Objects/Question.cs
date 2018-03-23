using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaNation.Models;

namespace Trivia_Nation
{
    class Question : IQuestion
    {
        ObjectId ID { set; get; }

       
        public string Body { set; get; }
        public string CorrectAnswer { get; set; }
        public List<string> AlternateAnswers { get; set; }
        


        public Question()
        {
            
            this.Body = "";
            this.CorrectAnswer = "";
            this.AlternateAnswers.Add("");

        }
        public Question(int id, string body, string answer)
        {
            this.Body = body;
            this.CorrectAnswer = answer;
        }

        public void addAltAnswer(string answer)
        {
            AlternateAnswers.Add(answer);
        }

        public void removeAnswer(int index)
        {
            AlternateAnswers.RemoveAt(index);
        }




        
       
    }
}
