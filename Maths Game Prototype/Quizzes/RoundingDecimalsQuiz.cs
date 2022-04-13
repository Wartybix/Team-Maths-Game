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
        public RoundingDecimalsQuiz()
        {
            QuizName = "Rounding Decimals";
            IntegerRounding = false;
            TextInputRestriction = new Regex("^[\\d]{0,3}$");
        }
    }
}
