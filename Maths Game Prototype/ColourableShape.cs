using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Path = System.Windows.Shapes.Path;

namespace Maths_Game_Prototype
{
    internal class ColourableShape
    {
        public Path Shape { get; } //The shape in the UI to be coloured in.
        public SolidColorBrush CurrentColour => (SolidColorBrush)Shape.Fill; //The current colour of the shape.
        public SolidColorBrush ExpectedColour { get; } //What the correct colour of the shape is.

        /// <summary>
        /// Constructor that allows the user to specify the UI element to be coloured in, and its correct colour, and sets those values accordingly.
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="expectedColour"></param>
        public ColourableShape(Path shape, SolidColorBrush expectedColour)
        {
            Shape = shape;
            ExpectedColour = expectedColour;
        }
    }
}
