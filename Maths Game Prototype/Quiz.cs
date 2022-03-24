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
        public string Name { get; set; }
        public Question[] Questions = new Question[5];
        protected Random Randoms;
        private MainWindow mainWindow;


        /// <summary>
        /// Adds questions to the quiz (in derived classes)
        /// Sets up UI for the quiz (in derived classes)
        /// Sets up new Random variable to set random variables in questions.
        /// </summary>
        public virtual void NewGame()
        {
            mainWindow = (MainWindow) Application.Current.MainWindow;
            Randoms = new Random();
        }

        /// <summary>
        /// Sets up the UI to display the current question.
        /// </summary>
        public abstract void DisplayQuestion();
    }
}
