using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Maths_Game_Prototype.Quizzes
{
    internal class BusStopDivisionQuiz : Quiz
    {
        private TextBlock[] _dividendTbs; //Holds all textboxes of the dividend on screen.

        public BusStopDivisionQuiz()
        {
            QuizName = "Bus Stop Method";
            OperatorCategory = Operators.Divide;
            PaperTip = true;
        }

        /// <summary>
        /// Checks if the number of digits after the decimal point is greater than 3.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsDecimalPlacesOver3(float value)
        {
            value -= (int) value;
            var decimalPlaces = 0;

            while (value > 0)
            {
                decimalPlaces++;
                value *= 10;
                value -= (int) value;

                if (decimalPlaces >= 3)
                    return true;
            }

            return false;
        }

        public override void NewGame()
        {
            base.NewGame();

            for (var index = 0; index < Questions.Length; index++)
            {
                var questionVariables = new Dictionary<string, dynamic>();
                var expectedAnswer = new Dictionary<string, string>();

                float dividend = Randoms.Next(0, 9999);
                float divisor = Randoms.Next(1, 21);

                while (IsDecimalPlacesOver3(dividend/divisor)) //Reduces divisor by one if answer has more than three digits after decimal place.
                    divisor--;

                questionVariables["dividend"] = dividend;
                questionVariables["divisor"] = divisor;
                expectedAnswer["ans"] = (dividend / divisor).ToString();

                Questions[index] = new Question(questionVariables, expectedAnswer);
            }

            _dividendTbs = new[]
            {
                MainWindow.BusStopDividendDigit1,
                MainWindow.BusStopDividendDigit2,
                MainWindow.BusStopDividendDigit3,
                MainWindow.BusStopDividendDigit4
            };

            ShowQuizLayout(MainWindow.BusStopArea);
            DisplayQuestion();
        }

        protected override void DisplayQuestion()
        {
            base.DisplayQuestion();

            var currentQuestion = Questions[QuestionNumber];
            var dividend = currentQuestion.QuestionVariables["dividend"];
            var divisor = currentQuestion.QuestionVariables["divisor"];

            MainWindow.BusStopUpperDividend.Text = dividend.ToString();
            MainWindow.BusStopUpperDivisor.Text = divisor.ToString();

            MainWindow.BusStopDivisor.Text = divisor.ToString();

            foreach (var textBlock in _dividendTbs)
                textBlock.Visibility = Visibility.Collapsed;

            var strDividend = dividend.ToString();
            for (var index = strDividend.Length - 1; index >= 0; index--)
            {
                _dividendTbs[index].Visibility = Visibility.Visible;
                _dividendTbs[index].Text = strDividend[index].ToString();
            }

            MainWindow.BusStopAnsTb.Text = string.Empty;
        }

        protected override void LockQuestion(bool locked)
        {
            MainWindow.BusStopAnsTb.IsEnabled = !locked;

            if (locked)
                MainWindow.NextQBtn.Focus();
            else
                MainWindow.BusStopAnsTb.Focus();
        }

        public override void CheckAnswer()
        {
            base.CheckAnswer();

            var currentQuestion = Questions[QuestionNumber];

            if (MainWindow.BusStopAnsTb.Text == currentQuestion.ExpectedAnswer["ans"])
                RightAnswer();
            else
                WrongAnswer(currentQuestion.ExpectedAnswer["ans"]);
        }
    }
}
