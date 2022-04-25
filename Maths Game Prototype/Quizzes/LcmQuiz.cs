using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Maths_Game_Prototype.Quizzes
{
    internal class LcmQuiz : CommonNumbersQuiz
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public LcmQuiz()
        {
            QuizName = "Lowest Common Multiple";
            OperatorCategory = Operators.Generic;
            TextInputRestriction = new Regex("^[\\d]{0,4}$"); //Allows a 4 digit positive integer to be entered.
            IsLcmQuiz = true;
        }
    }
}
