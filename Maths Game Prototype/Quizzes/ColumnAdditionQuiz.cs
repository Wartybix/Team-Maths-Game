using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths_Game_Prototype.Quizzes
{
    internal class ColumnAdditionQuiz : ColumnAddSubQuiz
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ColumnAdditionQuiz()
        {
            QuizName = "Column Addition";
            OperatorCategory = Operators.Plus;

            IsAddition = true;
        }
    }
}
