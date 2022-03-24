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
        public string QuizName { get; set; }
        public Question[] Questions = new Question[5];
        protected Random Randoms;
        protected MainWindow MainWindow;


        /// <summary>
        /// Adds questions to the quiz (in derived classes)
        /// Sets up UI for the quiz (in derived classes)
        /// Sets up new Random variable to set random variables in questions.
        /// </summary>
        public virtual void NewGame()
        {
            MainWindow = (MainWindow) Application.Current.MainWindow;
            Randoms = new Random();

            if (MainWindow != null) MainWindow.QuizNameTxt.Text = QuizName;
        }

        /// <summary>
        /// Sets up the UI to display the current question.
        /// </summary>
        public abstract void DisplayQuestion();
    }
}
