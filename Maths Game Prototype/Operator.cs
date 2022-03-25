using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Brush = System.Drawing.Brush;

namespace Maths_Game_Prototype
{
    internal class Operator
    {
        public char Symbol { get; }
        public SolidColorBrush Colour { get; }
        public SolidColorBrush LightColour { get; }

        public Operator(char symbol, SolidColorBrush colour, SolidColorBrush lightColour)
        {
            Symbol = symbol;
            Colour = colour;
            LightColour = lightColour;
        }
    }
}
