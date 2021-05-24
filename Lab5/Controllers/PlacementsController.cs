using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Backend5.Data;
using Backend5.Models;
using Backend5.Models.ViewModels;

namespace Backend5.Controllers
{
    public class PlacementsController : Controller
    {
        private readonly ApplicationDbContext context;

        public PlacementsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: /Placements
        public async Task<IActionResult> Index()
        {
            return this.View(await this.context.Placements.Include(m => m.Ward).Include(m => m.Patient).ToListAsync());
        }

        // GET: /Placements/Details
        public async Task<IActionResult> Details(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var placement = await this.context.Placements
                .Include(p => p.Patient)
                .Include(p => p.Ward)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (placement == null)
                return this.NotFound();

            return this.View(placement);
        }

        // GET: /Placements/Create
        public IActionResult Create()
        {
            this.ViewBag.PatientId = new SelectList(this.context.Patients, "Id", "Name");
            this.ViewBag.WardId = new SelectList(this.context.Wards, "Id", "Name");
            return this.View(new PlacementCreateModel());
        }

        // POST: Placements/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlacementCreateModel model)
        {
            if (this.ModelState.IsValid)
            {
                var placement = new Placement
                {
                    WardId = model.WardId,
                    PatientId = model.PatientId,
                    Bed = model.Bed
                };

                this.context.Add(placement);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            this.ViewBag.PatientId = new SelectList(this.context.Patients, "Id", "Name");
            this.ViewBag.WardId = new SelectList(this.context.Wards, "Id", "Name");
            return this.View(model);
        }

        // GET: /Placements/Edit/5
        public async Task<IActionResult> Edit(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var placement = await this.context.Placements.SingleOrDefaultAsync(m => m.Id == id);
            if (placement == null)
                return this.NotFound();

            var model = new PlacementCreateModel
            {
                PatientId = placement.PatientId,
                WardId = placement.WardId,
                Bed = placement.Bed
            };

            this.ViewBag.PatientId = new SelectList(this.context.Patients, "Id", "Name");
            this.ViewBag.WardId = new SelectList(this.context.Wards, "Id", "Name");
            return this.View(model);
        }

        // POST: Placements/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Int32? id, PlacementCreateModel model)
        {
            if (id == null)
                return this.NotFound();

            var placement = await this.context.Placements.SingleOrDefaultAsync(m => m.Id == id);
            if (placement == null)
                return this.NotFound();

            if (this.ModelState.IsValid)
            {
                placement.WardId = model.WardId;
                placement.PatientId = model.PatientId;
                placement.Bed = model.Bed;

                this.context.Update(placement);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index");
            }

            this.ViewBag.PatientId = new SelectList(this.context.Patients, "Id", "Name");
            this.ViewBag.WardId = new SelectList(this.context.Wards, "Id", "Name");
            return this.View(model);
        }

        // GET: Placements/Delete/5
        public async Task<IActionResult> Delete(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var placement = await this.context.Placements
                .Include(p => p.Patient)
                .Include(p => p.Ward)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (placement == null)
                return this.NotFound();

            return this.View(placement);
        }

        // POST: Placements/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var placement = await this.context.Placements.SingleOrDefaultAsync(m => m.Id == id);
            if (placement == null)
                return this.NotFound();

            this.context.Placements.Remove(placement);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction("Index");
        }
    }
}
