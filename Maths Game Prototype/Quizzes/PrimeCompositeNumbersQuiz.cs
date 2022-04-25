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
        }; //Holds all prime numbers between 1 and 100
        public bool PrimeSelected { get; set; } //True if the user has selected the 'prime' option, False if user has selected 'composite'.
        
        /// <summary>
        /// Constructor
        /// </summary>
        public PrimeCompositeNumbersQuiz()
        {
            QuizName = "Prime & Composite Numbers";
            OperatorCategory = Operators.Generic;
        }

        /// <summary>
        /// See parent class definition
        /// For each question, generates a random number between 1 and 100.
        /// Determines if this number is prime, and stores this result as the answer.
        /// </summary>
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

        /// <summary>
        /// See parent class definition
        /// Displays the number to evaluate.
        /// Hides the 'check answer' button since it isn't used in this quiz.
        /// </summary>
        protected override void DisplayQuestion()
        {
            base.DisplayQuestion();

            var currentQuestion = Questions[QuestionNumber];

            MainWindow.PrimesNumberTxt.Text = currentQuestion.QuestionVariables["num"].ToString();

            MainWindow.CheckAnsBtn.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// See parent class definition
        /// </summary>
        protected override void LockQuestion(bool locked)
        {
            MainWindow.PrimeBtn.IsEnabled = !locked;
            MainWindow.CompositeBtn.IsEnabled = !locked;

            if (locked)
                MainWindow.NextQBtn.Focus();
        }

        /// <summary>
        /// See parent class definition
        /// </summary>
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
