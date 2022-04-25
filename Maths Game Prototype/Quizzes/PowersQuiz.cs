using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Maths_Game_Prototype.Quizzes
{
    /// <summary>
    /// Parent template class to derived squaring quizzes and cubing quizzes.
    /// </summary>
    internal abstract class PowersQuiz : Quiz
    {
        protected bool IsCube; //Holds whether the quiz is the cubing quiz or not.

        /// <summary>
        /// Constructor
        /// </summary>
        protected PowersQuiz()
        {
            TextInputRestriction = new Regex("^[\\d]{0,4}$"); //Allows a 4 digit positive integer to be added.
            OperatorCategory = Operators.Generic;
        }

        /// <summary>
        /// See parent class definition
        /// Generates a random number between ranges depending on if the current quiz is a cubing quiz or squaring quiz.
        /// </summary>
        public override void NewGame()
        {
            base.NewGame();

            var numberPower = IsCube ? 3 : 2; //If the current quiz is cubing quiz, sets number power to 3, otherwise sets it to 2.

            for (var index = 0; index < Questions.Length; index++)
            {
                var questionVariables = new Dictionary<string, dynamic>();
                var expectedAnswer = new Dictionary<string, string>();

                var numberBase = Randoms.Next(0, IsCube ? 22 : 21); //If quiz is cubing quiz, max base is 21, else it is 20.

                //If quiz is squaring quiz, any generated base over 12 becomes 12 - base multiplied by 10.
                //Ensures only 0-12 can be generated, or multiples of 10 from 10-90 as per KS2 curriculum.
                if (!IsCube)
                {
                    if (numberBase > 12)
                        numberBase = (numberBase - 12) * 10;
                }

                questionVariables["base"] = numberBase;
                expectedAnswer["ans"] = Math.Pow(numberBase, numberPower).ToString();

                Questions[index] = new Question(questionVariables, expectedAnswer);
            }

            MainWindow.PowersExponent.Text = IsCube ? "³" : "²"; //Sets power text to squared or cubed depending whether quiz is squaring or cubing quiz.

            ShowQuizLayout(MainWindow.PowersArea);
            DisplayQuestion();
        }

        /// <summary>
        /// See parent class definition
        /// Displays the base for the user to find the value of sqaured/cubed.
        /// </summary>
        protected override void DisplayQuestion()
        {
            base.DisplayQuestion();

            var currentQuestion = Questions[QuestionNumber];

            MainWindow.PowersBase.Text = currentQuestion.QuestionVariables["base"].ToString();
            MainWindow.PowersTb.Text = string.Empty;
        }

        /// <summary>
        /// See parent class definition
        /// </summary>
        protected override void LockQuestion(bool locked)
        {
            MainWindow.PowersTb.IsEnabled = !locked;

            if (locked)
                MainWindow.NextQBtn.Focus();
            else
                MainWindow.PowersTb.Focus();
        }

        /// <summary>
        /// See parent class definition
        /// </summary>
        public override void CheckAnswer()
        {
            base.CheckAnswer();

            var currentQuestion = Questions[QuestionNumber];

            if (MainWindow.PowersTb.Text == currentQuestion.ExpectedAnswer["ans"])
                RightAnswer();
            else
                WrongAnswer(currentQuestion.ExpectedAnswer["ans"]);
        }
    }
}
