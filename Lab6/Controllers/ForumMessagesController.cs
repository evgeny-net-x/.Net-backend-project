using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Backend6.Data;
using Backend6.Models;
using Backend6.Services;
using Backend6.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Backend6.Controllers
{
    public class ForumMessagesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserPermissionsService userPermissionsService;

        public ForumMessagesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUserPermissionsService userPermissionsService)
        {
            this.context = context;
            this.userManager = userManager;
            this.userPermissionsService = userPermissionsService;
        }
        
        // GET: /ForumMessages/Create
        [Authorize]
        public async Task<IActionResult> Create(Int32? topicId)
        {
            if (topicId == null)
                return this.NotFound();

            var topic = await this.context.ForumTopics.SingleOrDefaultAsync(m => m.Id == topicId);
            if (topic == null)
                return this.NotFound();

            this.ViewBag.TopicId = topic.Id;
            return this.View(new ForumMessageCreateModel());
        }

        // POST: /ForumMessages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Int32? topicId, ForumMessageCreateModel model)
        {
            if (topicId == null)
                return this.NotFound();

            var topic = await this.context.ForumTopics.SingleOrDefaultAsync(m => m.Id == topicId);
            if (topic == null)
                return this.NotFound();

            var user = await this.userManager.GetUserAsync(this.HttpContext.User);

            if (this.ModelState.IsValid)
            {
                var message = new ForumMessage
                {
                    CreatorId = user.Id,
                    TopicId = topic.Id,
                    Text = model.Text,
                    Created = DateTime.Now,
                    Modified = DateTime.Now
                };

                this.context.Add(message);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index", "ForumTopics", new { id = topic.Id });
            }

            this.ViewBag.TopicId = topic.Id;
            return this.View(model);
        }

        // GET: /ForumMessages/Edit
        public async Task<IActionResult> Edit(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var message = await this.context.ForumMessages.SingleOrDefaultAsync(m => m.Id == id);
            if (message == null || !this.userPermissionsService.CanEditOrDeleteMessage(message))
                return this.NotFound();

            this.ViewBag.TopicId = message.TopicId;
            return this.View(new ForumMessageCreateModel{ Text = message.Text });
        }

        // POST: /ForumMessages/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Int32? id, ForumMessageCreateModel model)
        {
            if (id == null)
                return this.NotFound();

            var message = await this.context.ForumMessages.SingleOrDefaultAsync(m => m.Id == id);
            if (message == null || !this.userPermissionsService.CanEditOrDeleteMessage(message))
                return this.NotFound();

            if (this.ModelState.IsValid)
            {
                message.Text = model.Text;
                message.Modified = DateTime.Now;

                this.context.Update(message);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index", "ForumTopics", new { id = message.TopicId });
            }

            this.ViewBag.TopicId = message.TopicId;
            return this.View(model);
        }

        // GET: /ForumMessages/Delete
        public async Task<IActionResult> Delete(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var message = await this.context.ForumMessages.Include(m => m.Topic).SingleOrDefaultAsync(m => m.Id == id);
            if (message == null || !this.userPermissionsService.CanEditOrDeleteMessage(message))
                return this.NotFound();

            return this.View(message);
        }

        // POST: /ForumMessages/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var message = await this.context.ForumMessages.SingleOrDefaultAsync(m => m.Id == id);
            if (message == null || !this.userPermissionsService.CanEditOrDeleteMessage(message))
                return this.NotFound();

            this.context.ForumMessages.Remove(message);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction("Index", "ForumTopics", new { id = message.TopicId });
        }
    }
}
