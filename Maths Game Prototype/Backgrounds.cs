using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Maths_Game_Prototype
{
    internal static class Backgrounds
    {
        public static SolidColorBrush MinigameBackground => new SolidColorBrush(Color.FromRgb(0x84, 0xCD, 0xF6)); //Background used in minigames
        public static SolidColorBrush MinigameBackgroundLight => new SolidColorBrush(Color.FromRgb(0xBA, 0xE6, 0xFF)); //Background used in minigames
        public static SolidColorBrush QuizBackground => new SolidColorBrush(Color.FromRgb(0xF8, 0xFA, 0xB1)); //Background used in quizzes.
        public static SolidColorBrush QuizBackgroundLight => new SolidColorBrush(Color.FromRgb(0xFE, 0xFF, 0xCB)); //Background used in quizzes.
    }
}
