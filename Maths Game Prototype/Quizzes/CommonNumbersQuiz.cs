using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Maths_Game_Prototype.Quizzes
{
    internal class CommonNumbersQuiz : Quiz
    {
        protected bool IsLcmQuiz; //Holds whether the current quiz is a Lowest Common Multiple quiz.

        /// <summary>
        /// Constructor
        /// </summary>
        protected CommonNumbersQuiz()
        {
            PaperTip = true;
        }

        /// <summary>
        /// Returns the lowest common multiple of a and b.
        /// </summary>
        private int CalcLcm(int a, int b)
        {
            var x = a;
            var y = b;

            while (x != y)
            {
                if (x < y)
                    x += a;
                else
                    y += b;
            }

            return x;
        }

        /// <summary>
        /// Returns the greatest common divisor of a and b.
        /// Algorithm from https://stackoverflow.com/a/41766138
        /// </summary>
        private int CalcGcd(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        /// <summary>
        /// See parent class definition
        /// Changes text in UI to say 'lowest common multiple' or 'highest common factor' depending on IsLcmQuiz variable.
        /// For each question, generates two random numbers between 1 and 100 and stores them
        /// Sets the answer to either the GCD or LCM of these numbers depending on IsLcmQuiz variable.
        /// </summary>
        public override void NewGame()
        {
            base.NewGame();

            for (var index = 0; index < Questions.Length; index++)
            {
                var questionVariables = new Dictionary<string, dynamic>();
                var expectedAnswer = new Dictionary<string, string>();

                var a = Randoms.Next(1, 100);
                var b = Randoms.Next(1, 100);

                var ans = IsLcmQuiz ? CalcLcm(a, b) : CalcGcd(a, b);

                questionVariables["a"] = a;
                questionVariables["b"] = b;
                expectedAnswer["ans"] = ans.ToString();

                Questions[index] = new Question(questionVariables, expectedAnswer);
            }

            MainWindow.CommonNumbersLcmHcfText.Text = IsLcmQuiz ? "lowest common multiple" : "highest common factor";
            MainWindow.CommonNumbersTb.Width = IsLcmQuiz ? 192 : 128;
            
            ShowQuizLayout(MainWindow.CommonNumbersArea);
            DisplayQuestion();
        }

        /// <summary>
        /// See parent class definition
        /// Displays the two numbers to be considered.
        /// </summary>
        protected override void DisplayQuestion()
        {
            base.DisplayQuestion();

            var currentQuestion = Questions[QuestionNumber];
            var a = currentQuestion.QuestionVariables["a"];
            var b = currentQuestion.QuestionVariables["b"];

            MainWindow.CommonNumbersATxt.Text = a.ToString();
            MainWindow.CommonNumbersBTxt.Text = b.ToString();

            MainWindow.CommonNumbersTb.Text = string.Empty;
        }

        /// <summary>
        /// See parent class definition
        /// </summary>
        protected override void LockQuestion(bool locked)
        {
            MainWindow.CommonNumbersTb.IsEnabled = !locked;

            if (locked)
                MainWindow.NextQBtn.Focus();
            else
                MainWindow.CommonNumbersTb.Focus();
        }

        /// <summary>
        /// See parent class definition
        /// </summary>
        public override void CheckAnswer()
        {
            base.CheckAnswer();

            var currentQuestion = Questions[QuestionNumber];
            var ans = currentQuestion.ExpectedAnswer["ans"];

            if (MainWindow.CommonNumbersTb.Text == ans)
                RightAnswer();
            else
                WrongAnswer(ans);
        }
    }
}
