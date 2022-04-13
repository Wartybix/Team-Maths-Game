using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Maths_Game_Prototype
{
    internal abstract class Minigame
    {
        public string GameName { get; set; }
        protected Random Randoms;
        protected MainWindow MainWindow;

        /// <summary>
        /// Sets title in upper corner of minigame to minigame title.
        /// Changes background colour to blue and hides all quiz-specific UI elements.
        /// Sets up new Random variable to set random variables in derived minigames.
        /// </summary>
        public virtual void NewGame()
        {
            MainWindow = (MainWindow) Application.Current.MainWindow;
            Randoms = new Random();

            if (MainWindow == null) return;

            MainWindow.GameNameTxt.Text = GameName;
            MainWindow.GameInstance.Background = Backgrounds.MinigameBackground;
            MainWindow.TitleColour.Background = Operators.Generic.LightColour;
            MainWindow.PaperTip.Visibility = Visibility.Collapsed;
            MainWindow.ScoreArea.Visibility = Visibility.Collapsed;
            MainWindow.CheckAnsBtn.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Hides the previous quiz/minigame's UI
        /// Shows the minigame layout given as a parameter.
        /// </summary>
        /// <param name="layout"></param>
        protected void ShowMinigameLayout(dynamic layout)
        {
            if (MainWindow.CurrentGameLayout != null) MainWindow.CurrentGameLayout.Visibility = Visibility.Collapsed;
            MainWindow.CurrentGameLayout = layout;
            layout.Visibility = Visibility.Visible;
        }
    }
}
