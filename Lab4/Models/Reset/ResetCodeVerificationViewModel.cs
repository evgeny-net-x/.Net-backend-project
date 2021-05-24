using System;
using System.ComponentModel.DataAnnotations;

namespace Backend4.Models.Reset
{
    public class ResetCodeVerificationViewModel : ResetViewModel
    {
        [Required]
        public String Code { get; set; }
    }
}