using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Maths_Game_Prototype
{
    internal abstract class Quiz
    {
        public string QuizName { get; set; } //Name of the quiz
        public Question[] Questions = new Question[5]; //Array of questions associated with the quiz
        private int QuestionNumber { get; set; } //The index of the current question.
        protected Random Randoms; //A variable that produces random values for use in child classes.
        protected MainWindow MainWindow; //References the MainWindow.


        /// <summary>
        /// Adds questions to the quiz (in derived classes)
        /// Sets up UI for the quiz (in derived classes)
        /// Sets up new Random variable to set random variables in questions.
        /// </summary>
        public virtual void NewGame()
        {
            QuestionNumber = 0;

            MainWindow = (MainWindow) Application.Current.MainWindow;
            Randoms = new Random();

            if (MainWindow != null) MainWindow.QuizNameTxt.Text = QuizName;
        }

        /// <summary>
        /// Returns whether the current question is the last index in the questions array.
        /// </summary>
        /// <returns>True if last question, False if not.</returns>
        public bool EndOfQuiz()
        {
            return QuestionNumber == Questions.Length - 1;
        }

        /// <summary>
        /// Increments the question number by 1 and displays a new question.
        /// </summary>
        public void NextQuestion()
        {
            QuestionNumber++;

            DisplayQuestion();
        }

        /// <summary>
        /// Sets up the UI to display the current question.
        /// </summary>
        public abstract void DisplayQuestion();
    }
}
