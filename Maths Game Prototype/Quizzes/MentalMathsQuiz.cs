using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Maths_Game_Prototype
{
    internal class MentalMathsQuiz : Quiz
    {
        /// <summary>
        /// Constructor
        /// Sets quiz name to 'Mental Maths' and the operator category to generic.
        /// </summary>
        public MentalMathsQuiz()
        {
            QuizName = "Mental Maths";
            OperatorCategory = Operators.Generic;
        }

        /// <summary>
        /// Generates random questions of 'X [+/-] Y = _' and adds them to the quiz's questions.
        /// Displays the current quiz's layout and the first question.
        /// </summary>
        public override void NewGame()
        {
            base.NewGame();

            for (var index = 0; index < Questions.Length; index++)
            {
                var questionVariables = new Dictionary<string, dynamic>();
                var expectedAnswer = new Dictionary<string, string>();

                var x = Randoms.Next(0, 10);
                var y = Randoms.Next(0, 10);

                questionVariables.Add("x", x);
                questionVariables.Add("y", y);

                var isAddition = Randoms.NextDouble() >= 0.5;

                var chosenOperator = isAddition ? Operators.Plus : Operators.Minus;
                var result = isAddition ? x + y : x - y;

                questionVariables.Add("operator", chosenOperator);
                expectedAnswer.Add("ans", result.ToString());

                Questions[index] = new Question(questionVariables, expectedAnswer);
            }

            MainWindow.MentalMathsAnsTb.PreviewTextInput -= MainWindow.ThreeDigitPosIntegerTb_OnPreviewTextInput;
            MainWindow.MentalMathsAnsTb.PreviewTextInput += MainWindow.TwoCharIntegerTb_OnPreviewTextInput;
            MainWindow.MentalMathsAnsTb.Width = 128;

            MainWindow.MultiplicationHint.Visibility = Visibility.Collapsed;
            ShowQuizLayout(MainWindow.MentalMathsGrid);
            DisplayQuestion();
        }

        /// <summary>
        /// Displays the mental maths X, operator, and Y variables' text and colour.
        /// </summary>
        protected override void DisplayQuestion()
        {
            base.DisplayQuestion();

            var currentQuestion = Questions[QuestionNumber];

            if (currentQuestion == null) return;

            MainWindow.MentalMathsAnsTb.Text = string.Empty;
            MainWindow.MentalMathsX.Text = currentQuestion.QuestionVariables["x"].ToString();
            MainWindow.MentalMathsOperator.Text = currentQuestion.QuestionVariables["operator"].Symbol;
            MainWindow.MentalMathsOperator.Foreground = currentQuestion.QuestionVariables["operator"].Colour;
            MainWindow.MentalMathsY.Text = currentQuestion.QuestionVariables["y"].ToString();
        }

        /// <summary>
        /// Enables/disables the mental maths answer textbox.
        /// </summary>
        /// <param name="locked">False to enable, True to disable.</param>
        protected override void LockQuestion(bool locked)
        {
            MainWindow.MentalMathsAnsTb.IsEnabled = !locked;

            if (locked)
                MainWindow.NextQBtn.Focus();
            else
                MainWindow.MentalMathsAnsTb.Focus();
        }

        public override void CheckAnswer()
        {
            base.CheckAnswer();

            var currentQuestion = Questions[QuestionNumber];

            if (MainWindow.MentalMathsAnsTb.Text == currentQuestion.ExpectedAnswer["ans"])
            {
                RightAnswer();
            }
            else
            {
                WrongAnswer(currentQuestion.ExpectedAnswer["ans"]);
            }
        }
    }
}
