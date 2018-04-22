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
            AddQuestionToDataseViewModel tester = new AddQuestionToDataseViewModel();
            string answer = "This string was added to the question object";
            
            //Act
            tester.AddAltAnswer(answer);

            //Assert
            Assert.AreEqual("This string was added to the question object", tester.Question.AlternateAnswers[0]);


        }
    }
}
