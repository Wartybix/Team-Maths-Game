using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maths_Game_Prototype.Quizzes
{
    internal class CubingQuiz : PowersQuiz
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CubingQuiz()
        {
            QuizName = "Cubing Numbers";
            PaperTip = true;
            IsCube = true;
        }
    }
}
