using System;
using System.IO;
using System.Media;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using KeyEventHandler = System.Windows.Input.KeyEventHandler;

namespace Maths_Game_Prototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Variable initialisation

        private Grid _currentlyOpenMenu; //Holds the menu currently visible
        private Quiz _currentQuiz; //Holds the quiz currently in progress.
        public StackPanel CurrentQuizLayout; //Holds the layout of the current quiz.
        public SoundPlayer SoundPlayer = new SoundPlayer(); //Plays a .wav file asynchronously while the rest of the program executes.
        public int Score = 0; //Holds the user's current score in a given quiz.

        #endregion

        /// <summary>
        /// Sets up UI and assigns variables using UI elements.
        /// Sets currently open menu to the welcome menu.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            _currentlyOpenMenu = WelcomeScreen;
        }

        #region Menu logic

        /// <summary>
        /// Collapses all menus aside from the menu in the parameter.
        /// Menu specified in parameter is made visible.
        /// </summary>
        /// <param name="destinationMenu">The menu to be made visible</param>
        private void TransitionTo(Grid destinationMenu)
        {
            _currentlyOpenMenu.Visibility = Visibility.Collapsed;
            destinationMenu.Visibility = Visibility.Visible;

            _currentlyOpenMenu = destinationMenu;
        }

        #endregion

        #region Quiz Logic

        /// <summary>
        /// Sets score to zero and updates score textblock accordingly.
        /// </summary>
        public void ResetScore()
        {
            Score = 0;
            ScoreNo.Text = Score.ToString();
        }

        /// <summary>
        /// Increments score by 1 and updates score textblock accordingly.
        /// </summary>
        public void IncrementScore()
        {
            Score++;
            ScoreNo.Text = Score.ToString();
        }

        /// <summary>
        /// Displays next question if there are questions remaining in the current quiz.
        /// Displays results page if there are no more questions in the current quiz.
        /// </summary>
        private void NextQuestion()
        {
            if (!_currentQuiz.EndOfQuiz())
                _currentQuiz.NextQuestion();
            else
                ShowResults();
        }

        /// <summary>
        /// Shows user's score from the quiz they just finished.
        /// </summary>
        public void ShowResults()
        {
            TransitionTo(ResultsPage);

            QuizNameResultsTxt.Text = _currentQuiz.QuizName;
            ResultsUserScoreTxt.Text = Score.ToString();
            ResultsMaxScoreTxt.Text = _currentQuiz.Questions.Length.ToString();

            PlaySound(Score == _currentQuiz.Questions.Length ? Sounds.thrilled : Sounds.conclusion);
        }

        #endregion

        #region Sound

        /// <summary>
        /// Sets SoundPlayer's audio stream to the argument passed and plays it.
        /// </summary>
        /// <param name="audioStream">The audio stream to be played</param>
        public void PlaySound(Stream audioStream)
        {
            SoundPlayer.Stream = audioStream;
            SoundPlayer.Play();
        }

        #endregion

        #region OnClicks

        private void Ks2Btn_OnClick(object sender, RoutedEventArgs e)
        {
            TransitionTo(QuizMenu);
        }
        private void Ks3_OnClick(object sender, RoutedEventArgs e)
        {
            TransitionTo(QuizMenu);
        }

        private void QuizBackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            TransitionTo(KsMenu);
        }

        private void WelcomeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            TransitionTo(KsMenu);
        }

        private void ExitQuizBtn_OnClick(object sender, RoutedEventArgs e)
        {
            TransitionTo(QuizMenu);
        }

        private void ExampleQuizBtn_OnClick(object sender, RoutedEventArgs e)
        {
            TransitionTo(QuizInstance);

            _currentQuiz = new MentalMathsQuiz();
            _currentQuiz.NewGame();
        }

        private void NextQBtn_OnClick(object sender, RoutedEventArgs e)
        {
            NextQuestion();
        }

        private void CheckAnsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            _currentQuiz.CheckAnswer();
        }
        private void Replay_OnClick(object sender, RoutedEventArgs e)
        {
            TransitionTo(QuizInstance);

            _currentQuiz.NewGame();
        }

        #endregion

        #region KeyPress

        /// <summary>
        /// Enter key checks the current question's answer when pressed.
        /// Alternatively, advances to next question if answer is already displayed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuizInstance_OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            if (AnswerRevealArea.Visibility == Visibility.Visible)
                NextQuestion();
            else
                _currentQuiz.CheckAnswer();
        }

        #endregion
    }
}
