using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Backend5.Models
{
    public class WardStaff
    {
        public Int32 Id { get; set; }

        public Ward Ward { get; set; }
        public Int32 WardId { get; set; }

        [Required]
        [MaxLength(200)]
        public String Name { get; set; }

        public String Address { get; set; }
    }
}
