using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Maths_Game_Prototype.Quizzes
{
    internal abstract class ColumnAddSubQuiz : Quiz
    {
        protected bool IsAddition;

        protected ColumnAddSubQuiz()
        {
            PaperTip = true;
            TextInputRestriction = new Regex("(^-[\\d]{0,2}$)|(^[\\d]{0,3}$)");
        }

        public override void NewGame()
        {
            base.NewGame();

            for (var index = 0; index < Questions.Length; index++)
            {
                var questionVariables = new Dictionary<string, dynamic>();
                var expectedAnswer = new Dictionary<string, string>();

                var x = Randoms.Next(0, 100);
                var y = Randoms.Next(0, 100);

                questionVariables.Add("x", x);
                questionVariables.Add("y", y);

                var result = IsAddition ? x + y : x - y;

                expectedAnswer.Add("ans", result.ToString());

                Questions[index] = new Question(questionVariables, expectedAnswer);
            }

            MainWindow.ColumnSubAddOperator.Text = IsAddition ? Operators.Plus.Symbol : Operators.Minus.Symbol;
            MainWindow.ColumnSubAddOperator.Foreground = IsAddition ? Operators.Plus.Colour : Operators.Minus.Colour;

            ShowQuizLayout(MainWindow.ColumnSubAddArea);
            DisplayQuestion();
        }

        protected override void DisplayQuestion()
        {
            base.DisplayQuestion();

            var currentQuestion = Questions[QuestionNumber];

            var numXToString = currentQuestion.QuestionVariables["x"].ToString();
            var numYToString = currentQuestion.QuestionVariables["y"].ToString();

            if (numXToString.Length == 1)
            {
                MainWindow.ColumnSubAddXDigit1.Text = string.Empty;
                MainWindow.ColumnSubAddXDigit2.Text = numXToString[0].ToString();
            }
            else
            {
                MainWindow.ColumnSubAddXDigit1.Text = numXToString[0].ToString();
                MainWindow.ColumnSubAddXDigit2.Text = numXToString[1].ToString();
            }

            if (numYToString.Length == 1)
            {
                MainWindow.ColumnSubAddYDigit1.Text = string.Empty;
                MainWindow.ColumnSubAddYDigit2.Text = numYToString[0].ToString();
            }
            else
            {
                MainWindow.ColumnSubAddYDigit1.Text = numYToString[0].ToString();
                MainWindow.ColumnSubAddYDigit2.Text = numYToString[1].ToString();
            }

            MainWindow.ColumnSubAddAnswerTb.Text = string.Empty;
        }

        protected override void LockQuestion(bool locked)
        {
            MainWindow.ColumnSubAddAnswerTb.IsEnabled = !locked;

            if (locked)
                MainWindow.NextQBtn.Focus();
            else
                MainWindow.ColumnSubAddAnswerTb.Focus();
        }

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
