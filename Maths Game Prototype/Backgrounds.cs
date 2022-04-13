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
        public static SolidColorBrush MinigameBackground => new SolidColorBrush(Color.FromRgb(0x84, 0xCD, 0xF6));
        public static SolidColorBrush QuizBackground => new SolidColorBrush(Color.FromRgb(0xF8, 0xFA, 0xB1));
    }
}
