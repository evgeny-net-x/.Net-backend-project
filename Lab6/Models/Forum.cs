using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Backend6.Models
{
    public class Forum
    {
        public Int32 Id { get; set; }

        public ICollection<ForumTopic> ForumTopics { get; set; }

        public Int32 CategoryId { get; set; }
        public ForumCategory Category { get; set; }

        [Required]
        public String Name { get; set; }
        public String Description { get; set; }
    }
}
