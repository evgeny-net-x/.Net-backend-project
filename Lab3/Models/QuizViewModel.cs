using System;
using System.Collections.Generic;

namespace Backend3.Models
{
    public class QuizViewModel
    {
        public QuizExprModel Expr { get; set; }
        public List<QuizExprModel> Terms { get; set; } = new List<QuizExprModel>();
    }
}
