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
        public char Symbol { get; } //Symbol for the operator (e.g. '+', '-')
        public SolidColorBrush Colour { get; } //Colour of symbol when used.
        public SolidColorBrush LightColour { get; } //Colour of title bar where quiz is of this operator category.

        public Operator(char symbol, SolidColorBrush colour, SolidColorBrush lightColour)
        {
            Symbol = symbol;
            Colour = colour;
            LightColour = lightColour;
        }

        /// <summary>
        /// For creating generic operator.
        /// Used for quiz heading background where there is no specific operator category (e.g. mental maths).
        /// </summary>
        public Operator()
        {
            LightColour = new SolidColorBrush(Color.FromRgb(0x99, 0xE8, 0x8C));
        }
    }
}
