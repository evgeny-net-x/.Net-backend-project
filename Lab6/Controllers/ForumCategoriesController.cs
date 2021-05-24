using System;
using System.Collections.Generic;
using System.Linq;
using Backend6.Models;
using System.Threading.Tasks;
using Backend6.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend6.Data;
using Microsoft.AspNetCore.Authorization;

namespace Backend6.Controllers
{
    public class ForumCategoriesController : Controller
    {
        private readonly ApplicationDbContext context;

        public ForumCategoriesController(ApplicationDbContext context)
        {
            this.context = context;
        }
        
        // GET: /ForumCategories
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return this.View(await this.context.ForumCategories
                .Include(m => m.Forums)
                .ThenInclude(m => m.ForumTopics)
                .ToListAsync()
            );
        }

        // GET: /ForumCategories/Create
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public IActionResult Create()
        {
            return this.View(new ForumCategoryCreateModel());
        }

        // POST: /ForumCategories/Create
        [HttpPost]
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public async Task<IActionResult> Create(ForumCategoryCreateModel model)
        {
            if (this.ModelState.IsValid)
            {
                var category = new ForumCategory { Name = model.Name };
                this.context.Add(category);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        // GET: /ForumCategories/Edit
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public async Task<IActionResult> Edit(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var category = await this.context.ForumCategories.SingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
                return this.NotFound();

            return this.View(new ForumCategoryCreateModel { Name = category.Name });
        }

        // POST: /ForumCategories/Edit
        [HttpPost]
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public async Task<IActionResult> Edit(Int32? id, ForumCategoryCreateModel model)
        {
            if (id == null)
                return this.NotFound();

            var category = await this.context.ForumCategories.SingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
                return this.NotFound();

            if (this.ModelState.IsValid)
            {
                category.Name = model.Name;
                this.context.Update(category);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }

        // GET: /ForumCategories/Delete
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public async Task<IActionResult> Delete(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var category = await this.context.ForumCategories.SingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
                return this.NotFound();

            return this.View(category);
        }

        // POST: /ForumCategories/Delete
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public async Task<IActionResult> DeleteConfirmed(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var category = await this.context.ForumCategories.SingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
                return this.NotFound();

            this.context.Remove(category);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }
    }
}
