using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Maths_Game_Prototype.Quizzes
{
    internal class PrimeCompositeNumbersQuiz : Quiz
    {
        private int[] _primes =
        {
            2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97
        };
        public bool PrimeSelected { get; set; }
        
        public PrimeCompositeNumbersQuiz()
        {
            QuizName = "Prime & Composite Numbers";
            OperatorCategory = Operators.Generic;
        }

        public override void NewGame()
        {
            base.NewGame();

            for (var index = 0; index < Questions.Length; index++)
            {
                var questionVariables = new Dictionary<string, dynamic>();
                var expectedAnswer = new Dictionary<string, string>();

                var num = Randoms.Next(1, 100);
                var isPrime = _primes.Contains(num).ToString();

                questionVariables["num"] = num;
                expectedAnswer["ans"] = isPrime;

                Questions[index] = new Question(questionVariables, expectedAnswer);
            }

            ShowQuizLayout(MainWindow.PrimesArea);
            DisplayQuestion();
        }

        protected override void DisplayQuestion()
        {
            base.DisplayQuestion();

            var currentQuestion = Questions[QuestionNumber];

            MainWindow.PrimesNumberTxt.Text = currentQuestion.QuestionVariables["num"].ToString();

            MainWindow.CheckAnsBtn.Visibility = Visibility.Collapsed;
        }

        protected override void LockQuestion(bool locked)
        {
            MainWindow.PrimeBtn.IsEnabled = !locked;
            MainWindow.CompositeBtn.IsEnabled = !locked;

            if (locked)
                MainWindow.NextQBtn.Focus();
        }

        public override void CheckAnswer()
        {
            base.CheckAnswer();

            var currentQuestion = Questions[QuestionNumber];

            if (PrimeSelected.ToString() == currentQuestion.ExpectedAnswer["ans"])
                RightAnswer();
            else
                WrongAnswer(currentQuestion.ExpectedAnswer["ans"] == "True" ? "prime" : "composite");
        }
    }
}
