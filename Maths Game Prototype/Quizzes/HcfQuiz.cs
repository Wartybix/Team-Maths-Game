using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Maths_Game_Prototype.Quizzes
{
    internal class HcfQuiz : CommonNumbersQuiz
    {
        public HcfQuiz()
        {
            QuizName = "Highest Common Factor";
            OperatorCategory = Operators.Generic;
            TextInputRestriction = new Regex("^[\\d]{0,2}$");
            IsLcmQuiz = false;
        }
    }
}
