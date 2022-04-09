using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Maths_Game_Prototype.Quizzes
{
    internal class NumberSequencesQuiz : Quiz
    {
        private TextBlock[] _sequenceTextBlocks;
        private TextBox[] _sequenceInputs;

        public NumberSequencesQuiz()
        {
            QuizName = "Number Sequences";
            OperatorCategory = Operators.Generic;
            TextInputRestriction = new Regex("^-?[\\d]{0,2}$");
        }

        public override void NewGame()
        {
            base.NewGame();

            _sequenceTextBlocks = new[]
            {
                MainWindow.SeqNum1TextBlock,
                MainWindow.SeqNum2TextBlock,
                MainWindow.SeqNum3TextBlock,
                MainWindow.SeqNum4TextBlock,
                MainWindow.SeqNum5TextBlock
            };

            _sequenceInputs = new[]
            {
                MainWindow.SeqNum1Input,
                MainWindow.SeqNum2Input,
                MainWindow.SeqNum3Input,
                MainWindow.SeqNum4Input,
                MainWindow.SeqNum5Input
            };

            for (var index = 0; index < Questions.Length; index++)
            {
                var questionVariables = new Dictionary<string, dynamic>();
                var expectedAnswer = new Dictionary<string, string>();

                //a_n = a_1 + nd
                var a1 = Randoms.Next(-49, 50); //First term of sequence is between -49 and 49 (inclusive)
                
                var d = Randoms.Next(1, 11); //Difference between sequences between 1 and 10 (inc.)
                var dIsNegative = Randoms.NextDouble() >= 0.5;
                d = dIsNegative ? -d : d;

                var ansIndex = Randoms.Next(0, _sequenceTextBlocks.Length); //The index of sequence the user has to fill in.

                questionVariables.Add("a1", a1);
                questionVariables.Add("d", d);
                questionVariables.Add("ansIndex", ansIndex);

                expectedAnswer["ans"] = (a1 + ansIndex * d).ToString();

                Questions[index] = new Question(questionVariables, expectedAnswer);
            }

            ShowQuizLayout(MainWindow.NumberSequencesArea);
            DisplayQuestion();
        }

        protected override void DisplayQuestion()
        {
            base.DisplayQuestion();

            var currentQuestion = Questions[QuestionNumber];

            if (currentQuestion == null) return;

            var a1 = currentQuestion.QuestionVariables["a1"];
            var d = currentQuestion.QuestionVariables["d"];
            var ansIndex = currentQuestion.QuestionVariables["ansIndex"];

            for (var index = 0; index < _sequenceTextBlocks.Length; index++)
            {
                if (index == ansIndex)
                {
                    _sequenceTextBlocks[index].Visibility = Visibility.Collapsed;
                    _sequenceInputs[index].Visibility = Visibility.Visible;

                    _sequenceInputs[index].Text = string.Empty;
                    _sequenceInputs[index].Focus();
                }
                else
                {
                    _sequenceTextBlocks[index].Visibility = Visibility.Visible;
                    _sequenceInputs[index].Visibility = Visibility.Collapsed;

                    _sequenceTextBlocks[index].Text = (a1 + (index * d)).ToString();
                }
            }
        }

        protected override void LockQuestion(bool locked)
        {
            var currentQuestion = Questions[QuestionNumber];
            _sequenceInputs[currentQuestion.QuestionVariables["ansIndex"]].IsEnabled = !locked;

            if (locked)
                MainWindow.NextQBtn.Focus();
        }

        public override void CheckAnswer()
        {
            base.CheckAnswer();

            var currentQuestion = Questions[QuestionNumber];

            var ansIndex = currentQuestion.QuestionVariables["ansIndex"];

            if (_sequenceInputs[ansIndex].Text == currentQuestion.ExpectedAnswer["ans"])
                RightAnswer();
            else
                WrongAnswer(currentQuestion.ExpectedAnswer["ans"]);
        }
    }
}
