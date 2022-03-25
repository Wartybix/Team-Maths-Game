using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace Maths_Game_Prototype
{
    internal static class Operators
    {
        public static Operator Plus =>
            new Operator('+', new SolidColorBrush(Color.FromRgb(0xFF, 0x8A, 0x00)),
                new SolidColorBrush(Color.FromRgb(0xFF, 0xC2, 0x66)));

        public static Operator Minus =>
            new Operator('-', new SolidColorBrush(Color.FromRgb(0xC8, 0x32, 0xCB)),
                new SolidColorBrush(Color.FromRgb(0xD2, 0xA0, 0xF1)));

        public static Operator Multiply =>
            new Operator('×', new SolidColorBrush(Color.FromRgb(0x20, 0x4F, 0xF6)),
                new SolidColorBrush(Color.FromRgb(0x7C, 0x95, 0xED)));

        public static Operator Divide =>
            new Operator('÷', new SolidColorBrush(Color.FromRgb(0xC0, 0x56, 0x56)),
                new SolidColorBrush(Color.FromRgb(0xC0, 0x56, 0x56)));

        public static Operator Generic =>
            new Operator();
    }
}
