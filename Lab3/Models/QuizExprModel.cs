using System;
using System.ComponentModel.DataAnnotations;
namespace Backend3.Models
{
    public class QuizExprModel
    {
        [Required(ErrorMessage = "First operand is required")]
        [RegularExpression(@"-?[0-9]+", ErrorMessage = "First operand is a number")]
        public int A { get; set; }

        //[Required(ErrorMessage = "Operator is required")]
        [RegularExpression(@"[+\-*/]", ErrorMessage = "Allowed operators: +, -, *, /")]
        public char Op { get; set; }

        [Required(ErrorMessage = "Second operand is required")]
        [RegularExpression(@"-?[0-9]+", ErrorMessage = "Second operand is a number")]
        public int B { get; set; }

        [Required(ErrorMessage = "Result is required")]
        [RegularExpression(@"-?[0-9]+", ErrorMessage = "Result is a number")]
        public int Result { get; set; }

        public QuizExprModel Clone() => (QuizExprModel) this.MemberwiseClone();

        public int CalculateResult()
        {
            switch (this.Op)
            {
                case '+': return this.A + this.B;
                case '-': return this.A - this.B;
                case '*': return this.A * this.B;
                case '/': return this.A / this.B;
                default:  return 0;
            }
        }
    }
}
