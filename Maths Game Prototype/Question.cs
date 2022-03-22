using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Maths_Game_Prototype
{
    internal class Question
    {
        public Dictionary<string, dynamic> QuestionVariables { get; set; } //Variables used in question (e.g. x + y = _)
        public Dictionary<string, string> ExpectedAnswer { get; set; } //The expected answer(s) of the question

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="questionVariables"></param>
        /// <param name="expectedAnswer"></param>
        public Question(Dictionary<string, dynamic> questionVariables, Dictionary<string, string> expectedAnswer)
        {
            QuestionVariables = questionVariables;
            ExpectedAnswer = expectedAnswer;
        }

        /// <summary>
        /// Checks whether the user's answer matches the expected answer for the question.
        /// </summary>
        /// <param name="givenAnswer">The user's answer</param>
        /// <returns>True if answers match, False if answer's don't match.</returns>
        public bool IsAnswerCorrect(Dictionary<string, string> givenAnswer)
        {
            return givenAnswer == ExpectedAnswer;
        }

    }
}
