using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Maths_Game_Prototype.Quizzes
{
    internal abstract class ColumnAddSubQuiz : Quiz
    {
        protected bool IsAddition; //Stores whether the quiz is a column addition quiz or a column subtraction quiz.

        protected ColumnAddSubQuiz()
        {
            PaperTip = true;
            TextInputRestriction = new Regex("(^-[\\d]{0,6}$)|(^[\\d]{0,7}$)"); //Only allows a 7 digit positive integer or 6 digit negative integer
        }

        /// <summary>
        /// See parent class definition
        /// </summary>
        public override void NewGame()
        {
            base.NewGame();

            for (var index = 0; index < Questions.Length; index++)
            {
                var questionVariables = new Dictionary<string, dynamic>();
                var expectedAnswer = new Dictionary<string, string>();

                //Generates two positive 6-digit integers
                var x = Randoms.Next(0, 1000000);
                var y = Randoms.Next(0, 1000000);

                questionVariables.Add("x", x);
                questionVariables.Add("y", y);

                var result = IsAddition ? x + y : x - y; //Adds two numbers together for result if operation is addition, else it subtracts y from x and stores as result.

                expectedAnswer.Add("ans", result.ToString());

                Questions[index] = new Question(questionVariables, expectedAnswer);
            }

            MainWindow.ColumnSubAddOperator.Text = IsAddition ? Operators.Plus.Symbol : Operators.Minus.Symbol; //Sets operator in UI to plus or minus depending on the operation
            MainWindow.ColumnSubAddOperator.Foreground = IsAddition ? Operators.Plus.Colour : Operators.Minus.Colour; //Sets operator colour in UI to plus or minus colour depending on the operation

            ShowQuizLayout(MainWindow.ColumnSubAddArea);
            DisplayQuestion();
        }

        /// <summary>
        /// See parent class definition
        /// </summary>
        protected override void DisplayQuestion()
        {
            base.DisplayQuestion();

            var currentQuestion = Questions[QuestionNumber];

            var numXYToString = new[] //The values of X and Y are stored in index 0 and index 1 of this array respectively
            {
                currentQuestion.QuestionVariables["x"].ToString(),
                currentQuestion.QuestionVariables["y"].ToString()
            };

            //Stores the textboxes for each digit of X and Y
            //The textboxes for digits of X and textboxes for digits of Y are stored in index 0 and index 1 of this array respectively.
            var numXYDigits = new[]
            {
                new[]
                {
                    MainWindow.ColumnSubAddXDigit1,
                    MainWindow.ColumnSubAddXDigit2,
                    MainWindow.ColumnSubAddXDigit3,
                    MainWindow.ColumnSubAddXDigit4,
                    MainWindow.ColumnSubAddXDigit5,
                    MainWindow.ColumnSubAddXDigit6
                },
                new[]
                {
                    MainWindow.ColumnSubAddYDigit1,
                    MainWindow.ColumnSubAddYDigit2,
                    MainWindow.ColumnSubAddYDigit3,
                    MainWindow.ColumnSubAddYDigit4,
                    MainWindow.ColumnSubAddYDigit5,
                    MainWindow.ColumnSubAddYDigit6
                }
            };

            //Fills textboxes representing digits with digits from the questions X and Y variables
            for (var numberIndex = 0; numberIndex < numXYToString.Length; numberIndex++)
            {
                foreach (var digit in numXYDigits[numberIndex]) //Collapses all digit textboxes in case some aren't used
                    digit.Visibility = Visibility.Collapsed;

                var emptyDigits = numXYDigits[numberIndex].Length - numXYToString[numberIndex].Length; //Gets the number of empty digits in the number

                //Shows digit textboxes that are needed to represent the number, and adds text representing each digit to each digit textbox
                for (var digitIndex = numXYDigits[numberIndex].Length - 1; digitIndex >= emptyDigits; digitIndex--)
                {
                    numXYDigits[numberIndex][digitIndex].Visibility = Visibility.Visible;
                    numXYDigits[numberIndex][digitIndex].Text = numXYToString[numberIndex][digitIndex - emptyDigits].ToString();
                }
            }

            MainWindow.ColumnSubAddAnswerTb.Text = string.Empty; //Empties the user input textbox
        }

        /// <summary>
        /// See parent class definition
        /// </summary>
        protected override void LockQuestion(bool locked)
        {
            MainWindow.ColumnSubAddAnswerTb.IsEnabled = !locked;

            if (locked)
                MainWindow.NextQBtn.Focus();
            else
                MainWindow.ColumnSubAddAnswerTb.Focus();
        }

        /// <summary>
        /// See parent class definition
        /// </summary>
        public override void CheckAnswer()
        {
            base.CheckAnswer();

            var currentQuestion = Questions[QuestionNumber];

            if (MainWindow.ColumnSubAddAnswerTb.Text == currentQuestion.ExpectedAnswer["ans"])
                RightAnswer();
            else
                WrongAnswer(currentQuestion.ExpectedAnswer["ans"]);
        }
    }
}
