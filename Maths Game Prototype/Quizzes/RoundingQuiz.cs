using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Maths_Game_Prototype.Quizzes
{
    internal class RoundingQuiz : Quiz
    {
        protected bool IntegerRounding = true;

        public RoundingQuiz()
        {
            QuizName = "Rounding";
            OperatorCategory = Operators.Generic;
            TextInputRestriction = new Regex("^[\\d]{0,4}$"); //Allows a positive 4 digit integer to be typed
        }

        public override void NewGame()
        {
            base.NewGame();

            for (var index = 0; index < Questions.Length; index++)
            {
                var questionVariables = new Dictionary<string, dynamic>();
                var expectedAnswer = new Dictionary<string, string>();

                double unroundedNumber = Randoms.Next(0, 9500); //Unrounded number can be between 0 and 9500
                double roundedNumber; //For the rounded number

                if (IntegerRounding) //If playing rounding quiz, and NOT decimal rounding quiz
                {
                    var roundBy = Math.Pow(10, Randoms.Next(1, 4)); //Randomly generates either 10, 100, or 1000
                    roundedNumber = Math.Round(unroundedNumber / roundBy, MidpointRounding.AwayFromZero) * roundBy; //Rounds the unrounded number by the number to round by
                    questionVariables.Add("roundBy", roundBy);
                }
                else
                {
                    unroundedNumber /= 10; //Divides the unrounded number by 10, adding a digit after the decimal place.
                    roundedNumber = Math.Round(unroundedNumber, MidpointRounding.AwayFromZero); //Rounds the unrounded number to nearest integer
                }

                questionVariables.Add("unroundedNum", unroundedNumber);
                expectedAnswer.Add("ans", roundedNumber.ToString());

                Questions[index] = new Question(questionVariables, expectedAnswer);
            }

            if (IntegerRounding)
                MainWindow.WholeNumberTxt.Text = string.Empty;
            else
            {
                MainWindow.WholeNumberTxt.Text = "whole number";
                MainWindow.RoundToNumTxt.Text = string.Empty;
            }

            ShowQuizLayout(MainWindow.RoundingArea);
            DisplayQuestion();
        }

        protected override void DisplayQuestion()
        {
            base.DisplayQuestion();

            var currentQuestion = Questions[QuestionNumber];

            MainWindow.RoundingInputTb.Text = string.Empty;
            MainWindow.UnroundedNumTxt.Text = currentQuestion.QuestionVariables["unroundedNum"].ToString();

            if (IntegerRounding)
                MainWindow.RoundToNumTxt.Text = currentQuestion.QuestionVariables["roundBy"].ToString();
        }

        protected override void LockQuestion(bool locked)
        {
            MainWindow.RoundingInputTb.IsEnabled = !locked;

            if (locked)
                MainWindow.NextQBtn.Focus();
            else
                MainWindow.RoundingInputTb.Focus();
        }

        public override void CheckAnswer()
        {
            base.CheckAnswer();

            var currentQuestion = Questions[QuestionNumber];

            if (MainWindow.RoundingInputTb.Text == currentQuestion.ExpectedAnswer["ans"])
                RightAnswer();
            else
                WrongAnswer(currentQuestion.ExpectedAnswer["ans"]);
        }
    }
}
