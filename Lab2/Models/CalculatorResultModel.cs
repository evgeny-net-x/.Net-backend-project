using System;
using System.ComponentModel.DataAnnotations;

namespace Backend2.Models
{
    public class CalculatorResultModel
    {
        [Required(ErrorMessage = "First operand is required")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "First operand is number")]
        public String a { get; set; }

        [Required(ErrorMessage = "Second operand is required")]
        [RegularExpression(@"[1-9][0-9]?", ErrorMessage = "Second operand is number and not zero")]
        public String b { get; set; }

        public char op { get; set; } 
    }
}
