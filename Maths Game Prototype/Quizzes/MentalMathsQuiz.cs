using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Maths_Game_Prototype
{
    internal class MentalMathsQuiz : Quiz
    {
        public MentalMathsQuiz()
        {
            QuizName = "Mental Maths";
        }

        public override void NewGame()
        {
            base.NewGame();

            foreach (Question question in Questions)
            {
                int x = Randoms.Next(0, 10);
                int y = Randoms.Next(0, 10);

                question.QuestionVariables.Add("x", x);
                question.QuestionVariables.Add("y", y);

                question.ExpectedAnswer.Add("ans", (x + y).ToString());
            }
        }

        public override void DisplayQuestion()
        {

        }
    }
}
