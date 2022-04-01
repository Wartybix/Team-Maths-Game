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
        protected Operator OperatorCategory { get; set; } //The operator category of the quiz (e.g. addition focused quiz, division focused quiz, etc.)
        public Question[] Questions = new Question[5]; //Array of questions associated with the quiz
        protected int QuestionNumber { get; set; } //The index of the current question.
        protected Random Randoms; //A variable that produces random values for use in child classes.
        protected MainWindow MainWindow; //References the MainWindow.
        protected bool PaperTip = false; //True if 'do workings on paper' tip is shown in quiz.


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
            MainWindow.PaperTip.Visibility = PaperTip ? Visibility.Visible : Visibility.Collapsed;
            MainWindow.ResetScore();
        }
        
        /// <summary>
        /// Hides the previous quiz's layout.
        /// Displays the current quiz's layout.
        /// </summary>
        /// <param name="layout"></param>
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
        /// Enables input of answer textboxes.
        /// </summary>
        protected virtual void DisplayQuestion()
        {
            MainWindow.AnswerRevealArea.Visibility = Visibility.Collapsed;
            MainWindow.CheckAnsBtn.Visibility = Visibility.Visible;
            LockQuestion(false);
        }

        /// <summary>
        /// Enables/disables input of textboxes when answer is revealed.
        /// </summary>
        /// <param name="locked">True is input is to be disabled, False if input is to be enabled.</param>
        protected abstract void LockQuestion(bool locked);

        /// <summary>
        /// Checks user input against the current question's expected answer.
        /// Disables input of answer textboxes.
        /// Changes text of 'next question button' to 'Finish' or 'Next' depending if any more questions remain in the quiz.
        /// </summary>
        public virtual void CheckAnswer()
        {
            MainWindow.CheckAnsBtn.Visibility = Visibility.Collapsed;
            MainWindow.AnswerRevealArea.Visibility = Visibility.Visible;
            LockQuestion(true);

            MainWindow.NextQBtn.Content = new TextBlock()
                {Text = QuestionNumber == Questions.Length - 1 ? "Finish" : "Next >"};
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

        /// <summary>
        /// Displays a message when the user answers a question incorrectly.
        /// </summary>
        /// <param name="expectedAnswer"></param>
        public void WrongAnswer(string expectedAnswer)
        {
            MainWindow.AnswerRevealText.Text = $"No. The answer is {expectedAnswer}.";
            //MainWindow.PlaySound(Sounds.disappointment);
        }
    }
}
