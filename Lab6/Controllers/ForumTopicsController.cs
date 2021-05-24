using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Backend6.Data;
using Backend6.Models;
using Backend6.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Backend6.Services;

namespace Backend6.Controllers
{
    public class ForumTopicsController: Controller
    {
        public readonly ApplicationDbContext context;
        public readonly UserManager<ApplicationUser> userManager;
        public readonly IUserPermissionsService userPermissionsService;

        public ForumTopicsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUserPermissionsService userPermissionsService)
        {
            this.context = context;
            this.userManager = userManager;
            this.userPermissionsService = userPermissionsService;
        }

        // GET: /ForumTopics
        [AllowAnonymous]
        public async Task<IActionResult> Index(Int32? id)
        {
            if (id== null)
                return this.NotFound();

            return this.View(await this.context.ForumTopics
                .Include(m => m.Forum)
                .Include("Messages.Attachments")
                .Include("Messages.Creator")
                .SingleOrDefaultAsync(m => m.Id == id)
            );
        }

        // GET: /ForumTopics/Create
        [Authorize]
        public async Task<IActionResult> Create(Int32? forumId)
        {
            if (forumId == null)
                return this.NotFound();

            var forum = await this.context.Forums.SingleOrDefaultAsync(m => m.Id == forumId);
            if (forum == null)
                return this.NotFound();

            this.ViewBag.ForumId = forumId;
            return this.View(new ForumTopicCreateModel());
        }

        // POST: /ForumTopics/Create
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Int32? forumId, ForumTopicCreateModel model)
        {
            if (forumId == null)
                return this.NotFound();

            var forum = await this.context.Forums.SingleOrDefaultAsync(m => m.Id == forumId);
            if (forum == null)
                return this.NotFound();

            var user = await this.userManager.GetUserAsync(this.HttpContext.User);

            if (this.ModelState.IsValid)
            {
                var topic = new ForumTopic
                {
                    CreatorId = user.Id,
                    ForumId = forum.Id,
                    Name = model.Name,
                    Created = DateTime.Now
                };

                this.context.Add(topic);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index", "Forums", new { forum.Id });
            }

            this.ViewBag.ForumId = forumId;
            return this.View(model);
        }

        // GET: /ForumTopics/Edit
        public async Task<IActionResult> Edit(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var topic = await this.context.ForumTopics.SingleOrDefaultAsync(m => m.Id == id);
            if (topic == null || !this.userPermissionsService.CanEditOrDeleteTopic(topic))
                return this.NotFound();

            this.ViewBag.Id = topic.Id;
            return this.View(new ForumTopicCreateModel { Name = topic.Name });
        }

        // POST: /ForumTopics/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Int32? id, ForumTopicCreateModel model)
        {
            if (id == null)
                return this.NotFound();

            var topic = await this.context.ForumTopics.SingleOrDefaultAsync(m => m.Id == id);
            if (topic == null || !this.userPermissionsService.CanEditOrDeleteTopic(topic))
                return this.NotFound();

            if (this.ModelState.IsValid)
            {
                topic.Name = model.Name;
                
                this.context.Update(topic);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index", new { topic.Id });
            }

            this.ViewBag.Id = topic.Id;
            return this.View(model);
        }

        // GET: /ForumTopics/Delete
        public async Task<IActionResult> Delete(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var topic = await this.context.ForumTopics.SingleOrDefaultAsync(m => m.Id == id);
            if (topic == null || !this.userPermissionsService.CanEditOrDeleteTopic(topic))
                return this.NotFound();

            return this.View(topic);
        }

        // POST: /ForumTopics/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var topic = await this.context.ForumTopics.SingleOrDefaultAsync(m => m.Id == id);
            if (topic == null || !this.userPermissionsService.CanEditOrDeleteTopic(topic))
                return this.NotFound();

            this.context.Remove(topic);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction("Index", "Forums", new { id = topic.ForumId });
        }
    }
}
