using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Maths_Game_Prototype.Quizzes
{
    internal class AlgebraQuiz : Quiz
    {
        public AlgebraQuiz()
        {
            QuizName = "Algebra: Missing Numbers";
            OperatorCategory = Operators.Generic;
            PaperTip = true;
            TextInputRestriction = new Regex("^[\\d]{0,2}$");
        }

        public override void NewGame()
        {
            base.NewGame();

            for (var index = 0; index < Questions.Length; index++)
            {
                var questionVariables = new Dictionary<string, dynamic>();
                var expectedAnswer = new Dictionary<string, string>();

                var x = Randoms.Next(1, 20);
                var y = Randoms.Next(1, 20);

                var ans = 0;

                Operator operation = null;
                var operationNumber = Randoms.Next(3);

                switch (operationNumber)
                {
                    case 0:
                        operation = Operators.Plus;
                        ans = x + y;
                        break;
                    case 1:
                        operation = Operators.Minus;
                        ans = x - y;
                        break;
                    case 2:
                        operation = Operators.Multiply;
                        ans = x * y;
                        break;
                }

                var leftOperandAnswerable = Randoms.NextDouble() >= 0.5;

                questionVariables["x"] = x;
                questionVariables["operation"] = operation;
                questionVariables["y"] = y;
                questionVariables["ans"] = ans;
                questionVariables["leftOperandAnswerable"] = leftOperandAnswerable;

                expectedAnswer["ans"] = leftOperandAnswerable ? x.ToString() : y.ToString();

                Questions[index] = new Question(questionVariables, expectedAnswer);
            }

            ShowQuizLayout(MainWindow.AlgebraArea);
            DisplayQuestion();
        }

        protected override void DisplayQuestion()
        {
            base.DisplayQuestion();

            var currentQuestion = Questions[QuestionNumber];
            var currentOperator = currentQuestion.QuestionVariables["operation"];

            if (currentQuestion.QuestionVariables["leftOperandAnswerable"])
            {
                MainWindow.AlgebraXTb.Visibility = Visibility.Visible;
                MainWindow.AlgebraXTb.Text = string.Empty;
                MainWindow.AlgebraXTxt.Visibility = Visibility.Collapsed;

                MainWindow.AlgebraYTb.Visibility = Visibility.Collapsed;
                MainWindow.AlgebraYTxt.Visibility = Visibility.Visible;
                MainWindow.AlgebraYTxt.Text = currentQuestion.QuestionVariables["y"].ToString();
            }
            else
            {
                MainWindow.AlgebraXTb.Visibility = Visibility.Collapsed;
                MainWindow.AlgebraXTxt.Visibility = Visibility.Visible;
                MainWindow.AlgebraXTxt.Text = currentQuestion.QuestionVariables["x"].ToString();

                MainWindow.AlgebraYTb.Visibility = Visibility.Visible;
                MainWindow.AlgebraYTb.Text = string.Empty;
                MainWindow.AlgebraYTxt.Visibility = Visibility.Collapsed;
            }

            MainWindow.AlgebraOperator.Text = currentOperator.Symbol;
            MainWindow.AlgebraOperator.Foreground = currentOperator.Colour;

            MainWindow.AlgebraAnsTxt.Text = currentQuestion.QuestionVariables["ans"].ToString();
            LockQuestion(false);
        }

        protected override void LockQuestion(bool locked)
        {
            var currentQuestion = Questions[QuestionNumber];
            var tbToLock = currentQuestion.QuestionVariables["leftOperandAnswerable"]
                ? MainWindow.AlgebraXTb
                : MainWindow.AlgebraYTb;

            tbToLock.IsEnabled = !locked;

            if (locked)
                MainWindow.NextQBtn.Focus();
            else
                tbToLock.Focus();
        }

        public override void CheckAnswer()
        {
            base.CheckAnswer();

            var currentQuestion = Questions[QuestionNumber];

            var tbToCheck = currentQuestion.QuestionVariables["leftOperandAnswerable"]
                ? MainWindow.AlgebraXTb
                : MainWindow.AlgebraYTb;

            if (tbToCheck.Text == currentQuestion.ExpectedAnswer["ans"])
                RightAnswer();
            else
                WrongAnswer(currentQuestion.ExpectedAnswer["ans"]);
        }
    }
}
