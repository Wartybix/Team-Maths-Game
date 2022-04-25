using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths_Game_Prototype.Quizzes
{
    internal class SquaringQuiz : PowersQuiz
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SquaringQuiz() : base()
        {
            QuizName = "Squaring Whole Numbers";
            IsCube = false;
        }
    }
}
