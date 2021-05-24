using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Backend6.Models.ViewModels
{
    public class ForumMessageAttachmentCreateModel
    {
        public IFormFile FormFile { get; set; }
    }
}
