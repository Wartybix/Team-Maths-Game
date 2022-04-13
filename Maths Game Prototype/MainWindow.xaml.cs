using System;
using System.IO;
using System.Media;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using Maths_Game_Prototype.Quizzes;
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
        public dynamic CurrentQuizLayout; //Holds the layout of the current quiz.
        public SoundPlayer SoundPlayer = new SoundPlayer(); //Plays a .wav file asynchronously while the rest of the program executes.
        public int Score; //Holds the user's current score in a given quiz.
        private readonly Quiz[] _quizzes =
        {
            new WordsToDigitsQuiz(),
            new MentalMathsQuiz(),
            new ColumnAdditionQuiz(),
            new ColumnSubtractionQuiz(),
            new NumberSequencesQuiz(),
            new MultiplicationTablesQuiz(),
            new BusStopDivisionQuiz(),
            new RoundingQuiz(),
            new RoundingDecimalsQuiz()
        };

        #endregion

        /// <summary>
        /// Sets up UI and assigns variables using UI elements.
        /// Sets currently open menu to the welcome menu.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            _currentlyOpenMenu = WelcomeScreen;
            LoadQuizzes();
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

        private void LoadQuizzes()
        {
            foreach (var quiz in _quizzes)
            {
                var quizBtn = new Button
                {
                    Content = new TextBlock() {Text = quiz.QuizName},
                    Style = (Style)Resources["ListBtn"],
                    Tag = quiz
                };

                quizBtn.Click += QuizBtn_OnClick;

                QuizSelector.Children.Add(quizBtn);
            }
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

            PlaySound(Sounds.thrilled);
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
        private void QuizBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var buttonClicked = (Button)sender;

            TransitionTo(QuizInstance);
            _currentQuiz = (Quiz)buttonClicked.Tag;
            _currentQuiz.NewGame();
        }

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
            TransitionTo(WelcomeScreen);
        }

        private void WelcomeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            TransitionTo(QuizMenu);
        }

        private void ExitQuizBtn_OnClick(object sender, RoutedEventArgs e)
        {
            SoundPlayer.Stop();
            TransitionTo(QuizMenu);
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
            SoundPlayer.Stop();
            TransitionTo(QuizInstance);
            _currentQuiz.NewGame();
        }

        #endregion

        #region KeyPress

        /// <summary>
        /// Prevents spaces being entered into textboxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuizInstance_OnPreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

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

        /// <summary>
        /// Checks whether the input of given textbox (sender) matches the regular expression of the current quiz's text restriction
        /// If it does not match the regex, the character the user entered won't be added.
        /// </summary>
        /// <param name="sender">The textbox the user has written into</param>
        /// <param name="e">The value of what the user has typed</param>
        private void ValidateInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = (TextBox)sender; //Casts sender as textbox

            var writtenText = textBox.Text; //Extracts the textbox's text as a string
            writtenText = writtenText.Remove(textBox.SelectionStart, textBox.SelectionLength); //Removes text that has been highlighted by user (for overwriting)
            writtenText = writtenText.Insert(textBox.CaretIndex, e.Text); //Inserts the character the user has typed into the position of the text where the caret is.

            e.Handled = !_currentQuiz.TextInputRestriction.IsMatch(writtenText); //Adds character if the textbox's text matches the text input restriction regex of the current quiz.
        }

        /// <summary>
        /// Prevents pasting into textboxes
        /// </summary>
        /// <param name="sender">Currently focused textbox</param>
        /// <param name="e">Value of what user has executed</param>
        private void Tb_OnPreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste) //If the user has pasted something
                e.Handled = true; //Don't add anything to the textbox
        }

        #endregion
    }
}
