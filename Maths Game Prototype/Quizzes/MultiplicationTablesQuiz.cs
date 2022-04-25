using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Maths_Game_Prototype.Quizzes
{
    internal class MultiplicationTablesQuiz : Quiz
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MultiplicationTablesQuiz()
        {
            QuizName = "Multiplication Tables";
            OperatorCategory = Operators.Multiply;
            TextInputRestriction = new Regex("^[\\d]{0,3}$");
        }

        public override void NewGame()
        {
            base.NewGame();

            for (var index = 0; index < Questions.Length; index++)
            {
                var questionVariables = new Dictionary<string, dynamic>();
                var expectedAnswer = new Dictionary<string, string>();

                var x = Randoms.Next(0, 13);
                var y = Randoms.Next(0, 13);

                questionVariables["x"] = x;
                questionVariables["y"] = y;
                expectedAnswer["ans"] = (x * y).ToString();

                Questions[index] = new Question(questionVariables, expectedAnswer);
            }

            MainWindow.MentalMathsOperator.Text = OperatorCategory.Symbol;
            MainWindow.MentalMathsOperator.Foreground = OperatorCategory.Colour;

            MainWindow.MentalMathsAnsTb.Width = 192;

            ShowQuizLayout(MainWindow.MentalMathsGrid);
            DisplayQuestion();
        }

        protected override void DisplayQuestion()
        {
            base.DisplayQuestion();

            var currentQuestion = Questions[QuestionNumber]; 
            
            if (currentQuestion == null) return;

            MainWindow.MentalMathsX.Text = currentQuestion.QuestionVariables["x"].ToString();
            MainWindow.MentalMathsY.Text = currentQuestion.QuestionVariables["y"].ToString();

            MainWindow.MentalMathsAnsTb.Text = string.Empty;

            if (currentQuestion.QuestionVariables["x"] == 2 || currentQuestion.QuestionVariables["y"] == 2)
            {
                MainWindow.MultiplicationHint.Visibility = Visibility.Visible;

                if (currentQuestion.QuestionVariables["y"] == 2)
                    MainWindow.MultiplicationHintNumber.Text = currentQuestion.QuestionVariables["x"].ToString();
                else if (currentQuestion.QuestionVariables["x"] == 2)
                    MainWindow.MultiplicationHintNumber.Text = currentQuestion.QuestionVariables["y"].ToString();
            }
            else
                MainWindow.MultiplicationHint.Visibility = Visibility.Collapsed;
        }

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
                RightAnswer();
            else
                WrongAnswer(currentQuestion.ExpectedAnswer["ans"]);
        }
    }
}
