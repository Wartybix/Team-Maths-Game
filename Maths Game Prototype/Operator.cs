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
        public string Symbol { get; } //Symbol of the operator (e.g. '+', '-')
        public SolidColorBrush Colour { get; } //Colour of the symbol
        public SolidColorBrush LightColour { get; } //Colour used in the quiz's title bar depending on its operator category

        /// <summary>
        /// Constructor for making operator of given symbol, colour and light colour.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="colour"></param>
        /// <param name="lightColour"></param>
        public Operator(string symbol, SolidColorBrush colour, SolidColorBrush lightColour)
        {
            Symbol = symbol;
            Colour = colour;
            LightColour = lightColour;
        }

        /// <summary>
        /// Constructor for making generic operator.
        /// Used when a quiz has no specific operator category.
        /// </summary>
        public Operator()
        {
            LightColour = new SolidColorBrush(Color.FromRgb(0x99, 0xE8, 0X8C));
        }
    }
}
