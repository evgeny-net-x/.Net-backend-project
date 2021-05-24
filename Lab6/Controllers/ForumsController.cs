using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Backend6.Data;
using Backend6.Models;
using Backend6.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Backend6.Controllers
{
    public class ForumsController : Controller
    {
        private readonly ApplicationDbContext context;

        public ForumsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: /Forums
        [AllowAnonymous]
        public async Task<IActionResult> Index(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            return this.View(await this.context.Forums
                .Include("ForumTopics.Creator")
                .Include("ForumTopics.Messages")
                .SingleOrDefaultAsync(m => m.Id == id)
            );
        }

        // GET: /Forums/Create
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public async Task<IActionResult> Create(Int32? categoryId)
        {
            if (categoryId == null)
                return this.NotFound();

            var category = await this.context.ForumCategories.SingleOrDefaultAsync(m => m.Id == categoryId);
            if (category == null)
                return this.NotFound();

            this.ViewBag.CategoryId = category.Id;
            return this.View(new ForumCreateModel());
        }

        // POST: /Forums/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public async Task<IActionResult> Create(Int32? categoryId, ForumCreateModel model)
        {
            if (categoryId == null)
                return this.NotFound();

            var category = await this.context.ForumCategories.SingleOrDefaultAsync(m => m.Id == categoryId);
            if (category == null)
                return this.NotFound();

            if (ModelState.IsValid)
            {
                var forum = new Forum
                {
                    CategoryId = category.Id,
                    Name = model.Name,
                    Description = model.Description
                };

                this.context.Add(forum);
                await this.context.SaveChangesAsync();
                return RedirectToAction("Index", "ForumCategories");
            }

            this.ViewBag.CategoryId = category.Id;
            return View(model);
        }

        // GET: /Forums/Edit
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public async Task<IActionResult> Edit(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var forum = await this.context.Forums.SingleOrDefaultAsync(m => m.Id == id);
            if (forum == null)
                return this.NotFound();

            var model = new ForumCreateModel
            {
                Name = forum.Name,
                Description = forum.Description
            };

            this.ViewBag.CategoryId = forum.CategoryId;
            return this.View(model);
        }

        // POST: /Forums/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public async Task<IActionResult> Edit(Int32? id, ForumCreateModel model)
        {
            if (id == null)
                return this.NotFound();

            var forum = await this.context.Forums.SingleOrDefaultAsync(m => m.Id == id);
            if (forum == null)
                return this.NotFound();

            if (this.ModelState.IsValid)
            {
                forum.Name = model.Name;
                forum.Description = model.Description;

                this.context.Update(forum);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index", "ForumCategories");
            }

            this.ViewBag.CategoryId = forum.CategoryId;
            return this.View(model);
        }

        // GET: /Forums/Delete
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public async Task<IActionResult> Delete(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var forum = await this.context.Forums.SingleOrDefaultAsync(m => m.Id == id);
            if (forum == null)
                return this.NotFound();

            return this.View(forum);
        }

        // POST: /Forums/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public async Task<IActionResult> DeleteConfirmed(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var forum = await this.context.Forums.SingleOrDefaultAsync(m => m.Id == id);
            if (forum == null)
                return this.NotFound();

            this.context.Remove(forum);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction("Index", "ForumCategories");
        }
    }
}
