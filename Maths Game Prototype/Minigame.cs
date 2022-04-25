using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Maths_Game_Prototype
{
    internal abstract class Minigame
    {
        public string GameName { get; set; } //The name of the minigame
        protected Random Randoms; //Holds random values for minigames to use
        protected MainWindow MainWindow; //Allows access to UI elements within this class
        public string Tooltip; //Holds the written instructions for the minigame for the user to read
        public string PreviewImgName { get; set; } //Name of image for minigame thumbnail displayed in main menu.

        /// <summary>
        /// Sets title in upper corner of minigame to minigame title.
        /// Changes background colour to blue
        /// Hides all quiz-specific UI elements and shows all minigame-specific UI elements.
        /// Sets up new Random variable to set random variables in derived minigames.
        /// </summary>
        public virtual void NewGame()
        {
            MainWindow = (MainWindow) Application.Current.MainWindow;
            Randoms = new Random();

            if (MainWindow == null) return; //Leaves subroutine if MainWindow hasn't loaded yet.

            MainWindow.GameNameTxt.Text = GameName; //Sets game title
            MainWindow.MinigameTooltip.Text = Tooltip; //Sets minigame instructions
            MainWindow.GameInstance.Background = Backgrounds.MinigameBackground; //Sets background to blue
            MainWindow.TitleColour.Background = Operators.Generic.LightColour; //Sets title colour to green
            MainWindow.ScoreArea.Visibility = Visibility.Collapsed; //Hides score area
            MainWindow.PaperTip.Visibility = Visibility.Collapsed; //Hides 'do your workings on paper' tip
            MainWindow.CheckAnsBtn.Visibility = Visibility.Collapsed; //Hides the 'Check' button
            MainWindow.AnswerRevealArea.Visibility = Visibility.Collapsed; //Hides the answer reveal area
            MainWindow.MinigameTooltipArea.Visibility = Visibility.Visible; //Shows the minigame's instructions.
            MainWindow.ToolArea.Visibility = Visibility.Collapsed; //Hides the tool area.
            MainWindow.Toolbar.Visibility = Visibility.Collapsed; //Hides the toolbar.
            MainWindow.AnswerRevealArea.Background = Backgrounds.MinigameBackgroundLight; //Changes the answer reveal area to a pale blue.
            MainWindow.AnswerRevealText.Foreground = new SolidColorBrush(Colors.MidnightBlue); //Changes the answer reveal text to a dark blue.
        }

        /// <summary>
        /// Hides the previous quiz/minigame's UI
        /// Shows the minigame layout given as a parameter.
        /// </summary>
        /// <param name="layout"></param>
        protected void ShowMinigameLayout(dynamic layout)
        {
            if (MainWindow.CurrentGameLayout != null) MainWindow.CurrentGameLayout.Visibility = Visibility.Collapsed; //Hides the previous game layout if exists
            MainWindow.CurrentGameLayout = layout; //Sets the current game layout to the argument
            layout.Visibility = Visibility.Visible; //Shows the layout in the argument
        }

        /// <summary>
        /// Prevents/enables all user input during the game.
        /// </summary>
        /// <param name="locked">True to disable all user input, false to enable all user input</param>
        protected abstract void LockGame(bool locked);
    }
}
