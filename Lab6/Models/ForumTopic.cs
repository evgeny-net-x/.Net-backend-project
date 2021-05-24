using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Backend6.Models
{
    public class ForumTopic
    {
        public Int32 Id { get; set; }

        public Int32 ForumId { get; set; }
        public Forum Forum { get; set; }

        [Required]
        public String CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }

        public ICollection<ForumMessage> Messages { get; set; }

        [Required]
        public String Name { get; set; }
        public DateTime Created { get; set; }
    }
}
