using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Maths_Game_Prototype
{
    internal abstract class Quiz
    {
        public string QuizName { get; set; } //Name of the quiz
        protected Operator OperatorCategory { get; set; }
        public Question[] Questions = new Question[5]; //Array of questions associated with the quiz
        protected int QuestionNumber { get; set; } //The index of the current question.
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

            if (MainWindow == null) return;

            MainWindow.QuizNameTxt.Text = QuizName;
            MainWindow.TitleColour.Background = OperatorCategory.LightColour;
            MainWindow.ResetScore();
        }

        protected void ShowQuizLayout(StackPanel layout)
        {
            if (MainWindow.CurrentQuizLayout != null) MainWindow.CurrentQuizLayout.Visibility = Visibility.Collapsed;
            MainWindow.CurrentQuizLayout = layout;
            layout.Visibility = Visibility.Visible;
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
        protected virtual void DisplayQuestion()
        {
            MainWindow.AnswerRevealArea.Visibility = Visibility.Collapsed;
            MainWindow.CheckAnsBtn.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Checks user input against the current question's expected answer.
        /// </summary>
        public virtual void CheckAnswer()
        {
            MainWindow.CheckAnsBtn.Visibility = Visibility.Collapsed;
            MainWindow.AnswerRevealArea.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Displays a message when the user answers a question correctly.
        /// </summary>
        public void RightAnswer()
        {
            MainWindow.AnswerRevealText.Text = "Yes. That is correct.";
            MainWindow.PlaySound(Sounds.applause);
            MainWindow.IncrementScore();
        }

        public void WrongAnswer(string expectedAnswer)
        {
            MainWindow.AnswerRevealText.Text = $"No. The answer is {expectedAnswer}.";
            MainWindow.PlaySound(Sounds.disappointment);
        }
    }
}
