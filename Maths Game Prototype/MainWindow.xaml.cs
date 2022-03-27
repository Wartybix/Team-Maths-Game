using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

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


        #endregion

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

        #endregion


        private void NextQBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void CheckAnsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            _currentQuiz.CheckAnswer();
        }
    }
}
