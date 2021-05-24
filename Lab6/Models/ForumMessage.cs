using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Backend6.Models
{
    public class ForumMessage
    {
        public Int32 Id { get; set; }

        [Required]
        public String CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }

        public Int32 TopicId { get; set; }
        public ForumTopic Topic { get; set; }

        public ICollection<ForumMessageAttachment> Attachments { get; set; }

        [Required]
        public String Text { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
