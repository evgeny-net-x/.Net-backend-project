using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Backend6.Data;
using Backend6.Models;
using Backend6.Services;
using Backend6.Models.ViewModels;
using System.IO;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;

namespace Backend6.Controllers
{
    public class ForumMessageAttachmentsController : Controller
    {
        private static readonly HashSet<String> AllowedExtensions = new HashSet<String> { ".jpg", ".jpeg", ".png", ".gif" };

        private readonly ApplicationDbContext context;
        private readonly IHostingEnvironment hostEnv;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserPermissionsService userPermissionsService;

        public ForumMessageAttachmentsController(ApplicationDbContext context, IHostingEnvironment hostEnv, UserManager<ApplicationUser> userManager, IUserPermissionsService userPermissionsService)
        {
            this.context = context;
            this.hostEnv = hostEnv;
            this.userManager = userManager;
            this.userPermissionsService = userPermissionsService;
        }
        
        // GET: /ForumMessageAttachments/Create
        public async Task<IActionResult> Create(Int32? messageId)
        {
            if (messageId == null)
                return this.NotFound();

            var message = await this.context.ForumMessages.SingleOrDefaultAsync(m => m.Id == messageId);
            if (message == null)
                return this.NotFound();

            this.ViewBag.MessageId = message.Id;
            this.ViewBag.TopicId = message.TopicId;
            return this.View(new ForumMessageAttachmentCreateModel());
        }

        // POST: /ForumMessageAttachments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Int32? messageId, ForumMessageAttachmentCreateModel model)
        {
            if (messageId == null)
                return this.NotFound();

            var message = await this.context.ForumMessages.SingleOrDefaultAsync(m => m.Id == messageId);
            if (message == null || !this.userPermissionsService.CanCreateMessageAttachment(message))
                return this.NotFound();

            var fileName = Path.GetFileName(ContentDispositionHeaderValue.Parse(model.FormFile.ContentDisposition).FileName.Value.Trim('"'));
            var fileExt = Path.GetExtension(fileName);
            if (!ForumMessageAttachmentsController.AllowedExtensions.Contains(fileExt))
                this.ModelState.AddModelError(nameof(model.FormFile), "This file type is prohibited");

            if (model.FormFile != null)
            {
                var attachment = new ForumMessageAttachment
                {
                    MessageId = message.Id,
                    Created = DateTime.Now,
                    FileName = model.FormFile.FileName
                };

                var attachmentPath = Path.Combine(this.hostEnv.WebRootPath, "attachments", attachment.Id.ToString("N") + fileExt);
                attachment.FilePath = $"/attachments/{attachment.Id:N}{fileExt}";
                using (var fileStream = new FileStream(attachmentPath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read))
                {
                    await model.FormFile.CopyToAsync(fileStream);
                }

                this.context.Add(attachment);
                this.context.SaveChanges();
                return this.RedirectToAction("Index", "ForumTopics", new { id = message.TopicId });
            }

            this.ViewBag.TopicId = message.TopicId;
            return this.View(model);
        }

        // GET: /ForumMessageAttachments/Delete
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return this.NotFound();

            var attachment = await this.context.ForumMessageAttachments.Include(m => m.Message).SingleOrDefaultAsync(m => m.Id == id);
            if (attachment == null || !this.userPermissionsService.CanDeleteMessageAttachment(attachment))
                return this.NotFound();

            this.ViewBag.TopicId = attachment.Message.TopicId;
            return this.View(attachment);
        }

        // POST: /ForumMessageAttachments/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            if (id == null)
                return this.NotFound();

            var attachment = await this.context.ForumMessageAttachments.Include(m => m.Message).SingleOrDefaultAsync(m => m.Id == id);
            if (attachment == null || !this.userPermissionsService.CanDeleteMessageAttachment(attachment))
                return this.NotFound();

            Console.WriteLine(Path.Combine(this.hostEnv.WebRootPath, "attachments", attachment.Id.ToString("N") + Path.GetExtension(attachment.FilePath)));
            System.IO.File.Delete(Path.Combine(this.hostEnv.WebRootPath, "attachments", attachment.Id.ToString("N") + Path.GetExtension(attachment.FilePath)) );
            this.context.ForumMessageAttachments.Remove(attachment);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction("Index", "ForumTopics", new { id = attachment.Message.TopicId });
        }
    }
}
