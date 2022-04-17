using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Maths_Game_Prototype.Minigames;
using Maths_Game_Prototype.Quizzes;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using KeyEventHandler = System.Windows.Input.KeyEventHandler;
using Path = System.Windows.Shapes.Path;

namespace Maths_Game_Prototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Variable initialisation

        private Grid _currentlyOpenMenu; //Holds the menu currently visible
        private Border _currentlyOpenTool; //Holds tool last opened.
        private string _calcDisplayTxt = string.Empty; //Holds text to be written to calculator display
        private double _calcAcc; //Holds accumulator for calc.
        private char? _calcOperator; //Holds operator in use for calc.
        private readonly int _calcDisplayMaxLength = 9; //Holds maximum number of digits for calculator screen.
        private Quiz _currentQuiz; //Holds the quiz currently in progress.
        private Minigame _currentMinigame; //Holds the minigame currently in progress.
        public dynamic CurrentGameLayout; //Holds the layout of the current quiz.
        public SoundPlayer SoundPlayer = new SoundPlayer(); //Plays a .wav file asynchronously while the rest of the program executes.
        public int Score; //Holds the user's current score in a given quiz.
        private readonly Quiz[] _quizzes = //Holds all playable quizzes
        {
            new WordsToDigitsQuiz(),
            new MentalMathsQuiz(),
            new ColumnAdditionQuiz(),
            new ColumnSubtractionQuiz(),
            new NumberSequencesQuiz(),
            new MultiplicationTablesQuiz(),
            new BusStopDivisionQuiz(),
            new RoundingQuiz(),
            new RoundingDecimalsQuiz(),
            new PrimeCompositeNumbersQuiz(),
            new SquaringQuiz(),
            new CubingQuiz(),
            new HcfQuiz(),
            new LcmQuiz(),
            new AlgebraQuiz()
        };

        private readonly Minigame[] _minigames = //Holds all playable minigames
        {
            new ColourByNumbersMinigame()
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
            LoadGames();
            PopulateNumberGrid();
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

        /// <summary>
        /// Loads and displays all quizzes and minigames as buttons on the main menu.
        /// </summary>
        private void LoadGames()
        {
            foreach (var quiz in _quizzes) //Displays all quizzes as buttons
            {
                var quizBtn = new Button //Creates a new button
                {
                    Content = new TextBlock {Text = quiz.QuizName}, //Sets button text to quiz name
                    Style = (Style)Resources["ListBtn"], //Sets button style appropriate for lists.
                    Tag = quiz //Sets an instance of the quiz as the button's tag
                };

                quizBtn.Click += QuizBtn_OnClick; //Allows user to start associated quiz when button is clicked

                QuizSelector.Children.Add(quizBtn); //Adds button to the quiz stackpanel.
            }

            foreach (var minigame in _minigames)
            {
                var border = new Border
                {
                    //BorderBrush = new SolidColorBrush(Colors.Transparent),
                    Margin = new Thickness(0, 2, 0, 2),
                    Background = new SolidColorBrush(Colors.LightGray)
                };
            
                var frameSp = new StackPanel();
                border.Child = frameSp;

                var infoCard = new StackPanel
                {
                    Margin = new Thickness(8)
                };

                frameSp.Children.Add(new Image
                {
                    Source = new BitmapImage(new Uri("/Images/cbn-preview.png", UriKind.Relative))
                });

                frameSp.Children.Add(new Rectangle
                {
                    Height = 1,
                    Fill = new SolidColorBrush(Colors.Gray)
                });

                frameSp.Children.Add(infoCard);

                infoCard.Children.Add(new TextBlock
                {
                    Text = minigame.GameName,
                    Style = Resources["SmallText"] as Style,
                    FontFamily = Resources["ComicNeue-Bold"] as FontFamily,
                });
                infoCard.Children.Add(new TextBlock
                {
                    Text = minigame.Tooltip,
                    Style = Resources["SmallText"] as Style,
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center
                });

                var playBtn = new Button
                {
                    Content = new TextBlock {Text = "Play!"},
                    Tag = minigame,
                    Margin = new Thickness(0, 8, 0, 0)
                };

                playBtn.Click += MinigameBtn_OnClick;

                infoCard.Children.Add(playBtn);

                MinigameSelector.Children.Add(border);
            }
        }

        #endregion

        #region Quiz Logic
        private void PopulateNumberGrid()
        {
            for (int x = 0; x < NumberGrid.RowDefinitions.Count; x++)
            {
                for (int y = 0; y < NumberGrid.ColumnDefinitions.Count; y++)
                {
                    var border = new Border
                    {
                        CornerRadius = new CornerRadius(0),
                    };

                    border.SetValue(Grid.RowProperty, x);
                    border.SetValue(Grid.ColumnProperty, y);

                    var numValue = (x * 10) + y;

                    border.Child = new TextBlock
                    {
                        Text = numValue.ToString(),
                        Margin = new Thickness(0, 2, 0, 2),
                        Width = 32,
                        TextAlignment = TextAlignment.Center,
                        Foreground = new SolidColorBrush(numValue % 2 == 0 ? Colors.PaleVioletRed : Colors.SteelBlue),
                        FontFamily = Resources["ComicNeue-Bold"] as FontFamily
                    };

                    NumberGrid.Children.Add(border);
                }
            }
        }

        private void SelectTool(Border tool)
        {
            if (_currentlyOpenTool != null) _currentlyOpenTool.Visibility = Visibility.Collapsed;
            if (_currentlyOpenTool == tool && ToolArea.Visibility == Visibility.Visible)
            {
                ToolArea.Visibility = Visibility.Collapsed;
                return;
            }
            _currentlyOpenTool = tool;
            ToolArea.Visibility = Visibility.Visible;
            tool.Visibility = Visibility.Visible;
        }

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

        /// <summary>
        /// Plays the quiz associated with the button pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuizBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var buttonClicked = (Button)sender;

            TransitionTo(GameInstance);
            _currentMinigame = null;
            _currentQuiz = (Quiz)buttonClicked.Tag;
            _currentQuiz.NewGame();
        }

        /// <summary>
        /// Plays the minigame associated with the button press.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinigameBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var buttonClicked = (Button)sender;

            TransitionTo(GameInstance);
            _currentQuiz = null;
            _currentMinigame = (Minigame)buttonClicked.Tag;
            _currentMinigame.NewGame();
        }

        private void Ks2Btn_OnClick(object sender, RoutedEventArgs e)
        {
            TransitionTo(QuizMenu);
        }
        private void Ks3_OnClick(object sender, RoutedEventArgs e)
        {
            TransitionTo(QuizMenu);
        }

        /// <summary>
        /// Goes back to the welcome screen from the main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuizBackBtn_OnClick(object sender, RoutedEventArgs e)
        {
            TransitionTo(WelcomeScreen);
        }

        /// <summary>
        /// Goes to the main menu from the welcome screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WelcomeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            TransitionTo(QuizMenu);
        }

        /// <summary>
        /// Goes back to the main menu from a quiz instance.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitQuizBtn_OnClick(object sender, RoutedEventArgs e)
        {
            SoundPlayer.Stop();
            TransitionTo(QuizMenu);
        }

        /// <summary>
        /// Transitions to the next question in a quiz.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextQBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (_currentQuiz != null)
                NextQuestion();
            else if (_currentMinigame != null)
            {
                TransitionTo(QuizMenu);
                SoundPlayer.Stop();
            }
        }

        /// <summary>
        /// Checks if the given answer(s) is correct when pressed and displays the result.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckAnsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            _currentQuiz.CheckAnswer();
        }

        /// <summary>
        /// Restarts the current quiz when pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Replay_OnClick(object sender, RoutedEventArgs e)
        {
            SoundPlayer.Stop();
            TransitionTo(GameInstance);
            _currentQuiz.NewGame();
        }

        /// <summary>
        /// Fills the user's bucket with the colour clicked on in the colour by numbers key.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectColour(object sender, MouseButtonEventArgs e)
        {
            var keyBtn = sender as Border;
            var cBNGame = _currentMinigame as ColourByNumbersMinigame;

            cBNGame.SelectedColour = keyBtn.Background as SolidColorBrush; //Gets background colour of selected paint pot.
            CateBackground.Cursor = new Cursor(Application.GetResourceStream(new Uri($"Buckets/bucket-{cBNGame.PaintPots[keyBtn]}.cur", UriKind.Relative)).Stream);
                //Sets cursor to paint bucket icon associated with the paint pot clicked on.
        }

        private void PrimeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var quiz = _currentQuiz as PrimeCompositeNumbersQuiz;
            quiz.PrimeSelected = true;
            quiz.CheckAnswer();
        }

        private void CompositeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var quiz = _currentQuiz as PrimeCompositeNumbersQuiz;
            quiz.PrimeSelected = false;
            quiz.CheckAnswer();
        }

        private void CalcToolbarBtn_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectTool(CalculatorTool);
        }
        private void GridToolbarBtn_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectTool(NumberGridTool);
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
        /// Alternatively, advances to next question if answer is already displayed and the quiz can be advanced.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuizInstance_OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            if (AnswerRevealArea.Visibility == Visibility.Visible)
                NextQuestion();
            else if (_currentQuiz.CanAdvance)
                _currentQuiz.CheckAnswer();
        }

        /// <summary>
        /// Checks whether the input of given textbox (sender) matches the regular expression of the current quiz's text restriction
        /// If it does not match the regex, the character the user entered won't be added.
        /// If the character does match the regex, the question can now be advanced.
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

            if (e.Handled) return;

            _currentQuiz.CanAdvance = true;
            CheckAnsBtn.IsEnabled = true;
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

        #region Minigame Logic

        /// <summary>
        /// Fills the shape in the picture clicked on with the currently selected colour.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetShapeColour(object sender, MouseButtonEventArgs e)
        {
            var shape = sender as Path;
            var cBNGame = _currentMinigame as ColourByNumbersMinigame;

            if (cBNGame.SelectedColour == null) return; //Leaves procedure if there is no colour selected.

            shape.Fill = cBNGame.SelectedColour; //Fills the shape clicked on with the selected colour.
            cBNGame.CheckColours(); //Checks to see if all shapes in picture are of the correct colour.
        }

        #endregion

        #region Calc Logic

        public void ResetCalc()
        {
            SetCalcText(string.Empty);
            _calcDisplayTxt = string.Empty;
            _calcOperator = null;
            _calcAcc = 0;
        }

        private string GetCalcText()
        {
            return CalcDisplay.Text;
        }

        private void SetCalcText(string text)
        {
            CalcDisplay.Text = text;
        }

        private bool CalcOverflown()
        {
            return GetCalcText() == "Overflow";
        }

        private void Calculate()
        {
            double enteredNumber;

            if (CalcOverflown()) return;

            try
            {
                enteredNumber = Convert.ToDouble(GetCalcText());
            }
            catch
            {
                return;
            }

            enteredNumber = Convert.ToDouble(GetCalcText());

            if (_calcOperator == null)
            {
                _calcDisplayTxt = string.Empty;
                _calcAcc = enteredNumber;
                return;
            }

            double result = 0;

            switch (_calcOperator)
            {
                case '+':
                    result = _calcAcc + enteredNumber;
                    break;
                case '-':
                    result = _calcAcc - enteredNumber;
                    break;
                case '*':
                    result = _calcAcc * enteredNumber;
                    break;
                case '/':
                    result = _calcAcc / enteredNumber;
                    break;
            }

            var strResult = result.ToString();

            if (strResult.Length > _calcDisplayMaxLength)
            {
                Char shavedDigit = '\0';
                while (strResult.Length > _calcDisplayMaxLength && strResult.Contains("."))
                {
                    shavedDigit = strResult.Last();
                    strResult = strResult.Remove(strResult.Length - 1, 1);
                }

                if (strResult.Length <= _calcDisplayMaxLength)
                {
                    strResult += shavedDigit;
                    result = Convert.ToDouble(strResult);
                    result = Math.Round(result, 9, MidpointRounding.AwayFromZero);
                    strResult = result.ToString();
                    
                    _calcAcc = result;
                    SetCalcText(strResult);
                    _calcDisplayTxt = string.Empty;
                }
                else
                {
                    _calcAcc = 0;
                    SetCalcText("Overflow");
                    _calcDisplayTxt = string.Empty;
                }
            }
            else
            {
                _calcAcc = result;
                SetCalcText(strResult);
                _calcDisplayTxt = string.Empty;
            }

            _calcOperator = null;
        }

        private void Digit_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var btnTxt = btn.Content.ToString();

            var calcText = _calcDisplayTxt;

            if (calcText.Length >= _calcDisplayMaxLength) return;

            if (calcText == "0" && btnTxt != ".") return;

            if (calcText == string.Empty && btnTxt == ".") return;

            if (calcText.Contains(".") && btnTxt == ".") return;

            _calcDisplayTxt = calcText + btnTxt;
            SetCalcText(_calcDisplayTxt);
        }

        private void ModifierBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var btnTxt = (sender as Button).Content.ToString();

            if (btnTxt == "AC")
            {
                ResetCalc();
                return;
            }

            if (_calcDisplayTxt.Length > 0)
            {
                _calcDisplayTxt = _calcDisplayTxt.Remove(_calcDisplayTxt.Length - 1, 1);
                SetCalcText(_calcDisplayTxt);
            }
        }

        private void DivideBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Calculate();
            _calcOperator = '/';
        }

        private void MultiplyBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Calculate();
            _calcOperator = '*';
        }

        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Calculate();
            _calcOperator = '+';
        }

        private void SubtractBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Calculate();
            _calcOperator = '-';
        }

        private void Equals_OnClick(object sender, RoutedEventArgs e)
        {
            Calculate();
        }

        #endregion
    }
}
