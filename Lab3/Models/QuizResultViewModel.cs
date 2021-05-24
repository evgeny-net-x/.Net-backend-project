using System;
using System.Collections.Generic;

namespace Backend3.Models
{
    public class QuizResultViewModel
    {
        public Int32 CorrectAnswersCount { get; set; }

        public List<Boolean> IsCorrect { get; set; } = new List<Boolean>();
        public List<QuizExprModel> Terms { get; set; } = new List<QuizExprModel>();
    }
}
