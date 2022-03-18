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

        private readonly Grid[] _menus; //Holds all menu grids (i.e. KS2 selection menu, quiz selection menu, etc.)

        #endregion

        public MainWindow()
        {
            InitializeComponent();

            //testing

            _menus = new[] { WelcomeScreen, KsMenu, QuizMenu, QuizInstance }; //Populates menus array with all menus in the UI
        }

        #region Menu logic

        /// <summary>
        /// Collapses all menus aside from the menu in the parameter.
        /// Menu specified in parameter is made visible.
        /// </summary>
        /// <param name="destinationMenu">The menu to be made visible</param>
        private void TransitionTo(Grid destinationMenu)
        {
            foreach (var menu in _menus)
            {
                menu.Visibility = menu == destinationMenu ? Visibility.Visible : Visibility.Collapsed;
            }
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
        #endregion

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
        }
    }
}
