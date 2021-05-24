using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Backend6.Models
{
    public class ForumCategory
    {
        public Int32 Id { get; set; }

        public ICollection<Forum> Forums { get; set; }

        [Required]
        public String Name { get; set; }
    }
}
