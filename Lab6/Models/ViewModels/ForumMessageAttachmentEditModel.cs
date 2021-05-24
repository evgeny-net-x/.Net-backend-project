using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Backend6.Models.ViewModels
{
    public class ForumMessageAttachmentEditModel
    {
        public IFormFile File { get; set; }
        public String FilePath { get; set; }
        public String FileName { get; set; }
    }
}
