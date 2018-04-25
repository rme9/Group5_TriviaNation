using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TriviaNation.Core.Models;
using TriviaNation.ViewModels;

namespace TriviaNation.Test
{
    [TestClass]
    public class AddQuestionToDatabaseTester
    {
        [TestMethod]
        public void addAltAnswer_StringIsNotNull_AddsAnswerToQuestionObject()
        {
            //Arrange
            AddQuestionToDatabaseViewModel tester = new AddQuestionToDatabaseViewModel();
            string answer = "This string was added to the question object";
            
            //Act
            tester.AddAltAnswer(answer);

            //Assert
            Assert.AreEqual("This string was added to the question object", tester.Question.AlternateAnswers[0]);
        }

        [TestMethod]
        public void addToQuestionBank_questionObjectNotNull_addsQuestionToDatabase()
        {
            //Arrange
            AddQuestionToDatabaseViewModel tester = new AddQuestionToDatabaseViewModel();
            tester.addBody("This is the body of the question object");
            tester.AddAltAnswer("This the correct answer");
            tester.AddAltAnswer("This is the alternate answer");
            tester.AddToQuestionBank(tester.Question);

            //Act
            tester.AddToQuestionBank(tester.Question);

            //Assert
            Assert.AreEqual(tester.Question, tester.QuestionBank.Questions[0]);
        }

       
    }
}
