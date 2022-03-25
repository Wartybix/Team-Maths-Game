using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Maths_Game_Prototype
{
    internal static class Operators
    {
        public static Operator Plus =>
            new Operator('+', new SolidColorBrush(Colors.DarkOrange), 
                new SolidColorBrush(Colors.Wheat));

        public static Operator Minus =>
            new Operator('-', new SolidColorBrush(Colors.MediumPurple),
                new SolidColorBrush(Colors.Plum));

    }
}
