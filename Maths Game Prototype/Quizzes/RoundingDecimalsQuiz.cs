using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Maths_Game_Prototype.Quizzes
{
    internal class RoundingDecimalsQuiz : RoundingQuiz
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public RoundingDecimalsQuiz()
        {
            QuizName = "Rounding Decimals";
            IntegerRounding = false;
            TextInputRestriction = new Regex("^[\\d]{0,3}$"); //Allows the user to enter a 3 digit positive integer
        }
    }
}
