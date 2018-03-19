using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivia_Nation
{
    class Question
    {
        ObjectId ID { set; get; }
        string body { set; get; }
        string answer { set; get; }


        public Question()
        {
            
            this.body = "";
            this.body = "";

        }
        public Question(int id, string body, string answer)
        {
            this.body = body;
            this.answer = answer;
        }
        






    }
}
