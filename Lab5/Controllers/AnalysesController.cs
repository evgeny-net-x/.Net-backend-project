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
    public class AnalysesController : Controller
    {
        private readonly ApplicationDbContext context;

        public AnalysesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: /Analyses
        public async Task<IActionResult> Index(Int32? patientId)
        {
            if (patientId == null)
                return this.NotFound();

            this.ViewBag.PatientId = patientId;
            return this.View(await this.context.Analyses
                .Include(m => m.Lab)
                .Where(m => m.PatientId == patientId)
                .ToListAsync()
            );
        }

        // GET: /Analyses/Details
        public async Task<IActionResult> Details(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var analysis = await this.context.Analyses
                .Include(m => m.Lab)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (analysis == null)
                return this.NotFound();

            return this.View(analysis);
        }

        // GET: /Analyses/Create
        public IActionResult Create(Int32? patientId)
        {
            if (patientId == null)
                return this.NotFound();

            this.ViewBag.PatientId = patientId;
            this.ViewBag.LabId = new SelectList(this.context.Labs, "Id", "Name");
            return this.View(new AnalysisCreateModel());
        }

        // POST: Analyses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Int32? patientId, AnalysisCreateModel model)
        {
            if (patientId == null)
                return this.NotFound();

            var patient = await this.context.Patients.SingleOrDefaultAsync(m => m.Id == patientId);
            if (patient == null)
                return this.NotFound();

            if (this.ModelState.IsValid)
            {
                var analysis = new Analysis
                {
                    PatientId = patient.Id,
                    LabId = model.LabId,
                    Type = model.Type,
                    Date = model.Date,
                    Status = model.Status
                };

                this.context.Add(analysis);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index", new { patientId });
            }

            this.ViewBag.PatientId = patientId;
            this.ViewBag.LabId = new SelectList(this.context.Labs, "Id", "Name", model.LabId);
            return this.View(model);
        }

        // GET: /Analyses/Edit
        public async Task<IActionResult> Edit(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var analysis = await this.context.Analyses.SingleOrDefaultAsync(m => m.Id == id);

            if (analysis == null)
                return this.NotFound();

            var model = new AnalysisCreateModel
            {
                LabId = analysis.LabId,
                Type = analysis.Type,
                Date = analysis.Date,
                Status = analysis.Status
            };

            this.ViewBag.PatientId = analysis.PatientId;
            this.ViewBag.LabId = new SelectList(this.context.Labs, "Id", "Name", analysis.LabId);
            return this.View(model);
        }

        // POST: /Analyses/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Int32? id, AnalysisCreateModel model)
        {
            if (id == null)
                return this.NotFound();

            var analysis = await this.context.Analyses.SingleOrDefaultAsync(m => m.Id == id);
            if (analysis == null)
                return this.NotFound();

            if (this.ModelState.IsValid)
            {
                analysis.LabId = model.LabId;
                analysis.Type = model.Type;
                analysis.Date = model.Date;
                analysis.Status = model.Status;

                this.context.Update(analysis);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction("Index", new { analysis.PatientId });
            }

            this.ViewBag.PatientId = analysis.PatientId;
            return this.View(model);
        }

        // GET: /Analyses/Delete
        public async Task<IActionResult> Delete(Int32? id)
        {
            if (id == null)
                return this.NotFound();

            var analysis = await this.context.Analyses
                .Include(m => m.Lab)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (analysis == null)
                return this.NotFound();

            return this.View(analysis);
        }

        // POST: /Analyses/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Int32 id)
        {
            var analysis = await this.context.Analyses.SingleOrDefaultAsync(m => m.Id == id);
            this.context.Analyses.Remove(analysis);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction("Index", new { analysis.PatientId });
        }
    }
}
