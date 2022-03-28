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
        public MentalMathsQuiz()
        {
            QuizName = "Mental Maths";
            OperatorCategory = Operators.Generic;
        }

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

            ShowQuizLayout(MainWindow.MentalMathsGrid);
            DisplayQuestion();
        }

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
