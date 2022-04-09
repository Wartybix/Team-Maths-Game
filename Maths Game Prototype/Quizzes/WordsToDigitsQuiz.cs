using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Maths_Game_Prototype.Quizzes
{
    internal class WordsToDigitsQuiz : Quiz
    {
        public WordsToDigitsQuiz()
        {
            QuizName = "Words to Digits";
            OperatorCategory = Operators.Generic;
            TextInputRestriction = new Regex("^-?[\\d]{0,4}$");
        }

        private static string AbsNumberToWords(int number)
        {
            const int maxNumberCharLength = 4;
            var textOutput = new List<string>();

            #region Makes number absolute

            if (number < 0)
                number *= -1;

            #endregion

            #region Converting number into 4 digit int array

            var numberToString = number.ToString();
            var splitNum = new int[maxNumberCharLength];

            var splitNumberIndex = maxNumberCharLength - numberToString.Length;

            foreach (var character in numberToString)
            {
                splitNum[splitNumberIndex] = character - '0';
                splitNumberIndex++;
            }

            #endregion

            #region Declaring number words

            var digitWords = new[] {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};
            var tenWords = new[] {"ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"};
            var teenWords = new[]
                {"eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"};

            #endregion

            if (splitNum[0] != 0)
            {
                textOutput.Add(digitWords[splitNum[0]] + " thousand");
                if (splitNum[1] != 0 || splitNum[2] != 0 || splitNum[3] != 0)
                {
                    if (splitNum[1] == 0 && (splitNum[2] != 0 || splitNum[3] != 0))
                        textOutput.Add("and");
                }
            }

            if (splitNum[1] != 0)
            {
                textOutput.Add(digitWords[splitNum[1]] + " hundred");

                if (splitNum[2] != 0 || splitNum[3] != 0)
                    textOutput.Add("and");
            }

            if (splitNum[2] == 1 && splitNum[3] != 0)
                textOutput.Add(teenWords[splitNum[3] - 1]);
            else
            {
                if (splitNum[2] != 0)
                    textOutput.Add(tenWords[splitNum[2] - 1]);
                if (splitNum[3] != 0)
                    textOutput.Add(digitWords[splitNum[3]]);
                if (splitNum[3] == 0 && !textOutput.Any())
                    textOutput.Add("zero");
            }

            return string.Join(" ", textOutput);
        }

        public override void NewGame()
        {
            base.NewGame();

            for (var index = 0; index < Questions.Length; index++)
            {
                var questionVariables = new Dictionary<string, dynamic>();
                var expectedAnswer = new Dictionary<string, string>();

                var generatedNumber = Randoms.Next(-9999, 10000);

                questionVariables.Add("numInWords", AbsNumberToWords(generatedNumber));
                questionVariables.Add("isMinus", generatedNumber < 0);
                expectedAnswer.Add("ans", generatedNumber.ToString());

                Questions[index] = new Question(questionVariables, expectedAnswer);
            }

            ShowQuizLayout(MainWindow.WtdArea);
            DisplayQuestion();
        }

        protected override void DisplayQuestion()
        {
            base.DisplayQuestion();

            var currentQuestion = Questions[QuestionNumber];

            if (currentQuestion == null) return;

            MainWindow.WtdTb.Text = string.Empty;
            MainWindow.WtdMinusText.Text = currentQuestion.QuestionVariables["isMinus"] ? " minus" : string.Empty;
            MainWindow.WtdWordedNumberTxt.Text = currentQuestion.QuestionVariables["numInWords"];
        }

        protected override void LockQuestion(bool locked)
        {
            MainWindow.WtdTb.IsEnabled = !locked;

            if (locked)
                MainWindow.NextQBtn.Focus();
            else
                MainWindow.WtdTb.Focus();
        }

        public override void CheckAnswer()
        {
            base.CheckAnswer();

            var currentQuestion = Questions[QuestionNumber];

            if (MainWindow.WtdTb.Text == currentQuestion.ExpectedAnswer["ans"])
                RightAnswer();
            else
                WrongAnswer(currentQuestion.ExpectedAnswer["ans"]);
        }
    }
}
