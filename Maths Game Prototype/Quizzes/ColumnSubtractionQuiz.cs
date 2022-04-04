using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths_Game_Prototype.Quizzes
{
    internal class ColumnSubtractionQuiz : ColumnAddSubQuiz
    {
        public ColumnSubtractionQuiz() : base()
        {
            QuizName = "Column Subtraction";
            OperatorCategory = Operators.Minus;

            IsAddition = false;
        }
    }
}
